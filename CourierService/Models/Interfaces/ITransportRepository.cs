
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Entities;

namespace CourierService.Models.Interfaces
{
    public interface ITransportRepository
    {
        Task<Transport> GetTransportByIdAsync(int id);
        Task<IEnumerable<Transport>> GetAllTransportsAsync();
        IEnumerable<Transport> GetAllTransports();
        Transport GetTransportById(int id);
        Task AddTransportAsync(Transport transport);
        Task UpdateTransportAsync(Transport transport);
        Task DeleteTransportAsync(int id);
        int GetMaxTransportId();
        void AddTransport(Transport transport);

    }

}
