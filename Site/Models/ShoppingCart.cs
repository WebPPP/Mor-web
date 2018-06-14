using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class ShoppingCart
    {
        private mordbEntities db = new mordbEntities();
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
            order = new Order(user);
            orderLines = new List<OrderLine>();
        }

        public OrderLine existInCart(Item item)
        {
            if (orderLines.Where(line => line.ItemID == item.ItemID).Where(line => line.OrderID == this.order.OrderID).FirstOrDefault() != null)
                return orderLines.Where(line => line.ItemID == item.ItemID).Where(line => line.OrderID == this.order.OrderID).FirstOrDefault();
            else
                return null;
        }

        public void addToCart(Item item, int amount)
        {
            totalCash += item.Price * amount;
            if (existInCart(item) != null)
            {
                existInCart(item).Amount += amount;
            }
            else
            {
                orderLines.Add(new OrderLine(order.OrderID, item.ItemID, amount, item.Price));
                orderLines.Last().Item = db.Items.Find(orderLines.Last().ItemID);
            }
        }

        public bool RemoveLine(int lineID)
        {
            return orderLines.Remove(orderLines[lineID]);
        }

        public void sendOrder()
        {
            foreach (var line in orderLines)
            {
                db.OrderLines.Add(line);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}