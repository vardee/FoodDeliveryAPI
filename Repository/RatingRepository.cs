using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace backendTask.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDBContext _db;
        public RatingRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;
        }

        public async Task<bool> checkUserSetRating(string token, Guid Id)
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
                var userOrder = await _db.Orders.FirstOrDefaultAsync(o => o.UserId == user.Id);
                if (userOrder != null)
                {
                    var checkOrderedDishes = await _db.OrderedDishes.FirstOrDefaultAsync(od => od.DishId == Id && od.OrderId == userOrder.OrderId);
                    if (checkOrderedDishes != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        public async Task setDishRating(string token, Guid Id, double rating)
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
                var userOrder = await _db.Orders.FirstOrDefaultAsync(o => o.UserId == user.Id);
                if (userOrder != null)
                {
                    var checkOrderedDishes = await _db.OrderedDishes.FirstOrDefaultAsync(od => od.DishId == Id && od.OrderId == userOrder.OrderId);
                    if (checkOrderedDishes != null)
                    {
                        var existingRating = await _db.Ratings.FirstOrDefaultAsync(r => r.DishId == Id && r.UserId == user.Id);

                        if (existingRating != null)
                        {
                            existingRating.ratingValue = rating;
                        }
                        else
                        {
                            var newRating = new Rating
                            {
                                DishId = Id,
                                UserId = user.Id,
                                ratingValue = rating
                            };
                            _db.Ratings.Add(newRating);
                        }

                        var dish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == Id);

                        var allRatingsForDish = await _db.Ratings.Where(r => r.DishId == Id).ToListAsync();

                        if (allRatingsForDish.Count > 0)
                        {
                            double averageRating = (double)allRatingsForDish.Average(r => r.ratingValue);
                            dish.Rating = averageRating;
                        }

                        await _db.SaveChangesAsync();
                    }
                }
            }
        }
    }
}



