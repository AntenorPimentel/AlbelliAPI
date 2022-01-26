using AlbelliAPI.Business.Models;
using System.Collections.Generic;

namespace AlbelliAPI.Data.Models
{
    public class OrderPlaced 
    {
        public int OrderId { get; set; }
        public IEnumerable<ProductDetails> Products { get; set; }
    }

    public class OrderDetails 
    {
        public IEnumerable<ProductDetails> Products { get; set; }
        public double RequiredBinWidth { get; set; }
    }
}