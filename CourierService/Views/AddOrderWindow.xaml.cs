using System;
using System.Linq;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Views
{
    public partial class AddOrderWindow : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICargoTypeRepository _cargoTypeRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly ITransportRepository _transportRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public AddOrderWindow(
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            ICargoTypeRepository cargoTypeRepository,
            ICourierRepository courierRepository,
            ITransportRepository transportRepository,
            IDeliveryRepository deliveryRepository)
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _cargoTypeRepository = cargoTypeRepository;
            _courierRepository = courierRepository;
            _transportRepository = transportRepository;
            _deliveryRepository = deliveryRepository;

            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            // Загрузка клиентов
            ClientComboBox.ItemsSource = _clientRepository.GetAllClients()
                .Select(c => new
                {
                    c.ClientID,
                    FullName = $"{c.FirstName} {c.LastName}"
                }).ToList();

            // Загрузка типов груза
            CargoTypeComboBox.ItemsSource = _cargoTypeRepository.GetAllCargoTypes().ToList();

            // Загрузка курьеров
            CourierComboBox.ItemsSource = _courierRepository.GetAllCouriers()
                .Select(cr => new
                {
                    cr.CourierID,
                    FullName = $"{cr.FirstName} {cr.LastName}"
                }).ToList();

            // Загрузка типов транспорта
            TransportTypeComboBox.ItemsSource = _transportRepository.GetAllTransports().ToList();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Откройте диалоговое окно для добавления нового клиента
            var addClientWindow = new AddClientWindow(_clientRepository)
            {
                Owner = this // Устанавливаем владельца окна
            };
            if (addClientWindow.ShowDialog() == true)
            {
                // Обновите ComboBox после добавления
                LoadComboBoxes();
            }
        }

        private void AddCargoTypeButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Откройте диалоговое окно для добавления нового типа груза
            var addCargoTypeWindow = new AddCargoTypeWindow(_cargoTypeRepository);
            if (addCargoTypeWindow.ShowDialog() == true)
            {
                // Обновите ComboBox после добавления
                LoadComboBoxes();
            }
        }

        private void AddCourierButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Откройте диалоговое окно для добавления нового курьера
            var addCourierWindow = new AddCourierWindow(_courierRepository);
            if (addCourierWindow.ShowDialog() == true)
            {
                // Обновите ComboBox после добавления
                LoadComboBoxes();
            }
        }

        private void AddTransportButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Откройте диалоговое окно для добавления нового типа транспорта
            var addTransportWindow = new AddTransportWindow(_transportRepository);
            if (addTransportWindow.ShowDialog() == true)
            {
                // Обновите ComboBox после добавления
                LoadComboBoxes();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение даты заказа
                DateTime orderDate = OrderDatePicker.SelectedDate?.ToUniversalTime() ?? DateTime.UtcNow;

                // Получение максимального OrderID и увеличение на 1
                int maxOrderId = _orderRepository.GetMaxOrderId();
                int newOrderId = maxOrderId + 1;

                // Получение выбранных значений из ComboBox
                if (ClientComboBox.SelectedValue == null ||
                    CargoTypeComboBox.SelectedValue == null ||
                    CourierComboBox.SelectedValue == null ||
                    TransportTypeComboBox.SelectedValue == null)
                {
                    throw new Exception("Пожалуйста, заполните все поля.");
                }

                int clientId = (int)ClientComboBox.SelectedValue;
                int cargoTypeId = (int)CargoTypeComboBox.SelectedValue;
                int courierId = (int)CourierComboBox.SelectedValue;
                int transportId = (int)TransportTypeComboBox.SelectedValue;

                // Получение объектов из репозиториев
                var client = _clientRepository.GetClientById(clientId);
                var cargoType = _cargoTypeRepository.GetCargoTypeById(cargoTypeId);
                var courier = _courierRepository.GetCourierById(courierId);
                var transport = _transportRepository.GetTransportById(transportId);

                // Создание нового заказа
                var newOrder = new Order
                {
                    OrderID = newOrderId,
                    ClientID = client.ClientID,
                    OrderDate = orderDate,
                    CargoDescription = CargoDescriptionTextBox.Text,
                    CargoTypeID = cargoType.CargoTypeID,
                    BasePrice = decimal.Parse(BasePriceTextBox.Text),
                    CourierID = courier.CourierID,
                    TransportID = transport.TransportID
                };

                // Добавление заказа в репозиторий
                _orderRepository.AddOrder(newOrder);

                // Создание и добавление доставки
                var delivery = new Delivery
                {
                    DeliveryID = newOrder.OrderID,
                    OrderID = newOrder.OrderID,
                    DeliveryDate = orderDate.AddDays(1), // Пример: Установить дату доставки на следующий день
                    TotalPrice = newOrder.BasePrice // Использовать базовую цену заказа как итоговую цену
                };
                _deliveryRepository.AddDelivery(delivery);

                MessageBox.Show($"Заказ успешно добавлен с ID: {newOrder.OrderID}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для всех полей.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
