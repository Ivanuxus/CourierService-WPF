using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierService.Models.Entities;
using CourierService.Models.Repository;

namespace CourierService.Models.Interfaces
{

    public interface IRepository<T> where T : class
    {
        List<T> GetList();
        T GetItem(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);


    }
}
