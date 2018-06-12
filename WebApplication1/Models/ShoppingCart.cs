using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    public class ShoppingCart
    {
        OrderCrude Db;
        private Order order;
        public double totalCash = 0;
        public List<OrderLine> orderLines { get; private set; }

        public ShoppingCart()
        {
            order = new Order();
            orderLines = new List<OrderLine>();
        }
        public ShoppingCart(User user)
        {
            order = new Order { UserId = user.id};
            orderLines = new List<OrderLine>();
        }

        public OrderLine existInCart(Product item)
        {
            if (orderLines.Where(line => line.ProductId == item.id).Where(line => line.OrderId == this.order.OrderId).FirstOrDefault() != null)
                return orderLines.Where(line => line.ProductId == item.id).Where(line => line.OrderId == this.order.OrderId).FirstOrDefault();
            else
                return null;
        }

        public void addToCart(Product item, int amount)
        {
            totalCash += item.price * amount;
            if (existInCart(item) != null)
            {
                existInCart(item).Amount += amount;
            }
            else
            {
                
                orderLines.Add(new OrderLine(order.OrderId, item.id, amount, item.price));
                
            }
        }

        public bool RemoveLine(int lineID)
        {
            return orderLines.Remove(orderLines[lineID]);
        }

        public void sendOrder()
        {
            Db.Create(order);
        }
    }
}