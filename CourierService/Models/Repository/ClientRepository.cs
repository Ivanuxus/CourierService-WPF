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
    public class ClientRepository : IClientRepository
    {
        private readonly CourierServiceContext _context;

        public ClientRepository(CourierServiceContext context)
        {
            _context = context;
        }
        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }
        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public int GetMaxClientId()
        {
            return _context.Clients.Any() ? _context.Clients.Max(c => c.ClientID) : 0;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task AddClientAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await GetClientByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
        public Client GetClientById(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.ClientID == id);
        }
    }

}
