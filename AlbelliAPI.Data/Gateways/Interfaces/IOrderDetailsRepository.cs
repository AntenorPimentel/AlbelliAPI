using AlbelliAPI.Data.DTOs;

namespace AlbelliAPI.Data.Gateways.Interfaces
{
    public interface IOrderDetailsRepository
    {
        OrderDetailsPersistence GetOrderByID(int studentId);
        bool InsertOrder(OrderDetailsPersistence orderDetailsPersistence);
    }
}