using AlbelliAPI.Business.Models;
using AlbelliAPI.Business.Services;
using AlbelliAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlbelliAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : APIControllerController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("submit")]
        public async Task<ActionResult> SubmitOrder([FromBody] OrderPlaced orderPlaced) =>
            !ModelState.IsValid
                ? BadRequest(ModelState)
                : await ExecutePost(() => _orderService.SubmitOrder(orderPlaced), "Error submitting Order").ConfigureAwait(false);


        [HttpGet]
        [Route("details")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetails(int orderID) =>
            !ModelState.IsValid
                ? BadRequest(ModelState)
                : await ExecuteGet(() => _orderService.GetOrderDetails(orderID), $"Error retrieving Order Details for OrderId: {orderID}");
    }
}