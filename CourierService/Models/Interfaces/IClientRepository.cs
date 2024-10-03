
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Entities;

namespace CourierService.Models.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClientByIdAsync(int id);
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int id);  // Добавляем метод для получения клиента по ID
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        int GetMaxClientId();
        void AddClient(Client client);
    }

}
