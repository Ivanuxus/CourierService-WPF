using System;
using System.IO;
using System.Text;
using System.Windows;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Microsoft.Win32; // Добавлено для SaveFileDialog

namespace CourierService.Views
{
    public partial class DeleteOrderWindow : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository; 
        private readonly ICargoTypeRepository _cargoTypeRepository; 
        private readonly ICourierRepository _courierRepository; 
        private readonly ITransportRepository _transportRepository;

        public DeleteOrderWindow(
    IOrderRepository orderRepository,
    IClientRepository clientRepository,
    ICargoTypeRepository cargoTypeRepository,
    ICourierRepository courierRepository,
    ITransportRepository transportRepository)
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _cargoTypeRepository = cargoTypeRepository;
            _courierRepository = courierRepository;
            _transportRepository = transportRepository;

            LoadOrderIds(); // Если нужно, чтобы загрузить заказы при открытии окна
        }

        private void LoadOrderIds()
        {
            var orders = _orderRepository.GetAllOrders();
            foreach (var order in orders)
            {
                OrderIdComboBox.Items.Add(order.OrderID); // Заполнение ComboBox
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
{
    // Check if the selected item is not null and is of type int
    if (OrderIdComboBox.SelectedItem is int selectedOrderId)
    {
        // Получение данных заказа до удаления
        var order = _orderRepository.GetOrderById(selectedOrderId);
        if (order == null)
        {
            MessageBox.Show("Заказ не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            // Генерация и сохранение договора
            GenerateAndSaveContract(order);

            // Удаление заказа
            _orderRepository.DeleteOrder(selectedOrderId);
            MessageBox.Show("Заказ успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    else
    {
        MessageBox.Show("Пожалуйста, выберите заказ для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}




        private void GenerateAndSaveContract(Order order)
        {
            // Открытие диалога сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить договор",
                Filter = "PDF файлы (*.pdf)|*.pdf",
                FileName = $"Contract_Order_{order.OrderID}_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Получение информации о клиенте, курьере и транспорте
                    var client = _clientRepository.GetClientById(order.ClientID);
                    var cargoType = _cargoTypeRepository.GetCargoTypeById(order.CargoTypeID);
                    var courier = order.CourierID.HasValue ? _courierRepository.GetCourierById(order.CourierID.Value) : null;
                    var transport = order.TransportID.HasValue ? _transportRepository.GetTransportById(order.TransportID.Value) : null;

                    // Использование пустых строк, если объекты null
                    string clientName = client != null ? $"{client.FirstName} {client.LastName}" : "Неизвестно";
                    string cargoTypeName = cargoType != null ? cargoType.TypeName : "Неизвестно";
                    string courierName = courier != null ? $"{courier.FirstName} {courier.LastName}" : "Неизвестно";
                    string transportInfo = transport != null ? $"{transport.Type}, Номер: {transport.LicensePlate}" : "Неизвестно";

                    // Создание документа PDF
                    PdfDocument document = new PdfDocument();
                    document.Info.Title = $"Договор Заказа #{order.OrderID}";

                    PdfPage page = document.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont fontTitle = new XFont("Verdana", 20, XFontStyle.Bold);
                    XFont fontBody = new XFont("Verdana", 12, XFontStyle.Regular);

                    // Добавление заголовка
                    gfx.DrawString("Договор на оказание услуг", fontTitle, XBrushes.Black,
                        new XRect(0, 40, page.Width, 40), XStringFormats.TopCenter);

                    // Добавление информации о заказе
                    double yPoint = 100;
                    gfx.DrawString($"Номер заказа: {order.OrderID}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    // Используем имена вместо ID или "Неизвестно" если null
                    gfx.DrawString($"Клиент: {clientName}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    gfx.DrawString($"Дата заказа: {order.OrderDate.ToShortDateString()}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    gfx.DrawString($"Описание груза: {order.CargoDescription}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    gfx.DrawString($"Тип груза: {cargoTypeName}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    gfx.DrawString($"Базовая цена: {order.BasePrice:C}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    gfx.DrawString($"Курьер: {courierName}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 30;

                    gfx.DrawString($"Транспорт: {transportInfo}", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);
                    yPoint += 50;

                    // Добавление подписи
                    gfx.DrawString("Подпись клиента: ____________________", fontBody, XBrushes.Black,
                        new XRect(40, yPoint, page.Width - 80, 20), XStringFormats.TopLeft);

                    // Сохранение документа
                    document.Save(filePath);

                    MessageBox.Show($"Договор успешно сохранен по адресу:\n{filePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось сохранить договор: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Пользователь отменил сохранение
                MessageBox.Show("Сохранение договора отменено.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
