using System;
using System.Linq;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Views
{
    public partial class AddCargoTypeWindow : Window
    {
        private readonly ICargoTypeRepository _cargoTypeRepository;

        public AddCargoTypeWindow(ICargoTypeRepository cargoTypeRepository)
        {
            InitializeComponent();
            _cargoTypeRepository = cargoTypeRepository;
        }

        private void AddCargoTypeButton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем название и описание типа груза из текстовых полей
                string cargoTypeName = CargoTypeNameTextBox.Text.Trim();
                string cargoTypeDescription = CargoTypeDescriptionTextBox.Text.Trim();

                // Проверка на пустое название типа груза
                if (string.IsNullOrEmpty(cargoTypeName))
                {
                    MessageBox.Show("Пожалуйста, введите название типа груза.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Находим максимальный CargoTypeID и увеличиваем на 1
                int maxCargoTypeId = _cargoTypeRepository.GetMaxCargoTypeId();
                int newCargoTypeId = maxCargoTypeId + 1;

                // Создаем новый тип груза
                CargoType newCargoType = new CargoType
                {
                    CargoTypeID = newCargoTypeId,
                    TypeName = cargoTypeName,
                    Description = cargoTypeDescription
                };

                // Добавляем тип груза в репозиторий
                _cargoTypeRepository.AddCargoType(newCargoType);

                // Сообщаем об успешном добавлении
                MessageBox.Show($"Тип груза '{cargoTypeName}' успешно добавлен с ID: {newCargoTypeId}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Закрываем окно
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении типа груза: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
