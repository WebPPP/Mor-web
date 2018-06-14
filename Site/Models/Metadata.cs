using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Site.Models
{
    [MetadataType(typeof(ItemValidation))]
    public partial class Item
    {
        //Partial Class compiled with code produced by VS designer
    }

    [Bind(Exclude = "ItemID")]
    public class ItemValidation
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public int Price { get; set; }
    }

    [MetadataType(typeof(RoleValidation))]
    public partial class Roles
    {

    }

    public class RoleValidation
    {
        [Display(Name = "Role")]
        public string Name { get; set; }
    }

    public partial class OrderLine
    {
        [DataType(DataType.Currency)]
        public float SubTotal { get; set; }

        public OrderLine()
        {
        }
        public OrderLine(int orderID, int itemID, int amount, int price)
        {
            this.OrderID = orderID;
            this.ItemID = itemID;
            this.Amount = amount;
            this.PricePerItem = price;
            this.SubTotal = price * amount;
        }

    }
    public class OrderLineValidation
    {
        [Display(Name = "Price"), DataType(DataType.Currency)]
        public int PricePerItem { get; set; }
    }

    public partial class Order
    {
        public Order(User user)
        {
            this.DateTime = DateTime.Now;
            this.UserID = user.UserID;
        }
    }
}