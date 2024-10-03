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
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            var order = _orderRepository.GetOrderById(_orderId);
            if (order != null)
            {
                OrderIdTextBlock.Text = order.OrderID.ToString();
                // Изменяем поля ввода на текстовые для имен
                var client = _clientRepository.GetAllClients().FirstOrDefault(c => c.ClientID == order.ClientID);
                ClientNameTextBox.Text = $"{client.FirstName} {client.LastName}";

                OrderDatePicker.SelectedDate = order.OrderDate; // Устанавливаем дату заказа
                CargoDescriptionTextBox.Text = order.CargoDescription;

                var cargoType = _cargoTypeRepository.GetAllCargoTypes().FirstOrDefault(ct => ct.CargoTypeID == order.CargoTypeID);
                CargoTypeNameTextBox.Text = cargoType.TypeName; // Изменяем на текстовое поле для типа груза

                var courier = _courierRepository.GetAllCouriers().FirstOrDefault(cr => cr.CourierID == order.CourierID);
                CourierNameTextBox.Text = $"{courier.FirstName} {courier.LastName}"; // Изменяем на текстовое поле для имени курьера

                BasePriceTextBox.Text = order.BasePrice.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение даты заказа
                DateTime orderDate = OrderDatePicker.SelectedDate?.ToUniversalTime() ?? DateTime.UtcNow;

                // Получение имени клиента
                string clientName = ClientNameTextBox.Text.Trim();
                var clientNames = clientName.Split(' ');
                Client client;

                // Проверка существования клиента
                client = _clientRepository.GetAllClients()
                    .FirstOrDefault(c => c.FirstName.Equals(clientNames[0], StringComparison.OrdinalIgnoreCase)
                                      && c.LastName.Equals(string.Join(" ", clientNames.Skip(1))));

                if (client == null)
                {
                    int maxClientId = _clientRepository.GetMaxClientId();
                    client = new Client
                    {
                        ClientID = maxClientId + 1,
                        FirstName = clientNames[0],
                        LastName = string.Join(" ", clientNames.Skip(1)),
                        Phone = "Неизвестно", // Устанавливаем значение по умолчанию
                        Email = "Неизвестно" // Устанавливаем значение по умолчанию
                    };
                    _clientRepository.AddClient(client);
                }

                // Получение типа груза
                string cargoTypeName = CargoTypeNameTextBox.Text.Trim();
                CargoType cargoType = _cargoTypeRepository.GetAllCargoTypes()
                    .FirstOrDefault(ct => ct.TypeName.Equals(cargoTypeName, StringComparison.OrdinalIgnoreCase));

                if (cargoType == null)
                {
                    int maxCargoTypeId = _cargoTypeRepository.GetMaxCargoTypeId();
                    cargoType = new CargoType
                    {
                        CargoTypeID = maxCargoTypeId + 1,
                        TypeName = cargoTypeName,
                        Description = "Неизвестно" // Устанавливаем значение по умолчанию
                    };
                    _cargoTypeRepository.AddCargoType(cargoType);
                }

                // Получение курьера
                string courierName = CourierNameTextBox.Text.Trim();
                var courierNames = courierName.Split(' ');
                Courier courier = _courierRepository.GetAllCouriers()
                    .FirstOrDefault(cr => cr.FirstName.Equals(courierNames[0], StringComparison.OrdinalIgnoreCase)
                                       && cr.LastName.Equals(string.Join(" ", courierNames.Skip(1))));

                if (courier == null)
                {
                    int maxCourierId = _courierRepository.GetMaxCourierId();
                    courier = new Courier
                    {
                        CourierID = maxCourierId + 1,
                        FirstName = courierNames[0],
                        LastName = string.Join(" ", courierNames.Skip(1)),
                        Phone = "Неизвестно" // Устанавливаем значение по умолчанию
                    };
                    _courierRepository.AddCourier(courier);
                }

                // Получение текущего заказа
                var currentOrder = _orderRepository.GetOrderById(_orderId);

                // Обновление заказа
                currentOrder.ClientID = client.ClientID;
                currentOrder.OrderDate = orderDate;
                currentOrder.CargoDescription = CargoDescriptionTextBox.Text;
                currentOrder.CargoTypeID = cargoType.CargoTypeID;
                currentOrder.BasePrice = decimal.Parse(BasePriceTextBox.Text);
                currentOrder.CourierID = courier.CourierID;

                _orderRepository.UpdateOrder(currentOrder);
                MessageBox.Show("Заказ успешно изменен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для всех полей.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при изменении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
