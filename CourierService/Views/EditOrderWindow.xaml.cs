using System;
using System.Linq;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Views
{
    public partial class EditOrderWindow : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICargoTypeRepository _cargoTypeRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly ITransportRepository _transportRepository;
        private readonly int _orderId;

        public EditOrderWindow(
            IOrderRepository orderRepository,
            int orderId,
            IClientRepository clientRepository,
            ICargoTypeRepository cargoTypeRepository,
            ICourierRepository courierRepository,
            ITransportRepository transportRepository)
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _cargoTypeRepository = cargoTypeRepository;
            _courierRepository = courierRepository;
            _transportRepository = transportRepository;
            _orderId = orderId;

            LoadComboBoxes();
            LoadOrderData();
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
        }

        private void LoadOrderData()
        {
            var order = _orderRepository.GetOrderById(_orderId);
            if (order != null)
            {
                OrderIdTextBlock.Text = order.OrderID.ToString();

                // Установка значений в ComboBox
                ClientComboBox.SelectedValue = order.ClientID;
                OrderDatePicker.SelectedDate = order.OrderDate; // Устанавливаем дату заказа
                CargoDescriptionTextBox.Text = order.CargoDescription;

                CargoTypeComboBox.SelectedValue = order.CargoTypeID; // Устанавливаем тип груза
                CourierComboBox.SelectedValue = order.CourierID; // Устанавливаем курьера

                BasePriceTextBox.Text = order.BasePrice.ToString();
            }
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение даты заказа
                DateTime orderDate = OrderDatePicker.SelectedDate?.ToUniversalTime() ?? DateTime.UtcNow;

                // Получение выбранных значений из ComboBox
                if (ClientComboBox.SelectedValue == null ||
                    CargoTypeComboBox.SelectedValue == null ||
                    CourierComboBox.SelectedValue == null)
                {
                    throw new Exception("Пожалуйста, заполните все поля.");
                }

                int clientId = (int)ClientComboBox.SelectedValue;
                int cargoTypeId = (int)CargoTypeComboBox.SelectedValue;
                int courierId = (int)CourierComboBox.SelectedValue;

                // Получение текущего заказа
                var currentOrder = _orderRepository.GetOrderById(_orderId);
                if (currentOrder == null)
                {
                    throw new Exception("Заказ не найден.");
                }

                // Обновление данных заказа
                currentOrder.ClientID = clientId;
                currentOrder.OrderDate = orderDate;
                currentOrder.CargoDescription = CargoDescriptionTextBox.Text;
                currentOrder.CargoTypeID = cargoTypeId;
                currentOrder.BasePrice = decimal.Parse(BasePriceTextBox.Text);
                currentOrder.CourierID = courierId;

                // Сохранение изменений
                _orderRepository.UpdateOrder(currentOrder);

                MessageBox.Show($"Заказ успешно обновлён с ID: {currentOrder.OrderID}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для полей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
