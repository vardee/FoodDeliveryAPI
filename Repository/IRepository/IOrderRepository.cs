using backendTask.DataBase.Dto;

namespace backendTask.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<GetOrderByIdDTO> getOrderById(string token, Guid Id);
        public Task createOrderDTO(string token, CreateOrderDTO createOrderDTO);
    }
}
