using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DbRepository<T>
    {
        public MongoClient Client { get; set; }
        public IMongoCollection<T> Collection { get; set; }

        public  DbRepository( string table)
        {
            Client = new MongoClient(ConfigurationManager.AppSettings["mongoDbHost"]);
            var Db = Client.GetDatabase(ConfigurationManager.AppSettings["mongoDbName"]);
            Collection = Db.GetCollection<T>(table);

        }
    }
}
