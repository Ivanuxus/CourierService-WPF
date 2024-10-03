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
        private readonly IDeliveryRepository _deliveryRepository; // Add this line for Delivery repository

        public AddOrderWindow(
            IOrderRepository orderRepository,
            IClientRepository clientRepository,
            ICargoTypeRepository cargoTypeRepository,
            ICourierRepository courierRepository,
            ITransportRepository transportRepository,
            IDeliveryRepository deliveryRepository) // Add this parameter
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _cargoTypeRepository = cargoTypeRepository;
            _courierRepository = courierRepository;
            _transportRepository = transportRepository;
            _deliveryRepository = deliveryRepository; // Initialize Delivery repository
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get order date
                DateTime orderDate = OrderDatePicker.SelectedDate?.ToUniversalTime() ?? DateTime.UtcNow;

                // Get maximum OrderID and increase it by 1
                int maxOrderId = _orderRepository.GetMaxOrderId();
                int newOrderId = maxOrderId + 1;

                // Get names from text boxes
                string clientName = ClientNameTextBox.Text.Trim();
                string cargoTypeName = CargoTypeNameTextBox.Text.Trim();
                string courierName = CourierNameTextBox.Text.Trim();
                string transportType = TransportTypeTextBox.Text.Trim();

                Client client;
                var clientNames = clientName.Split(' ');
                if (clientNames.Length < 2)
                {
                    throw new Exception("Please enter at least a first and last name for the client.");
                }
                string clientFirstName = clientNames[0];
                string clientLastName = string.Join(" ", clientNames.Skip(1));

                // Check if client exists
                client = _clientRepository.GetAllClients()
                    .FirstOrDefault(c => c.FirstName.Equals(clientFirstName, StringComparison.OrdinalIgnoreCase)
                                        && c.LastName.Equals(clientLastName, StringComparison.OrdinalIgnoreCase));
                if (client == null)
                {
                    int maxClientId = _clientRepository.GetMaxClientId();
                    client = new Client
                    {
                        ClientID = maxClientId + 1,
                        FirstName = clientFirstName,
                        LastName = clientLastName,
                        Phone = "Неизвестно",
                        Email = "Неизвестно"
                    };
                    _clientRepository.AddClient(client);
                }

                // Process cargo type
                CargoType cargoType;
                cargoType = _cargoTypeRepository.GetAllCargoTypes()
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

                // Process courier
                Courier courier;
                var courierNames = courierName.Split(' ');
                if (courierNames.Length < 2)
                {
                    throw new Exception("Please enter at least a first and last name for the courier.");
                }
                string courierFirstName = courierNames[0];
                string courierLastName = string.Join(" ", courierNames.Skip(1));

                courier = _courierRepository.GetAllCouriers()
                    .FirstOrDefault(cr => cr.FirstName.Equals(courierFirstName, StringComparison.OrdinalIgnoreCase)
                                       && cr.LastName.Equals(courierLastName, StringComparison.OrdinalIgnoreCase));
                if (courier == null)
                {
                    int maxCourierId = _courierRepository.GetMaxCourierId();
                    courier = new Courier
                    {
                        CourierID = maxCourierId + 1,
                        FirstName = courierFirstName,
                        LastName = courierLastName,
                        Phone = "Неизвестно" // Устанавливаем значение по умолчанию
                    };
                    _courierRepository.AddCourier(courier);
                }

                // Process transport
                Transport transport;
                transport = _transportRepository.GetAllTransports()
                    .FirstOrDefault(t => t.Type.Equals(transportType, StringComparison.OrdinalIgnoreCase));
                if (transport == null)
                {
                    int maxTransportId = _transportRepository.GetMaxTransportId();
                    transport = new Transport
                    {
                        TransportID = maxTransportId + 1,
                        Type = transportType,
                        LicensePlate = "Неизвестно" // Устанавливаем значение по умолчанию
                    };
                    _transportRepository.AddTransport(transport);
                }

                // Create new order
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

                // Add order to repository
                _orderRepository.AddOrder(newOrder);

                // Create and add delivery
                var delivery = new Delivery
                {
                    DeliveryID = newOrder.OrderID,
                    OrderID = newOrder.OrderID,
                    DeliveryDate = orderDate.AddDays(1), // Example: Set delivery date to the next day
                    TotalPrice = newOrder.BasePrice // Use the order base price as total price
                };
                _deliveryRepository.AddDelivery(delivery);

                MessageBox.Show($"Order successfully added with ID: {newOrder.OrderID}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values for all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
