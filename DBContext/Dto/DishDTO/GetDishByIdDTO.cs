using backendTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto.DishDTO
{
    public class GetDishByIdDTO
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public string? Image { get; set; }

        public bool Vegetarian { get; set; }

        public double? Rating { get; set; }

        public DishCategory Category { get; set; }
        public Guid Id { get; set; }
    }

}
