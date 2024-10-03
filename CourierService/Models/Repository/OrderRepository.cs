using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.Arm;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourierService.Models.DTO;

namespace CourierService.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CourierServiceContext _context;

        public OrderRepository(CourierServiceContext context)
        {
            _context = context;
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public IEnumerable<Order> GetAllOrders() // Синхронный метод
        {
            return _context.Orders.ToList();
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        public IEnumerable<Client> GetClientsWithMoreThanTwoOrders()
        {
            return _context.Orders
                .GroupBy(o => o.ClientID)
                .Where(g => g.Count() > 2)
                .Select(g => new Client { ClientID = g.Key }) // Предполагается, что Client имеет только ClientID
                .ToList();
        }
        public decimal GetTotalAmountForUser(int clientId)
        {
            return _context.Orders
                .Where(o => o.ClientID == clientId)
                .Sum(o => o.BasePrice);
        }

        public Dictionary<int, (decimal totalAmount, decimal discountAmount, decimal totalAfterDiscount)> GetClientDiscounts()
        {
            var discounts = new Dictionary<int, (decimal totalAmount, decimal discountAmount, decimal totalAfterDiscount)>();

            var clientsWithDiscount = GetClientsWithMoreThanTwoOrders();

            foreach (var client in clientsWithDiscount)
            {
                decimal totalAmount = GetTotalAmountForUser(client.ClientID);
                decimal discountAmount = totalAmount * 0.1m; // 10% скидка
                decimal totalAfterDiscount = totalAmount - discountAmount;

                discounts[client.ClientID] = (totalAmount, discountAmount, totalAfterDiscount);
            }

            return discounts;
        }
        public int GetMaxOrderId()
        {
            // Возвращаем максимальный OrderID, или 0 если заказов нет
            return _context.Orders.Any() ? _context.Orders.Max(o => o.OrderID) : 0;
        }





        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}