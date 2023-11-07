using backendTask.AdditionalService;
using backendTask.DataBase;
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DBContext.Models;
using backendTask.InformationHelps;
using backendTask.Repository;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Specialized;
using System.Net;
using System.Security.Claims;

namespace backendTask.Controllers
{
    [Route("/")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly AppDBContext _db;

        public UserController(IUserRepository userRepo, AppDBContext db)
        {
            _userRepo = userRepo;
            _db = db;
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponseDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepo.Login(loginRequestDTO);
            var user = _db.Users.FirstOrDefault(x => x.Email == loginRequestDTO.email);
            if (user==null)
            {
                throw new BadRequestException("Неправильный Email или пароль ");
            }
            else if(!(HashPassword.VerifyPassword(loginRequestDTO.password, user.Password)))
            {
                throw new BadRequestException("Неправильный Email или пароль ");
            }
            else
            {
                return Ok(new { token = loginResponse.token });
            }
        }



        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegistrationResponseDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var registrationResponse = await _userRepo.Register(registrationRequestDTO);
            return Ok(new { token = registrationResponse.token });

        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("Logout")]
        [ProducesResponseType(typeof(Error), 401)]
        public async Task<IActionResult> Logout()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                await _userRepo.Logout(token);
                return Ok();
            }
            throw new UnauthorizedException("Данный пользователь не авторизован");

        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("GetProfile")]

        [ProducesResponseType(typeof(GetProfileDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetProfile()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                return Ok( await _userRepo.GetProfileDto(token));
            }
            throw new UnauthorizedException("Данный пользователь не авторизован");
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPut("EditProfile")]
        [ProducesResponseType(typeof(EditProfileRequestDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> editProfile([FromBody] EditProfileRequestDTO editProfileRequestDTO)
        {
            
            string authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Substring("Bearer ".Length);
                await _userRepo.EditProfile(token, editProfileRequestDTO);
                return Ok();
            }

            throw new UnauthorizedException("Данный пользователь не авторизован");
        }

    }
}
