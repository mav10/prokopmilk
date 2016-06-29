using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Tovar
    {
        [Key]
        public int Tovar_ID { get; set; }
        public string Category{ get; set; }
        public float Volume { get; set; }
        public char Dimension { get; set; }
        public float Price { get; set; }
        public string HOT { get; set; }
        [Column(TypeName = "image")]
        public byte[] Pict { get; set; }

        //Ссылка на позиции заказа
        public virtual List<OrderItem> OrderItems { get; set; }


    }
}