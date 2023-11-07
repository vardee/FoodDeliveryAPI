using backendTask.DataBase;
using backendTask.DataBase.Dto;
using backendTask.DataBase.Dto.DishDTO;
using backendTask.DataBase.Models;
using backendTask.Enums;
using backendTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace backendTask.Repository
{
    public class DishRepository: IDishRepository
    {
        private readonly AppDBContext _db;
        public DishRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;

        }
        public async Task<GetDishResponseDTO> GetDishResponseDTO(DishCategory dishCategory, bool vegetarian, DishSorting sorting, int page)
        {
            List<Dish> dishes = _db.Dishes.ToList();

            if (dishCategory != null)
            {
                dishes = dishes.Where(d => d.Category == dishCategory).ToList();
            }

            if (!vegetarian)
            {
                dishes = dishes.Where(d => d.Vegetarian == true || d.Vegetarian == false).ToList();
            }

            switch (sorting)
            {
                case DishSorting.NameAsc:
                    dishes = dishes.OrderBy(d => d.Name).ToList();
                    break;
                case DishSorting.NameDesc:
                    dishes = dishes.OrderByDescending(d => d.Name).ToList();
                    break;
                case DishSorting.PriceAsc:
                    dishes = dishes.OrderBy(d => d.Price).ToList();
                    break;
                case DishSorting.PriceDesc:
                    dishes = dishes.OrderByDescending(d => d.Price).ToList();
                    break;
                case DishSorting.RatingAsc:
                    dishes = dishes.OrderBy(d => d.Rating).ToList();
                    break;
                case DishSorting.RatingDesc:
                    dishes = dishes.OrderByDescending(d => d.Rating).ToList();
                    break;
            }

            int pageSize = 10;
            int skipAmount = (page - 1) * pageSize;
            List<Dish> currentDishes = dishes.Skip(skipAmount).Take(pageSize).ToList();

            if (currentDishes.Count == 0)
            {
                throw new BadRequestException("Страница не найдена");
            }

            return new GetDishResponseDTO
            {
                Dishes = currentDishes,
                PageInformation = new PageInformationDTO
                {
                    size = pageSize,
                    count = page,
                    current = page
                }
            };
        }
        public async Task<GetDishByIdDTO> GetDishByIdDTO(Guid CurrentId)
        {
            var currentDish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == CurrentId);
            if(currentDish == null)
            {
                throw new BadRequestException("Данного блюда нет в списке блюд");
            }
            else
            {
                return new GetDishByIdDTO
                {
                    Name = currentDish.Name,
                    Description = currentDish.Description,
                    Price = currentDish.Price,
                    Image = currentDish.Image,
                    Vegetarian = currentDish.Vegetarian,
                    Rating = currentDish.Rating,
                    Category = currentDish.Category,
                    Id = currentDish.Id,
                };
            }
        }
    }  
}

