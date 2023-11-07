using backendTask.DataBase;
using backendTask.DataBase.Dto;
using backendTask.DataBase.Dto.DishDTO;
using backendTask.DataBase.Dto.RatingDTO;
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DBContext.Models;
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
        [ProducesResponseType(typeof(GetDishResponseDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetDishes(DishCategory dishCategory, bool vegetarian, DishSorting sorting, int page)
        {
            var result = await _dishRepo.GetDishResponseDTO(dishCategory, vegetarian, sorting, page);
            return Ok(result);
        }
        [HttpGet("dish/{Id:guid}")]
        [ProducesResponseType(typeof(GetDishByIdDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetDishByIdDTO(Guid Id)
        {

            var result = await _dishRepo.GetDishByIdDTO(Id);
            return Ok(result);
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("api/dish/{Id:guid}/rating/check")]
        [ProducesResponseType(typeof(CheckUserSetRatingDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> checkUserSetRating(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                return Ok(await _ratingRepo.checkUserSetRating(token, Id));
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost("api/dish/{Id:guid}/rating")]
        [ProducesResponseType(typeof(SetDishRatingDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> setDishRating(Guid Id,double Rating)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                await _ratingRepo.setDishRating(token, Id, Rating);
                return Ok();
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }
    }
}
