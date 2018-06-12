using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.interfaces;

namespace WebApplication1.Models
{
    public class ordercrude : IDbManager<Order>
    {
        public void Create(Order item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Order find(string id)
        {
            throw new NotImplementedException();
        }

        public List<Order> findAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Order NewItem)
        {
            throw new NotImplementedException();
        }
    }
}