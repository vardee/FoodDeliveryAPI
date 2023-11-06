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
        private readonly IRatingRepository _ratingRepo;
        private readonly AppDBContext _db;

        public DishController(IDishRepository dishRepo, AppDBContext db, IRatingRepository ratingRepo)
        {
            _dishRepo = dishRepo;
            _db = db;
            _ratingRepo = ratingRepo;
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
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("api/dish/{Id:guid}/rating/check")]
        public async Task<IActionResult> checkUserSetRating(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                return Ok(await _ratingRepo.checkUserSetRating(token, Id));
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost("api/dish/{Id:guid}/rating")]
        public async Task<IActionResult> setDishRating(Guid Id,double Rating)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                await _ratingRepo.setDishRating(token, Id, Rating);
                return Ok();
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }
    }
}
