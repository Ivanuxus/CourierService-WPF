
using Ninject.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Interfaces;
using CourierService.Models.Repository;
using CourierService.Views;

namespace CourierService.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            // Привязки для всех репозиториев
            Bind<ICargoTypeRepository>().To<CargoTypeRepository>();
            Bind<IClientRepository>().To<ClientRepository>();
            Bind<ICourierRepository>().To<CourierRepository>();
            Bind<ITransportRepository>().To<TransportRepository>();
            Bind<IOrderRepository>().To<OrderRepository>();
            Bind<IDeliveryRepository>().To<DeliveryRepository>();
            Bind<OrdersViewModel>().ToSelf();
            Bind<OrdersView>().ToSelf();
        }
    }
}
