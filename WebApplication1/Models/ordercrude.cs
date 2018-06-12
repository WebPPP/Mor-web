using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Controllers;
using WebApplication1.Models.interfaces;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace WebApplication1.Models
{
    public class OrderCrude : IDbManager<Order>
    {
        DbRepository<Order> newDb;
        public OrderCrude()
        {
            newDb = new DbRepository<Order>("Orders");
        }
        public void Create(Order item)
        {
            if (item != null)
                newDb.Collection.InsertOne(item);
        }

        public void Delete(string id)
        {
            var Filter = Builders<Order>.Filter.Eq(s => s.OrderId, ObjectId.Parse(id));
            newDb.Collection.FindOneAndDeleteAsync(Filter);
        }

        public Order find(string id)
        {
            var ProductId = new ObjectId(id);
            return newDb.Collection.AsQueryable<Order>().FirstOrDefault(a => a.OrderId == ProductId);
        }

        public List<Order> findAll()
        {
            return newDb.Collection.AsQueryable<Order>().ToList();
        }

        public void Update(Order NewItem)
        {
            throw new NotImplementedException();
        }

    }
}