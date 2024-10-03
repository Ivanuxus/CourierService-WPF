using System;
using System.Linq;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Views
{
    public partial class AddClientWindow : Window
    {
        private readonly IClientRepository _clientRepository;

        public AddClientWindow(IClientRepository clientRepository)
        {
            InitializeComponent();
            _clientRepository = clientRepository;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение введенных данных
                string firstName = FirstNameTextBox.Text.Trim();
                string lastName = LastNameTextBox.Text.Trim();
                string phone = PhoneTextBox.Text.Trim();
                string email = EmailTextBox.Text.Trim();

                // Валидация обязательных полей
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    MessageBox.Show("Имя и фамилия обязательны для заполнения.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка, существует ли уже такой клиент
                var existingClient = _clientRepository.GetAllClients()
                    .FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                      && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

                if (existingClient != null)
                {
                    MessageBox.Show("Клиент с таким именем и фамилией уже существует.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Получение максимального ClientID
                int maxClientId = _clientRepository.GetMaxClientId();
                int newClientId = maxClientId + 1;

                // Создание нового клиента
                var newClient = new Client
                {
                    ClientID = newClientId,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = string.IsNullOrEmpty(phone) ? "Неизвестно" : phone,
                    Email = string.IsNullOrEmpty(email) ? "Неизвестно" : email,
                    Discount = 0 // По умолчанию
                };

                // Добавление клиента в репозиторий
                _clientRepository.AddClient(newClient);

                // Информирование пользователя об успешном добавлении
                MessageBox.Show("Клиент успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
