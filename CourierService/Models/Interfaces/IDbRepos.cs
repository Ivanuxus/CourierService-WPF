
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Models.Interfaces
{
    public interface IDbRepos
    {
        IOrderRepository Orders { get; }
        ICargoTypeRepository CargoTypes { get; } // Изменено на ICargoTypeRepository
        IClientRepository Clients { get; }
        ICourierRepository Couriers { get; }
        IDeliveryRepository Deliveries { get; }
        ITransportRepository Transports { get; }
    }
}