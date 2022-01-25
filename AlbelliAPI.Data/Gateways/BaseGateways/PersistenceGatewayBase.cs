using AlbelliAPI.Data.DTOs;
using System.Threading.Tasks;

namespace AlbelliAPI.Data.Gateways
{
    public class PersistenceGatewayBase
    {
        protected OrderDetailsRepository _orderDetailsRepository;

        public PersistenceGatewayBase(OrderDetailsRepository orderDetailsRepository) 
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<OrderDetailsPersistence> Get<T>(int parameter) 
        {
            return await Task.FromResult(_orderDetailsRepository.GetOrderByID(parameter));
        }

        public async Task Update<T>(OrderDetailsPersistence orderDetailsPersistence)
        {
            await Task.FromResult(_orderDetailsRepository.InsertOrder(orderDetailsPersistence));
        }
    }
}