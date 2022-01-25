using AlbelliAPI.Business.Models;
using AlbelliAPI.Data.DTOs;
using AlbelliAPI.Data.Gateways;
using AlbelliAPI.Data.Models;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace AlbelliAPI.Business.Services
{
    public class OrderService : OrderServiceBase, IOrderService
    {
        private readonly IOrderPersistenceGateway _orderPersistence;

        public OrderService(IOrderPersistenceGateway orderPersistenceGateway, IMapper mapper)
        {
            _orderPersistence = orderPersistenceGateway;
            _mapper = mapper;
        }

        public async Task<OrderDetails> GetOrderDetails(int orderID)
        {
            if (!IsValidOrderID(orderID))
                throw new ArgumentException("OrderId is invalid");
            
            var orderDetailsPersistence = await _orderPersistence.GetOrderDetails(orderID);
            var orderDetailsPersistenceCalculated = new OrderDetailsPersistence();

            if (orderDetailsPersistence == null)
                _logger.Information("No Order Placed with OrderId: {orderID}", orderID);
            else
                orderDetailsPersistenceCalculated = CalculateRequiredBinWidth(orderDetailsPersistence);

            return _mapper.Map<OrderDetailsPersistence, OrderDetails>(orderDetailsPersistenceCalculated);
        }

        public async Task SubmitOrder(OrderPlaced orderPlaced)
        {
            if (IsValidOrderPlaced(orderPlaced))
            {
                var orderPersistence = _mapper.Map<OrderPlaced, OrderDetailsPersistence>(orderPlaced);
                await _orderPersistence.SubmitOrder(orderPersistence);
            }
        }
    }
}