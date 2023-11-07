using backendTask.DataBase.Dto.OrderDTO;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using backendTask.DBContext.Models;
using backendTask.DataBase;

namespace backendTask.Controllers
{
    [Authorize(Policy = "TokenNotInBlackList")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly TokenHelper _tokenHelper;
        private readonly AppDBContext _db;

        public OrderController(IOrderRepository orderRepo, TokenHelper tokenHelper,AppDBContext db)
        {
            _orderRepo = orderRepo;
            _tokenHelper = tokenHelper;
            _db = db;
        }

        [HttpGet("order/{Id:guid}")]
        [ProducesResponseType(typeof(GetOrderByIdDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetOrderById(Guid Id)
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }

            return Ok(await _orderRepo.getOrderById(token, Id));
        }

        [HttpPost("order")]
        [ProducesResponseType(typeof(CreateOrderDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }

            await _orderRepo.createOrderDTO(token, createOrderDTO);

            return Ok();
        }

        [HttpGet("order")]
        [ProducesResponseType(typeof(GetListOrdersDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetListOrders()
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }

            return Ok(await _orderRepo.getListOrdersDTO(token));
        }

        [HttpPost("order/{Id:guid}/status")]
        [ProducesResponseType(typeof(ConfirmOrderStatusDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> ConfirmOrderStatusDTO(Guid Id)
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }

            return Ok(await _orderRepo.confirmOrderStatus(token, Id));
        }
    }
}
