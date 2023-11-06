using backendTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto
{
    public class GetListOrdersDTO
    {
        public Guid Id { get; set; }
        public DateTime deliveryTime { get; set; }
        public DateTime orderTime { get; set; }
        public OrderStatus status { get; set; }
        public int price { get; set; }
    }
}
