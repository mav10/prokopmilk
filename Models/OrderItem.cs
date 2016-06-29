using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }

        //Ссылка на заказ
        public Order Order { get; set; }
        public int Tovar_ID { get; set; }
       
    }
}