using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Enums;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backendTask.Controllers
{
    [Route("api/")]
    public class DishController : Controller
    {
        private readonly IDishRepository _dishRepo;
        private readonly AppDBContext _db;

        public DishController(IDishRepository dishRepo, AppDBContext db)
        {
            _dishRepo = dishRepo;
            _db = db;
        }
        [HttpGet("dish")]
        public async Task<IActionResult> GetDishes(DishCategory dishCategory, bool vegetarian, DishSorting sorting, int page)
        {
            var result = await _dishRepo.GetDishResponseDTO(dishCategory, vegetarian, sorting, page);
            return Ok(result);
        }
        [HttpGet("dish/{Id:guid}")]
        public async Task<IActionResult> GetDishByIdDTO(Guid Id)
        {
            Console.WriteLine(Id);
            var result = await _dishRepo.GetDishByIdDTO(Id);
            return Ok(result);
        }
    }
}
