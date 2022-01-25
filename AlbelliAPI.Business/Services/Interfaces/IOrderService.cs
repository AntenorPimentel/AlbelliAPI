using AlbelliAPI.Business.Models;
using AlbelliAPI.Data.Models;
using System.Threading.Tasks;

namespace AlbelliAPI.Business.Services
{
    public interface IOrderService
    {
        Task<OrderDetails> GetOrderDetails(int orderID);
        Task SubmitOrder(OrderPlaced orderPlaced);
    }
}