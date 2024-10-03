using CourierService.Models.Entities;
using CourierService.Models.Interfaces;

namespace CourierService.Models.Repository
{
    public class DbRepos : IDbRepos
    {
        private readonly CourierServiceContext _context;

        public DbRepos(CourierServiceContext context)
        {
            _context = context;
        }

        // Реализация методов интерфейса IDbRepos
        public IOrderRepository Orders => new OrderRepository(_context);
        public ICargoTypeRepository CargoTypes => new CargoTypeRepository(_context);


        public IClientRepository Clients => new ClientRepository(_context);
        public ICourierRepository Couriers => new CourierRepository(_context);
        public IDeliveryRepository Deliveries => new DeliveryRepository(_context);
        public ITransportRepository Transports => new TransportRepository(_context);
    }
}
