using ASPCoreCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.iRepository
{
    public interface MenuiRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll();
        void Delete(int id);
        void Insert(T item);
        T FindByID(int id);
        void Update(T item);
    }
}
