using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreCRUD.Models;

namespace ASPCoreCRUD.iRepository
{
    public interface usuarioiRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll();
        void Delete(int id);
        void Insert(T item);
        T FindByID(int id);
        void Update(T item);
    }
}
