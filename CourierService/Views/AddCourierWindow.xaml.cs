using System;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Views
{
    public partial class AddCourierWindow : Window
    {
        private readonly ICourierRepository _courierRepository;

        public AddCourierWindow(ICourierRepository courierRepository)
        {
            InitializeComponent();
            _courierRepository = courierRepository;
        }

        private void AddCourierButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем имя и фамилию курьера из текстовых полей
                string firstName = FirstNameTextBox.Text.Trim();
                string lastName = LastNameTextBox.Text.Trim();
                string phone = PhoneTextBox.Text.Trim();

                // Проверка на пустые поля
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    MessageBox.Show("Пожалуйста, введите имя и фамилию курьера.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Находим максимальный CourierID и увеличиваем на 1
                int maxCourierId = _courierRepository.GetMaxCourierId();
                int newCourierId = maxCourierId + 1;

                // Создаем нового курьера
                Courier newCourier = new Courier
                {
                    CourierID = newCourierId,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = string.IsNullOrEmpty(phone) ? "Неизвестно" : phone // Устанавливаем значение по умолчанию, если телефон не введен
                };

                // Добавляем курьера в репозиторий
                _courierRepository.AddCourier(newCourier);

                // Сообщаем об успешном добавлении
                MessageBox.Show($"Курьер '{firstName} {lastName}' успешно добавлен с ID: {newCourierId}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Закрываем окно
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении курьера: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
