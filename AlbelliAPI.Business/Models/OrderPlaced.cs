using System.Collections.Generic;

namespace AlbelliAPI.Data.Models
{
    public class OrderPlaced
    {
        public int OrderId { get; set; }
        public IEnumerable<ProductPlaced> Products { get; set; }
    }

    public class ProductPlaced
    {
        public string ProductType { get; set; }

        public int Quantity { get; set; }
    }
}