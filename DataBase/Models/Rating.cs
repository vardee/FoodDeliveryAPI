using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Models
{
    public class Rating
    {
        [Key]
        public Guid DishId { get; set; }
        [Key]
        public Guid UserId { get; set; }

        public double? ratingValue { get; set; }

    }
}
