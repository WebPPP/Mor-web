using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class OrderLine
    {
        public OrderLine( ObjectId OrderId, ObjectId ProductId, int Amount, int Total)
        {
            this.OrderId =OrderId;
            this.Amount = Amount;
            this.ProductId = ProductId;
            this.Total = Total;
        }
        
        [BsonId]
        public ObjectId OrderId;
        [BsonElement("ProductId")]
        public ObjectId ProductId { get; set; }
        [BsonElement("Amount")]
        public int Amount { get; set; }
        [BsonElement("Total")]
        public int Total { get; set; }
        [BsonElement("Product")]
        public Product Product { get; set; }

 
    }
}