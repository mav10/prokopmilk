using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class AllModels
    {
        public AllModels() { }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tovar> Tovars { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<New> News { get; set; }
        public Tovar tovar { get; set; }
        public Customer customer { get; set; }
    }
}