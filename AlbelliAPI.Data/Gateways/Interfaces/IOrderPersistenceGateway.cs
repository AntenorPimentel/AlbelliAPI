using AlbelliAPI.Data.DTOs;
using System.Threading.Tasks;

namespace AlbelliAPI.Data.Gateways
{
    public interface IOrderPersistenceGateway
    {
        Task<OrderDetailsPersistence> GetOrderDetails(int orderID);
        Task SubmitOrder(OrderDetailsPersistence orderPlaced);
    }
}