
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Entities;

namespace CourierService.Models.Interfaces
{
    public interface ICourierRepository
    {
        Task<IEnumerable<Courier>> GetAllCouriersAsync();
        Task<Courier> GetCourierByIdAsync(int id);
        IEnumerable<Courier> GetAllCouriers();
        Courier GetCourierById(int id);

        Task AddCourierAsync(Courier courier);
        Task UpdateCourierAsync(Courier courier);
        Task DeleteCourierAsync(int id);
        int GetMaxCourierId();
        void AddCourier(Courier courier);

    }

}
