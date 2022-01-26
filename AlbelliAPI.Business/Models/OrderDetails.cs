using System.Collections.Generic;

namespace AlbelliAPI.Business.Models
{
    public class OrderDetails
    {
        public IEnumerable<ProductDetails> Products { get; set; }
        public double RequiredBinWidth { get; set; }
    }

    public class ProductDetails
    {
        public string ProductType { get; set; }
        public int Quantity { get; set; }
    }
}