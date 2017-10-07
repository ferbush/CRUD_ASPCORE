using ASPCoreCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreCRUD.iRepository
{
    public interface perfiliRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll();
        void Delete(string id);
        void Insert(T item);
        T FindByID(string id);
        void Update(T item);
    }
}
