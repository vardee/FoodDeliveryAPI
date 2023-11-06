﻿using backendTask.DataBase.Models;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backendTask.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly AppDBContext _db;

        public CartController(ICartRepository cartRepo, AppDBContext db)
        {
            _cartRepo = cartRepo;
            _db = db;
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("GetUserCart")]
        public async Task<IActionResult> GetUserCart()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                var result = await _cartRepo.GetUserCartDTO(token);
                return Ok(result);
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost("AddToUserCart/{Id:guid}")]
        public async Task<IActionResult> AddToUserCartDTO(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                await _cartRepo.AddToUserCartDTO(token, Id);
                return Ok(new { message = "Блюдо успешно добавлено в корзину" });
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpDelete("DeleteFromUserCart/{Id:guid}")]
        public async Task<IActionResult> DeleteFromUserCartDTO(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                await _cartRepo.DeleteFromUserCartDTO(token, Id);
                return Ok(new { message = "Блюдо успешно удалено из корзину" });
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }

    }
}