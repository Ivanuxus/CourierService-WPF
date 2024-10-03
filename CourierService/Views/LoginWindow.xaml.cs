using CourierService.Models.Interfaces;
using CourierService.Services;
using Ninject;
using System.Windows;

namespace CourierService.Views
{
    public partial class LoginWindow : Window
    {
        private UserService _userService;
        private readonly IKernel _kernel;

        public LoginWindow(IKernel kernel)
        {
            InitializeComponent();
            _userService = new UserService();
            _kernel = kernel;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (_userService.Authenticate(username, password))
            {
                // Получаем все необходимые репозитории из контейнера
                var orderRepository = _kernel.Get<IOrderRepository>();
                var clientRepository = _kernel.Get<IClientRepository>();
                var cargoTypeRepository = _kernel.Get<ICargoTypeRepository>();
                var courierRepository = _kernel.Get<ICourierRepository>();
                var transportRepository = _kernel.Get<ITransportRepository>();
                var deliveryRepository = _kernel.Get<IDeliveryRepository>();

                // Создаем OrdersView с необходимыми репозиториями
                var ordersView = new OrdersView(orderRepository, clientRepository, cargoTypeRepository, courierRepository, transportRepository, deliveryRepository);

                // Устанавливаем OrdersView как главное окно
                Application.Current.MainWindow = ordersView;

                // Отображаем OrdersView
                ordersView.Show();

                // Устанавливаем результат диалога
                this.DialogResult = true;

                // Закрываем LoginWindow
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильное имя пользователя или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            try
            {
                _userService.Register(username, password);
                MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
