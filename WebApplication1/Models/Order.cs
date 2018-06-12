using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Order
    {
        public ObjectId OrderId { get; set; }
        public ObjectId CustomerId { get; set; }
        public ObjectId[] ProductId { get; set; }
        public int TotalPrice { get; set; }

    }
}