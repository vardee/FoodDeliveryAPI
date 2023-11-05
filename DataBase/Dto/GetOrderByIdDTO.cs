using backendTask.DataBase.Models;
using backendTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto
{
    public class GetOrderByIdDTO
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly deliveryTime { get; set; }
        public DateTime orderTime { get; set; }
        public OrderStatus status { get; set; }
        public double price { get; set; }
        public List<GetUserCartResponseDTO> dishes { get; set; }
        public string adress { get; set; }
    }
}
