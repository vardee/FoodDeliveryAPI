using backendTask.AdditionalService;
using backendTask.DataBase;
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DBContext.Models;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace backendTask.Controllers
{
    [Route("/")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly TokenHelper _tokenHelper;
        private readonly AppDBContext _db;

        public UserController(IUserRepository userRepo, AppDBContext db, TokenHelper tokenHelper)
        {
            _userRepo = userRepo;
            _db = db;
            _tokenHelper = tokenHelper;
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponseDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepo.Login(loginRequestDTO);
            var user = _db.Users.FirstOrDefault(x => x.Email == loginRequestDTO.email);
            return Ok(new { token = loginResponse.token });
        }



        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegistrationResponseDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var registrationResponse = await _userRepo.Register(registrationRequestDTO);
            return Ok(new { token = registrationResponse.token });

        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("Logout")]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Logout()
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
            await _userRepo.Logout(token);
            return Ok();

        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpGet("GetProfile")]

        [ProducesResponseType(typeof(GetProfileDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> GetProfile()
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
            return Ok(await _userRepo.GetProfileDto(token));
        }
        [Authorize(Policy = "TokenNotInBlackList")]
        [HttpPut("EditProfile")]
        [ProducesResponseType(typeof(EditProfileRequestDTO), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        [ProducesResponseType(typeof(Error), 401)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> editProfile([FromBody] EditProfileRequestDTO editProfileRequestDTO)
        {
            string token = _tokenHelper.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userRepo.EditProfile(token, editProfileRequestDTO);
            return Ok();
        }

    }
}
