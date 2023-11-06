using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backendTask.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly AppDBContext _db;

        public OrderController(IOrderRepository orderRepo, AppDBContext db)
        {
            _orderRepo = orderRepo;
            _db = db;
        }

        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("order/{Id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                return Ok(await _orderRepo.getOrderById(token, Id));
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                await _orderRepo.createOrderDTO(token, createOrderDTO);

                return Ok(new {message = "Заказ успешно создан"});
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("order")]
        public async Task<IActionResult> GetListOrders()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                return Ok(await _orderRepo.getListOrdersDTO(token));
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost ("order/{Id:guid}/status")]
        public async Task<IActionResult> ConfirmOrderStatusDTO(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                return Ok(await _orderRepo.confirmOrderStatus(token,Id));
            }

            return BadRequest(new { message = "Плохой профиль бро" });
        }
    }
}
