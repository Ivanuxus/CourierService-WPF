using CourierService.Models.Entities;
using CourierService.Models.Interfaces;
using CourierService.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CourierService.Views
{
    public partial class OrdersView : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICargoTypeRepository _cargoTypeRepository;
        private readonly ICourierRepository _courierRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ITransportRepository _transportRepository;

        public OrdersView(
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

            DataContext = new OrdersViewModel(
                orderRepository,
                clientRepository,
                cargoTypeRepository,
                courierRepository,
                transportRepository);
            LoadOrderIds();
            
        }

        public void LoadOrderIds()
        {
            var orders = _orderRepository.GetAllOrders();
            OrderIdComboBox.Items.Clear(); // Очистка перед добавлением новых элементов
            foreach (var order in orders)
            {
                OrderIdComboBox.Items.Add(order.OrderID);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new AddOrderWindow(_orderRepository, _clientRepository, _cargoTypeRepository, _courierRepository, _transportRepository, _deliveryRepository);
            addOrderWindow.ShowDialog();
            LoadOrders();
        }

        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            var discountWindow = new DiscountWindow(_orderRepository, _clientRepository); // Передаем IClientRepository
            discountWindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderIdComboBox.SelectedItem is int selectedOrderId)
            {
                var editOrderWindow = new EditOrderWindow(_orderRepository, selectedOrderId, _clientRepository, _cargoTypeRepository, _courierRepository, _transportRepository);
                editOrderWindow.ShowDialog();
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteOrderWindow = new DeleteOrderWindow(
                _orderRepository,
                _clientRepository,
                _cargoTypeRepository,
                _courierRepository,
                _transportRepository,
                _deliveryRepository
            );
            deleteOrderWindow.ShowDialog();

            // Обновление списка заказов после удаления
            LoadOrders();
        }


        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(OrdersDataGrid.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("BasePrice", ListSortDirection.Ascending));
            view.Refresh();
        }

        private void SortDescending_Click(object sender, RoutedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(OrdersDataGrid.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("BasePrice", ListSortDirection.Descending));
            view.Refresh();
        }

        public void LoadOrders()
        {
            var viewModel = DataContext as OrdersViewModel;
            if (viewModel != null)
            {
                viewModel.LoadOrders(); // Загрузка заказов
                OrdersDataGrid.ItemsSource = viewModel.Orders; // Присваиваем данные в DataGrid
            }
        }
    }
}