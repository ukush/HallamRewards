using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public string description { get; set; }
    }
}
