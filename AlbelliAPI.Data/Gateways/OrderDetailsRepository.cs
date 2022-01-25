using AlbelliAPI.Data.DTOs;
using AlbelliAPI.Data.Gateways.Interfaces;
using System.Collections.Generic;

namespace AlbelliAPI.Data.Gateways
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly List<OrderDetailsPersistence> _orderDetailsPersistence;

        public OrderDetailsRepository() 
        {
            _orderDetailsPersistence = new List<OrderDetailsPersistence>();
        }

        public OrderDetailsPersistence GetOrderByID(int id) =>
            _orderDetailsPersistence.Find(x => x.OrderId == id);

        public bool InsertOrder(OrderDetailsPersistence orderDetailsPersistence)
        {
            var repositoryCount = _orderDetailsPersistence.Count;

            _orderDetailsPersistence.Add(orderDetailsPersistence);

            return repositoryCount < _orderDetailsPersistence.Count;
        }     
    }
}