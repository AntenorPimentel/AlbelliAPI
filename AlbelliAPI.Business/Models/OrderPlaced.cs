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
        public int ProductType { get; set; }
        public int Quantity { get; set; }

    }
}