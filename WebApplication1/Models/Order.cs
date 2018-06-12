using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Order
    {

        [BsonId]
        public ObjectId OrderId { get; set; }
        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }
        [BsonElement("Lines")]
        public List<OrderLine> Lines { get; set; }
        [BsonElement("Totalprice")]
        public int TotalPrice { get; set; }

    }
}