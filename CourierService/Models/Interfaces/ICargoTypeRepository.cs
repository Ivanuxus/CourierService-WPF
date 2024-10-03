
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Entities;

namespace CourierService.Models.Interfaces
{
    public interface ICargoTypeRepository
    {
        Task<CargoType> GetCargoTypeByIdAsync(int id);
        Task<IEnumerable<CargoType>> GetAllCargoTypesAsync();
        IEnumerable<CargoType> GetAllCargoTypes();
        int GetMaxCargoTypeId();
        void AddCargoType(CargoType cargoType);
        CargoType GetCargoTypeById(int id);

    }
}
