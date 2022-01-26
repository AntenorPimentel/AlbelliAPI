using AlbelliAPI.Business.Models;
using AlbelliAPI.Data.DTOs;
using AlbelliAPI.Data.Gateways;
using AlbelliAPI.Data.Models;
using AutoMapper;
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
            var orderDetailsPersistenceCalculated = new OrderDetailsPersistence();

            if (IsValidOrderID(orderID))
            {
                var orderDetailsPersistence = await _orderPersistence.GetOrderDetails(orderID);

                if (orderDetailsPersistence != null)
                    orderDetailsPersistenceCalculated = CalculateRequiredBinWidth(orderDetailsPersistence);
            }

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