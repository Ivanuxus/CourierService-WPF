
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.DTO;
using CourierService.Models.Entities;
namespace CourierService.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(int orderId);
        IEnumerable<Order> GetAllOrders(); // Измените на синхронный метод
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
        Dictionary<int, (decimal totalAmount, decimal discountAmount, decimal totalAfterDiscount)> GetClientDiscounts();
        int GetMaxOrderId();

    }
}
