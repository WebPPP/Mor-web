using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.interfaces
{
    interface IDbManager<T>
    {
        void Create(T item);
        List<T> findAll();
        T find(string id);
        void Delete(string id);
        void Update(T NewItem);
    }
}
