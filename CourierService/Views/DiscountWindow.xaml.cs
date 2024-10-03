using CourierService.Models.Interfaces;
using System;
using System.Linq;
using System.Windows;

namespace CourierService.Views
{
    /// <summary>
    /// Interaction logic for DiscountWindow.xaml
    /// </summary>
    public partial class DiscountWindow : Window
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository; // Добавлено

        public DiscountWindow(IOrderRepository orderRepository, IClientRepository clientRepository) // Добавлено
        {
            InitializeComponent();
            _orderRepository = orderRepository;
            _clientRepository = clientRepository; // Добавлено
            LoadDiscounts();
        }

        private void LoadDiscounts()
        {
            var discounts = _orderRepository.GetClientDiscounts();

            foreach (var discount in discounts)
            {
                int clientId = discount.Key;
                var discountValue = discount.Value; // Получаем значение

                // Получаем информацию о клиенте
                var client = _clientRepository.GetClientById(clientId);
                string clientName = client != null ? $"{client.FirstName} {client.LastName}" : "Неизвестный клиент"; // Получаем имя клиента

                // Деструктуризация с явным указанием
                decimal totalAmount = discountValue.totalAmount;
                decimal discountAmount = discountValue.discountAmount;
                decimal totalAfterDiscount = discountValue.totalAfterDiscount;

                // Добавляем информацию в ListBox
                UsersListBox.Items.Add($"{clientName}: Итоговая сумма: {totalAmount:C}, Скидка: {discountAmount:C}, Сумма со скидкой: {totalAfterDiscount:C}");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
