using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrder.Interfaces.Abstract
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? objectId);
        void Add(T objectT);
        void Edit(T objectT);
        bool Remove(int? objectId);
    }
}
