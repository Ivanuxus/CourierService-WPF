using System;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Views
{
    public partial class AddTransportWindow : Window
    {
        private readonly ITransportRepository _transportRepository;

        public AddTransportWindow(ITransportRepository transportRepository)
        {
            InitializeComponent();
            _transportRepository = transportRepository;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем максимальный TransportID и увеличиваем его на 1
                int maxTransportId = _transportRepository.GetMaxTransportId();
                int newTransportId = maxTransportId + 1;

                // Получаем данные из текстовых полей
                string transportType = TransportTypeTextBox.Text.Trim();
                string licensePlate = LicensePlateTextBox.Text.Trim();

                // Проверяем, чтобы текстовые поля не были пустыми
                if (string.IsNullOrEmpty(transportType) || string.IsNullOrEmpty(licensePlate))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем новый транспорт
                var newTransport = new Transport
                {
                    TransportID = newTransportId,
                    Type = transportType,
                    LicensePlate = licensePlate
                };

                // Добавляем транспорт в репозиторий
                _transportRepository.AddTransport(newTransport);

                MessageBox.Show($"Транспорт успешно добавлен с ID: {newTransport.TransportID}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true; // Закрываем окно и возвращаем результат
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении транспорта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
