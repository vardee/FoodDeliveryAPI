using backendTask.DataBase.Dto;
using backendTask.Enums;

namespace backendTask.Repository.IRepository
{
    public interface IDishRepository
    {
        Task<GetDishResponseDTO> GetDishResponseDTO(DishCategory dishCategory, bool vegetarian, DishSorting sorting,int page);
        Task<GetDishByIdDTO> GetDishByIdDTO(Guid id);
    }
}
