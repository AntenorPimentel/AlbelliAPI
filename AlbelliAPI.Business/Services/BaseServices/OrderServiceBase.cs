using AlbelliAPI.Business.Models;
using AlbelliAPI.Business.Validator;
using AlbelliAPI.Data.DTOs;
using AlbelliAPI.Data.Models;
using System;
using System.Linq;

namespace AlbelliAPI.Business.Services
{
    public class OrderServiceBase : ServiceBase
    {
        protected static OrderDetailsPersistence CalculateRequiredBinWidth(OrderDetailsPersistence orderDetailsPersistence)
        {
            double requiredBinWidth = 0.0;

            var productsGroupBy = orderDetailsPersistence.Products
                .GroupBy(obj => new { obj.ProductType })
                .OrderByDescending(obj => obj.Key.ProductType);

            foreach (var products in productsGroupBy.ToList())
            {
                requiredBinWidth += CalculateWidth(products.Key.ProductType, products.ToList().FirstOrDefault().Quantity);
            }

            orderDetailsPersistence.RequiredBinWidth = requiredBinWidth;

            return orderDetailsPersistence;
        }

        private static double CalculateWidth(Enums.ProductTypes productType, int quantity)
        {
            double requiredBinWidth = 0.0;

            switch (productType)
            {
                case Enums.ProductTypes.PhotoBook:
                    requiredBinWidth = quantity * 19;
                    break;
                case Enums.ProductTypes.Calendar:
                    requiredBinWidth = quantity * 10;
                    break;
                case Enums.ProductTypes.Canvas:
                    requiredBinWidth = quantity * 16;
                    break;
                case Enums.ProductTypes.SetOfGreetingCards:
                    requiredBinWidth = quantity * 4.7;
                    break;
                case Enums.ProductTypes.Mug:
                    requiredBinWidth = quantity % 4 == 0 ? quantity / 4 * 94 : (quantity / 4) * 94 + 94;
                    break;
                default:
                    break;
            }
            return requiredBinWidth;
        }

        protected static bool IsValidOrderPlaced(OrderPlaced orderPlaced)
        {
            var validator = new OrderPlacedValidator().Validate(orderPlaced);
            return validator.IsValid ? validator.IsValid : throw new ArgumentException(validator.ToString(", "));
        }

        protected static bool IsValidOrderID(int orderId) => orderId > 0;
    }
}