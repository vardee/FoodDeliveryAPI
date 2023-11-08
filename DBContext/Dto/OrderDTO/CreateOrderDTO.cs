using backendTask.InformationHelps;

namespace backendTask.DataBase.Dto.OrderDTO
{
    public class CreateOrderDTO
    {
        [MinDeliveryTime(60, ErrorMessage = "Доставка должна быть не менее чем через 60 минут.")]
        public DateTime deliveryTime { get; set; }
        public Guid address { get; set; }
    }
}
