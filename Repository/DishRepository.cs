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
            if (page <= 0)
            {
                throw new NotFoundException("Данная страница не найдена");
            }

            if (dishCategory != null)
            {
                dishes = dishes.Where(d => d.Category == dishCategory).ToList();
            }

            if (vegetarian == false)
            {
                dishes = dishes.Where(d =>  d.Vegetarian == false).ToList();
            }
            else
            {
                dishes = dishes.Where(d => d.Vegetarian == true).ToList();
            }

            if (dishes.Count == 0)
            {
                throw new BadRequestException("Нет данных, удовлетворяющих вашим критериям");
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

            int pageSize = 6;
            int skipAmount = (page - 1) * pageSize;

            if (skipAmount >= dishes.Count)
            {
                throw new NotFoundException("Данная страница не найдена");
            }

            List<Dish> currentDishes = dishes.Skip(skipAmount).Take(pageSize).ToList();

            return new GetDishResponseDTO
            {
                Dishes = currentDishes,
                PageInformation = new PageInformationDTO
                {
                    size = pageSize,
                    count = (int)Math.Ceiling((double)dishes.Count / pageSize),
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

