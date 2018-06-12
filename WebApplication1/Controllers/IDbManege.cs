using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    interface IDbManege<T>
    {
        void Create(T item);
        List<T> findAll();
        T find(string id);
        void Delete(string id);
        void Update(T NewItem);
    }
}
