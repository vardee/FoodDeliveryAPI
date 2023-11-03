using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Models
{
    public class Cart
    {
        [Key]
        public Guid DishId { get; set; }
        [Key]
        public Guid UserId { get; set; }
        public int Amount { get; set; }
    }
}
