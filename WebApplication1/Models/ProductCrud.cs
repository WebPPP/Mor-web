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
    public class ProductCrud : IDbManager<Product>
    {
        DbRepository<Product> newDb;
        public ProductCrud()
        {
            newDb = new DbRepository<Product>("Product"); 
        }
        public void Create(Product item)
        {
            if(item!=null)
            newDb.Collection.InsertOne(item);
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Product find(string id)
        {
            var ProductId = new ObjectId(id);
            return newDb.Collection.AsQueryable<Product>().FirstOrDefault(a=>a.id == ProductId);
        }
        public List<Product> findAll()
        {
            return newDb.Collection.AsQueryable<Product>().ToList();
        }

        public void Update(Product NewItem)
        {
            var Filter = Builders<Product>.Filter.Eq(s => s.id, NewItem.id);
            newDb.Collection.FindOneAndReplace(Filter, NewItem);
        }
    }
}