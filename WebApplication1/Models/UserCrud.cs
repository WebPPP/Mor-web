using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication1.Controllers;
using WebApplication1.Models.interfaces;

namespace WebApplication1.Models
{
    public class UserCrud: IDbManager<User>
    {
        //MongoClient Client;
        
        //IMongoCollection<User> Collection;
        public DbRepository<User> NewDb;

        public UserCrud()
        {
            //Client = new MongoClient(ConfigurationManager.AppSettings["mongoDbHost"]);
            //var Db = Client.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
            //Collection = Db.GetCollection<User>("Users");
            NewDb = new DbRepository<User>("Users");
        }
        public List<User> findAll()
        {
            return NewDb.Collection.AsQueryable<User>().ToList();
        }
        public User find(string id)
        {
            var UserId = new ObjectId(id);
            return NewDb.Collection.AsQueryable<User>().SingleOrDefault(a=>a.id == UserId);
            
        }
        public User findByName(string Username)
        {
            return NewDb.Collection.AsQueryable<User>().FirstOrDefault(a => a.name == Username);
        }
        public void Create(User NewItem)
        {
            NewDb.Collection.InsertOne(NewItem);
        }
        public void Update(User Newitem)
        {   
            var Filter = Builders<User>.Filter.Eq(s => s.id, Newitem.id);
            NewDb.Collection.FindOneAndReplace(Filter, Newitem);
        }
        public void Delete(string id)
        {

            var Filter = Builders<User>.Filter.Eq(s => s.id, ObjectId.Parse(id));
            NewDb.Collection.FindOneAndDeleteAsync(Filter);
        }

    }
}