using backendTask.DataBase.Dto.CartDTO;
using backendTask.DataBase.Models;
using backendTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto.OrderDTO
{
    public class GetOrderByIdDTO
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime deliveryTime { get; set; }
        public DateTime orderTime { get; set; }
        public OrderStatus status { get; set; }
        public int price { get; set; }
        public List<GetUserCartResponseDTO> dishes { get; set; }
        public string address { get; set; }
    }
}
