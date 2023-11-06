using backendTask.DataBase.Dto.OrderDTO;

namespace backendTask.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<GetOrderByIdDTO> getOrderById(string token, Guid Id);
        public Task<List<GetListOrdersDTO>> getListOrdersDTO(string token);
        public Task createOrderDTO(string token, CreateOrderDTO createOrderDTO);
        public Task<ConfirmOrderStatusDTO> confirmOrderStatus(string token,Guid Id);
    }
}
