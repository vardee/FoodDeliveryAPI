using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Enums;
using backendTask.Migrations;
using backendTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace backendTask.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _db;
        public OrderRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;
        }
        public async Task<GetOrderByIdDTO> getOrderById(string token, Guid Id)
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
                var userOrder = await _db.Orders.FirstOrDefaultAsync(o => o.OrderId == Id);
                if (userOrder != null)
                {
                    var orderedDishes = _db.OrderedDishes
                        .Where(od => od.OrderId == Id)
                        .ToList();

                    var orderedUserDishes = orderedDishes.Select(orderedDish => new GetUserCartResponseDTO
                    {
                        Id = orderedDish.DishId,
                        name = _db.Dishes.FirstOrDefault(dish => dish.Id == orderedDish.DishId)?.Name,
                        price = (int)(_db.Dishes.FirstOrDefault(dish => dish.Id == orderedDish.DishId)?.Price),
                        amount = orderedDish.Amount,
                        totalPrice = orderedDish.Amount * (_db.Dishes.FirstOrDefault(dish => dish.Id == orderedDish.DishId)?.Price ?? 0),
                        image = _db.Dishes.FirstOrDefault(dish => dish.Id == orderedDish.DishId)?.Image
                    }).ToList();

                    return new GetOrderByIdDTO
                    {
                        Id = userOrder.OrderId,
                        deliveryTime = userOrder.DeliveryTime,
                        orderTime = userOrder.OrderTime,
                        status = userOrder.Status,
                        price = userOrder.Price,
                        dishes = orderedUserDishes,
                        adress = userOrder.Address
                    };
                }
            }
            return null;
        }

        public async Task createOrderDTO(string token, CreateOrderDTO createOrderDTO)
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
                    var totalPrice = 0;
                    var orderId = Guid.NewGuid();
                    var userCartItems = _db.Carts
                        .Where(cartItem => cartItem.UserId == user.Id)
                        .ToList();

                    if (!userCartItems.Any())
                    {
                        throw new Exception(message:"Корзина пуста"); 
                    }

                    foreach (var cartItem in userCartItems)
                    {
                        totalPrice += cartItem.Amount * (_db.Dishes.FirstOrDefault(dish => dish.Id == cartItem.DishId)?.Price ?? 0);
                        var orderedDish = new OrderedDishes
                        {
                            DishId = cartItem.DishId,
                            Amount = cartItem.Amount,
                            OrderId = orderId
                        };

                        _db.OrderedDishes.Add(orderedDish);
                        _db.Carts.Remove(cartItem);
                    }
                    await _db.SaveChangesAsync();

                    var newOrder = new Order
                    {
                        OrderId = orderId,
                        UserId = user.Id,
                        DeliveryTime = createOrderDTO.deliveryTime,
                        OrderTime = DateTime.UtcNow,
                        Status = OrderStatus.InProcess,
                        Price = totalPrice,
                        Address = createOrderDTO.AdreessId
                    };

                    _db.Orders.Add(newOrder);
                    await _db.SaveChangesAsync();
                }
            }
        }
        public async Task<List<GetListOrdersDTO>> getListOrdersDTO(string token)
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
                    var userOrders = _db.Orders
                        .Where(od => od.UserId == user.Id)
                        .ToList();

                    var listOrdersDTO = userOrders.Select(order => new GetListOrdersDTO
                    {
                        Id = order.OrderId,
                        deliveryTime = order.DeliveryTime,
                        orderTime = order.OrderTime,
                        status = order.Status,
                        price = order.Price,
                    }).ToList();

                    return listOrdersDTO;
                }
            }
            return new List<GetListOrdersDTO>();
        }
        public async Task<ConfirmOrderStatusDTO> confirmOrderStatus(string token, Guid Id)
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
                    // Найдите заказ пользователя по UserId и OrderId
                    var order = await _db.Orders.FirstOrDefaultAsync(od => od.UserId == user.Id && od.OrderId == Id);

                    if (order != null)
                    {
   
                        order.Status = OrderStatus.Delivered;
                        await _db.SaveChangesAsync();
                    }
                }
            }
            return null;
        }
    }
}
