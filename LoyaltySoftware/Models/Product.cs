﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Product
    {
        [Display (Name = "Product ID")]
        public int productId { get; set; }
        [Display(Name = "Image")]
        public string productImageSrc { get; set; }
        [Display (Name = "Name")]
        public string productName { get; set; }
        [Display (Name = "Price")]
        public string productPrice { get; set; }

        [Display(Name = "Description")]
        public string productDescription { get; set; }
    }
}
