using backendTask.DataBase.Dto;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace backendTask.Controllers
{
    [Route("/")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpPost("login")]
        public async  Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.email == null || string.IsNullOrEmpty(loginResponse.token))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return View();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequestDTO model)
        {
            return View();
        }
    }
}
