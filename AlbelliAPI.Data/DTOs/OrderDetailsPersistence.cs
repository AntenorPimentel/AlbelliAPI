using AlbelliAPI.Business.Models;
using System.Collections.Generic;

namespace AlbelliAPI.Data.DTOs
{
    public class OrderDetailsPersistence 
    {
        public int OrderId { get; set; }
        public IEnumerable<ProductPersitence> Products { get; set; }
        public double RequiredBinWidth { get; set; }
    }

    public class ProductPersitence
    {
        public Enums.ProductTypes ProductType { get; set; }
        public int Quantity { get; set; }
    }
}