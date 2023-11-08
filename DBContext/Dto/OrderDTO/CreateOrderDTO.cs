namespace backendTask.DataBase.Dto.OrderDTO
{
    public class CreateOrderDTO
    {
        public DateTime deliveryTime { get; set; }
        public Guid address { get; set; }
    }
}
