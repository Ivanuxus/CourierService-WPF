using CourierService.Util;
using CourierService.Views;
using Ninject;
using System.Text;
using System.Windows;

namespace CourierService
{
    public partial class App : Application
    {
        private IKernel _kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _kernel = new StandardKernel(new NinjectRegistrations());
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Открываем окно входа
            var loginWindow = _kernel.Get<LoginWindow>();
            bool? dialogResult = loginWindow.ShowDialog();

            if (dialogResult == true)
            {
                // Если логин успешен, открываем главное окно
                var ordersView = _kernel.Get<OrdersView>();

                // Устанавливаем OrdersView как главное окно
                Application.Current.MainWindow = ordersView;

            
            }
            else
            {
                // Закрываем приложение, если вход не удался или было отменено
                Shutdown(); // Закрываем приложение только если вход не удался
            }
        }
    }
}
