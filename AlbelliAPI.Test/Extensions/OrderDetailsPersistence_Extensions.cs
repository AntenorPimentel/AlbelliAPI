using AlbelliAPI.Business.Models;
using AlbelliAPI.Data.DTOs;
using Bogus;
using System.Collections.Generic;

namespace AlbelliAPI.Test.Extensions
{
    public static class OrderDetailsPersistence_Extensions
    {
        public static OrderDetailsPersistence Build(this OrderDetailsPersistence instance)
        {
            instance = new Faker<OrderDetailsPersistence>()
                            .RuleFor(o => o.OrderId, new Faker().IndexFaker)
                            .RuleFor(o => o.Products, 
                                    new Faker<ProductPersitence>()
                                        .RuleFor(p => p.ProductType, new Faker().PickRandom<Enums.ProductTypes>())
                                        .RuleFor(p => p.Quantity, new Faker().Random.Number(min: 1, max: 10))
                                        .Generate(10))
                            .RuleFor(o => o.RequiredBinWidth, new Faker().Random.Number(min: 10, max: 9999))
                            .Generate();

            return instance;
        }

        public static OrderDetailsPersistence WithNull(this OrderDetailsPersistence instance)
        {
            instance = null;

            return instance;
        }

        public static OrderDetailsPersistence WithProducts(this OrderDetailsPersistence instance, Enums.ProductTypes productTypes, int quantity)
        {
            instance = new OrderDetailsPersistence
            {
                OrderId = 1,
                Products = new List<ProductPersitence>()
                {
                    new ProductPersitence() { ProductType = productTypes, Quantity = quantity }
                },
                RequiredBinWidth = 0.0
            };

            return instance;
        }

        public static OrderDetailsPersistence WithAllProducts(this OrderDetailsPersistence instance, int quantityOfMug)
        {
            instance = new OrderDetailsPersistence
            {
                OrderId = 1,
                Products = new List<ProductPersitence>()
                {
                    new ProductPersitence() { ProductType = Enums.ProductTypes.PhotoBook, Quantity = 23 },
                    new ProductPersitence() { ProductType = Enums.ProductTypes.Calendar, Quantity = 4 },
                    new ProductPersitence() { ProductType = Enums.ProductTypes.Canvas, Quantity = 6 },
                    new ProductPersitence() { ProductType = Enums.ProductTypes.SetOfGreetingCards, Quantity = 13 },
                    new ProductPersitence() { ProductType = Enums.ProductTypes.Mug, Quantity = quantityOfMug }
                },
                RequiredBinWidth = 0.0
            };

            return instance;
        }
    }
}