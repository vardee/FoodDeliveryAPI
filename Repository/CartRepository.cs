﻿ using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Enums;
using backendTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace backendTask.Repository
{
    public class CartRepository: ICartRepository
    {
        private readonly AppDBContext _db;
       public CartRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;
        }
        public async Task<List<GetUserCartResponseDTO>> GetUserCartDTO(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string email = "";
            if (jwtToken.Payload.TryGetValue("email", out var emailObj) && emailObj is string emailValue)
            {
                email = emailValue;
            }

            if (!string.IsNullOrEmpty(email))
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    var userCartItems = _db.Carts
                        .Where(cart => cart.UserId == user.Id)
                        .ToList();

                    var userCartDTO = userCartItems.Select(cartItem => new GetUserCartResponseDTO
                    {
                        Id = cartItem.DishId,
                        name = _db.Dishes.FirstOrDefault(dish => dish.Id == cartItem.DishId)?.Name,
                        price = (int)(_db.Dishes.FirstOrDefault(dish => dish.Id == cartItem.DishId)?.Price),
                        amount = cartItem.Amount,
                        totalPrice = cartItem.Amount * (_db.Dishes.FirstOrDefault(dish => dish.Id == cartItem.DishId)?.Price ?? 0),
                        image = _db.Dishes.FirstOrDefault(dish => dish.Id == cartItem.DishId)?.Image
                    }).ToList();

                    return userCartDTO;
                }
            }
            return new List<GetUserCartResponseDTO>();
        }
        public async Task AddToUserCartDTO(string token, Guid dishId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string email = "";

            if (jwtToken.Payload.TryGetValue("email", out var emailObj) && emailObj is string emailValue)
            {
                email = emailValue;
            }

            if (!string.IsNullOrEmpty(email))
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    var dish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);

                    if (dish == null)
                    {
                        throw new Exception(message: "Данного блюда нет");
                    }
                    var cartItem = _db.Carts.FirstOrDefault(c => c.UserId == user.Id && c.DishId == dishId);

                    if (cartItem == null)
                    {
                        cartItem = new Cart
                        {
                            UserId = user.Id,
                            DishId = dishId,
                            Amount = 1
                        };
                        _db.Carts.Add(cartItem);
                    }
                    else
                    {
                        cartItem.Amount++;
                    }

                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteFromUserCartDTO(string token, Guid dishId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            string email = "";

            if (jwtToken.Payload.TryGetValue("email", out var emailObj) && emailObj is string emailValue)
            {
                email = emailValue;
            }

            if (!string.IsNullOrEmpty(email))
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    var cartItem = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == user.Id && c.DishId == dishId);

                    if (cartItem != null)
                    {
                        if (cartItem.Amount > 1)
                        {
                            cartItem.Amount--;
                        }
                        else
                        {
                            _db.Carts.Remove(cartItem);
                        }

                        await _db.SaveChangesAsync();
                    }
                }
            }
        }


    }
}