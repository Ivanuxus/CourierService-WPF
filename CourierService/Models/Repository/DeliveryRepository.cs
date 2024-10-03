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
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly CourierServiceContext _context;

        public DeliveryRepository(CourierServiceContext context)
        {
            _context = context;
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int id)
        {
            return await _context.Deliveries.FindAsync(id);
        }

        public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public async Task AddDeliveryAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeliveryAsync(int id)
        {
            var delivery = await GetDeliveryByIdAsync(id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
        public void AddDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
        }
        public void DeleteDeliveryByOrderId(int orderId)
        {
            using (var context = new CourierServiceContext())
            {
                var delivery = context.Deliveries.FirstOrDefault(d => d.OrderID == orderId);
                if (delivery != null)
                {
                    context.Deliveries.Remove(delivery);
                    context.SaveChanges();
                }
            }
        }

    }


}
