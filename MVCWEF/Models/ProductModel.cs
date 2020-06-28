using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWEF.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}