using CourierService.Models.Entities;
using CourierService.Models.Interfaces;
using CourierService.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

public class OrdersViewModel : INotifyPropertyChanged
{
    public ObservableCollection<OrderViewModel> Orders { get; set; }
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ICargoTypeRepository _cargoTypeRepository;
    private readonly ICourierRepository _courierRepository;
    private readonly ITransportRepository _transportRepository;

    public OrdersViewModel(
        IOrderRepository orderRepository,
        IClientRepository clientRepository,
        ICargoTypeRepository cargoTypeRepository,
        ICourierRepository courierRepository,
        ITransportRepository transportRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _cargoTypeRepository = cargoTypeRepository;
        _courierRepository = courierRepository;
        _transportRepository = transportRepository;

        Orders = new ObservableCollection<OrderViewModel>();
        LoadOrders();
    }

    public void LoadOrders()
    {
        var orders = _orderRepository.GetAllOrders();
        var clients = _clientRepository.GetAllClients();
        var cargoTypes = _cargoTypeRepository.GetAllCargoTypes();
        var couriers = _courierRepository.GetAllCouriers();
        var transports = _transportRepository.GetAllTransports();

        Orders.Clear();
        foreach (var order in orders)
        {
            var client = clients.FirstOrDefault(c => c.ClientID == order.ClientID);
            var cargoType = cargoTypes.FirstOrDefault(ct => ct.CargoTypeID == order.CargoTypeID);
            var courier = order.CourierID.HasValue
                ? couriers.FirstOrDefault(cr => cr.CourierID == order.CourierID.Value)
                : null;
            var transport = order.TransportID.HasValue
                ? transports.FirstOrDefault(t => t.TransportID == order.TransportID.Value)
                : null;

            Orders.Add(new OrderViewModel
            {
                OrderID = order.OrderID,
                ClientName = client != null ? $"{client.FirstName} {client.LastName}" : "Неизвестный клиент",
                OrderDate = order.OrderDate,
                CargoDescription = order.CargoDescription,
                CargoTypeName = cargoType != null ? cargoType.TypeName : "Неизвестный тип",
                BasePrice = order.BasePrice,
                CourierName = courier != null ? $"{courier.FirstName} {courier.LastName}" : "Нет курьера",
                TransportType = transport != null ? transport.Type : "Нет транспорта"
            });
        }

        // Установка сортировки по BasePrice
        var view = CollectionViewSource.GetDefaultView(Orders);
        view.SortDescriptions.Clear();
        view.SortDescriptions.Add(new SortDescription(nameof(OrderViewModel.BasePrice), ListSortDirection.Ascending));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
