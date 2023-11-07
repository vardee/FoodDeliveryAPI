﻿using backendTask.DataBase;
using backendTask.DataBase.Dto.CartDTO;
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DBContext.Models;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backendTask.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;
        private readonly AppDBContext _db;
        private readonly TokenHelper _tokenHelper;

        public CartController(ICartRepository cartRepo, AppDBContext db,TokenHelper tokenHelper)
        {
            _cartRepo = cartRepo;
            _db = db;
            _tokenHelper = tokenHelper;
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("GetUserCart")]
        [ProducesResponseType(typeof(GetUserCartResponseDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetUserCart()
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }

            var result = await _cartRepo.GetUserCartDTO(token);
            return Ok(result);
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost("AddToUserCart/{Id:guid}")]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> AddToUserCartDTO(Guid Id)
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
            await _cartRepo.AddToUserCartDTO(token, Id);
            return Ok();
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpDelete("DeleteFromUserCart/{Id:guid}")]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> DeleteFromUserCartDTO(Guid Id)
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
            await _cartRepo.DeleteFromUserCartDTO(token, Id);
            return Ok();
        }

    }
}
