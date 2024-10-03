using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.Arm;
using CourierService.Models.Entities;
using CourierService.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Models.Repository
{
    public class CargoTypeRepository : ICargoTypeRepository
    {
        private readonly CourierServiceContext _context;

        public CargoTypeRepository(CourierServiceContext context)
        {
            _context = context;
        }

        public async Task<CargoType> GetCargoTypeByIdAsync(int id)
        {
            return await _context.CargoTypes.FindAsync(id);
        }

        public async Task<IEnumerable<CargoType>> GetAllCargoTypesAsync()
        {
            return await _context.CargoTypes.ToListAsync();
        }

        // Методы CRUD удалены
        public IEnumerable<CargoType> GetAllCargoTypes()
        {
            return _context.CargoTypes.ToList();
        }
        public void AddCargoType(CargoType cargoType)
        {
            _context.CargoTypes.Add(cargoType);
            _context.SaveChanges();
        }

        public int GetMaxCargoTypeId()
        {
            return _context.CargoTypes.Any() ? _context.CargoTypes.Max(ct => ct.CargoTypeID) : 0;
        }
        public CargoType GetCargoTypeById(int id)
        {
            // Assuming CargoTypes is your DbSet in the CourierServiceContext
            return _context.CargoTypes.FirstOrDefault(cargoType => cargoType.CargoTypeID == id);
        }
    }
}
