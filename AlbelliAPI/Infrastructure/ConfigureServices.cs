using AlbelliAPI.Business;
using AlbelliAPI.Business.Services;
using AlbelliAPI.Data.Gateways;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlbelliAPI.Infrastructure
{
    public class ConfigureServices
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var orderDetailsRepository = new OrderDetailsRepository { };

            //Services
            services.AddTransient<IOrderService, OrderService>();

            //PersistenceGateways
            services.AddTransient<IOrderPersistenceGateway>(ctx => new OrderPersistenceGateway(orderDetailsRepository));
        }
    }
}