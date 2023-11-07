using backendTask.DataBase;
using backendTask.DataBase.Dto.OrderDTO;
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DBContext.Models;
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
        [ProducesResponseType(typeof(GetOrderByIdDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetOrderById(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                return Ok(await _orderRepo.getOrderById(token, Id));
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost("order")]
        [ProducesResponseType(typeof(CreateOrderDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                await _orderRepo.createOrderDTO(token, createOrderDTO);

                return Ok();
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("order")]
        [ProducesResponseType(typeof(GetListOrdersDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetListOrders()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                return Ok(await _orderRepo.getListOrdersDTO(token));
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPost ("order/{Id:guid}/status")]
        [ProducesResponseType(typeof(ConfirmOrderStatusDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> ConfirmOrderStatusDTO(Guid Id)
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);

                return Ok(await _orderRepo.confirmOrderStatus(token,Id));
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }
    }
}
