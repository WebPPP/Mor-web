using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace WebApplication1.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId id { get; set; }
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("price")]
        public int price { get; set; }
        [BsonElement("StockAmount")]
        public int StockAmount { get; set; }
        [BsonElement("ImgUrl")]
        public string ImgUrl { get; set; }

    }

}