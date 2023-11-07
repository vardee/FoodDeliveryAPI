using backendTask.DataBase.Models;

namespace backendTask.DataBase.Dto.DishDTO
{
    public class GetDishResponseDTO
    {
        public List<Dish> Dishes { get; set; }
        public PageInformationDTO PageInformation { get; set; }
    }
}
