using AlbelliAPI.Data.DTOs;
using System.Threading.Tasks;

namespace AlbelliAPI.Data.Gateways
{
    public class OrderPersistenceGateway : PersistenceGatewayBase, IOrderPersistenceGateway
    {
        public OrderPersistenceGateway(OrderDetailsRepository orderDetailsRepository) : base (orderDetailsRepository) { }

        public async Task<OrderDetailsPersistence> GetOrderDetails(int orderID) => 
            await Get<OrderDetailsPersistence>(orderID);

        public async Task SubmitOrder(OrderDetailsPersistence orderPlaced) =>
            await Update<OrderDetailsPersistence>(orderPlaced);
    }
}