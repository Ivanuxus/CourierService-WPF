using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private int _orderId;
        private int _clientId;
        private DateTime _orderDate;
        private string _cargoDescription;
        private int _cargoTypeId;
        private decimal _basePrice;
        private int? _courierId;
        private int? _transportId;
        private string _clientName;
        private string _cargoTypeName;
        private string _courierName;
        private string _transportType;
        public string ClientName
        {
            get => _clientName;
            set { _clientName = value; OnPropertyChanged(nameof(ClientName)); }
        }
        public string CargoTypeName
        {
            get => _cargoTypeName;
            set { _cargoTypeName = value; OnPropertyChanged(nameof(CargoTypeName)); }
        }
        public string CourierName
        {
            get => _courierName;
            set { _courierName = value; OnPropertyChanged(nameof(CourierName)); }
        }   
        public string TransportType
        {
            get => _transportType;
            set { _transportType = value; OnPropertyChanged(nameof(TransportType)); }
        }

        public int OrderID
        {
            get => _orderId;
            set { _orderId = value; OnPropertyChanged(nameof(OrderID)); }
        }

        public int ClientID
        {
            get => _clientId;
            set { _clientId = value; OnPropertyChanged(nameof(ClientID)); }
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set { _orderDate = value; OnPropertyChanged(nameof(OrderDate)); }
        }

        public string CargoDescription
        {
            get => _cargoDescription;
            set { _cargoDescription = value; OnPropertyChanged(nameof(CargoDescription)); }
        }

        public int CargoTypeID
        {
            get => _cargoTypeId;
            set { _cargoTypeId = value; OnPropertyChanged(nameof(CargoTypeID)); }
        }

        public decimal BasePrice
        {
            get => _basePrice;
            set { _basePrice = value; OnPropertyChanged(nameof(BasePrice)); }
        }

        public int? CourierID
        {
            get => _courierId;
            set { _courierId = value; OnPropertyChanged(nameof(CourierID)); }
        }

        public int? TransportID
        {
            get => _transportId;
            set { _transportId = value; OnPropertyChanged(nameof(TransportID)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OrdersViewModel
    {
        public ObservableCollection<OrderViewModel> Orders { get; set; }
        private readonly IOrderRepository _orderRepository;
        public ObservableCollection<Client> Clients { get; set; }

        public OrdersViewModel(IOrderRepository orderRepository, IClientRepository clientRepository)
        {
            _orderRepository = orderRepository;
            Orders = new ObservableCollection<OrderViewModel>();
            Clients = new ObservableCollection<Client>(clientRepository.GetAllClients());
            LoadOrders();
        }


        public void LoadOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(new OrderViewModel
                {
                    OrderID = order.OrderID,
                    ClientID = order.ClientID,
                    OrderDate = order.OrderDate,
                    CargoDescription = order.CargoDescription,
                    CargoTypeID = order.CargoTypeID,
                    BasePrice = order.BasePrice,
                    CourierID = order.CourierID,
                    TransportID = order.TransportID
                });
            }

            // Установка сортировки по BasePrice
            var view = CollectionViewSource.GetDefaultView(Orders);
            view.SortDescriptions.Add(new SortDescription("BasePrice", ListSortDirection.Ascending));
        }
    }
}