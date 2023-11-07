using backendTask.DataBase.Dto.DishDTO;
using backendTask.Enums;

namespace backendTask.Repository.IRepository
{
    public interface IDishRepository
    {
        Task<GetDishResponseDTO> GetDishResponseDTO(DishCategory dishCategory, bool vegetarian, DishSorting sorting,int page);
        Task<GetDishByIdDTO> GetDishByIdDTO(Guid id);
    }
}
