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
                        new Faker<ProductPlaced>()
                            .RuleFor(p => p.ProductType, (int) new Faker().PickRandom<Enums.ProductTypes>())
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
            instance.Products.ToList().FirstOrDefault().ProductType = 0; 

            return instance;
        }

        public static OrderPlaced WithInvalidProductType(this OrderPlaced instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = 6;

            return instance;
        }

        public static OrderPlaced WithoutQuantity(this OrderPlaced instance)
        {
            instance.Products.ToList().FirstOrDefault().Quantity = 0;

            return instance;
        }
    }
}
