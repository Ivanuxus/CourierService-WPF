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
    public class CourierRepository : ICourierRepository
    {
        private readonly CourierServiceContext _context;

        public CourierRepository(CourierServiceContext context)
        {
            _context = context;
        }

        public async Task<Courier> GetCourierByIdAsync(int id)
        {
            return await _context.Couriers.FindAsync(id);
        }

        public async Task<IEnumerable<Courier>> GetAllCouriersAsync()
        {
            return await _context.Couriers.ToListAsync();
        }

        public async Task AddCourierAsync(Courier courier)
        {
            await _context.Couriers.AddAsync(courier);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourierAsync(Courier courier)
        {
            _context.Couriers.Update(courier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourierAsync(int id)
        {
            var courier = await GetCourierByIdAsync(id);
            if (courier != null)
            {
                _context.Couriers.Remove(courier);
                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<Courier> GetAllCouriers()
        {
            return _context.Couriers.ToList();
        }

        public Courier GetCourierById(int id)
        {
            return _context.Couriers.FirstOrDefault(c => c.CourierID == id);
        }
        public void AddCourier(Courier courier)
        {
            _context.Couriers.Add(courier);
            _context.SaveChanges();
        }

        public int GetMaxCourierId()
        {
            return _context.Couriers.Any() ? _context.Couriers.Max(cr => cr.CourierID) : 0;
        }
    }


}
