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
    public class TransportRepository : ITransportRepository
    {
        private readonly CourierServiceContext _context;

        public TransportRepository(CourierServiceContext context)
        {
            _context = context;
        }

        public async Task<Transport> GetTransportByIdAsync(int id)
        {
            return await _context.Transports.FindAsync(id);
        }

        public async Task<IEnumerable<Transport>> GetAllTransportsAsync()
        {
            return await _context.Transports.ToListAsync();
        }

        public async Task AddTransportAsync(Transport transport)
        {
            await _context.Transports.AddAsync(transport);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransportAsync(Transport transport)
        {
            _context.Transports.Update(transport);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransportAsync(int id)
        {
            var transport = await GetTransportByIdAsync(id);
            if (transport != null)
            {
                _context.Transports.Remove(transport);
                await _context.SaveChangesAsync();
            }
        }
        public IEnumerable<Transport> GetAllTransports()
        {
            return _context.Transports.ToList();
        }

        public Transport GetTransportById(int id)
        {
            return _context.Transports.FirstOrDefault(t => t.TransportID == id);
        }
        public void AddTransport(Transport transport)
        {
            _context.Transports.Add(transport);
            _context.SaveChanges();
        }

        public int GetMaxTransportId()
        {
            return _context.Transports.Any() ? _context.Transports.Max(t => t.TransportID) : 0;
        }
    }


}
