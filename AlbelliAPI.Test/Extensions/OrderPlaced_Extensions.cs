using AlbelliAPI.Business.Models;
using AlbelliAPI.Data.Models;
using Bogus;
using System.Linq;

namespace AlbelliAPI.Test.Extensions
{
    public static class OrderPlaced_Extensions
    {
        public static OrderPlaced Build(this OrderPlaced instance)
        {
            instance = new Faker<OrderPlaced>()
                .RuleFor(o => o.OrderId, 1)
                .RuleFor(o => o.Products,
                        new Faker<ProductDetails>()
                            .RuleFor(p => p.ProductType, new Faker().PickRandom<Enums.ProductTypes>().ToString())
                            .RuleFor(p => p.Quantity, new Faker().Random.Number(min: 1, max: 10))
                            .Generate(10))
                .Generate();

            return instance;
        }

        public static OrderPlaced WithInvalidOrderId(this OrderPlaced instance)
        {
            instance.OrderId = 0;

            return instance;
        }

        public static OrderPlaced WithoutProductType(this OrderPlaced instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = string.Empty; 

            return instance;
        }

        public static OrderPlaced WithNullProductType(this OrderPlaced instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = null;

            return instance;
        }

        public static OrderPlaced WithInvalidProductType(this OrderPlaced instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = "pencil";

            return instance;
        }

        public static OrderPlaced WithoutQuantity(this OrderPlaced instance)
        {
            instance.Products.ToList().FirstOrDefault().Quantity = 0;

            return instance;
        }
    }
}
