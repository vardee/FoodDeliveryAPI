using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backendTask.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _db;
        private string secretKey;

        public UserRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string Email)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == Email);
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == loginRequestDTO.email.ToLower()
            && u.Password == loginRequestDTO.password);
            if(user == null)
            {
                return new LoginResponseDTO()
                {
                    token = "",
                    email = null

                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                token = tokenHandler.WriteToken(token),
                email = user

            };
            return loginResponseDTO;
        }

        public async Task<User> Register(RegistraionRequestDTO registraionRequestDTO)
        {
            User user = new User()
            {
                FullName = registraionRequestDTO.FullName,
                Email = registraionRequestDTO.Email,
                Password = registraionRequestDTO.Password,
                BirthDate = registraionRequestDTO.BirthDate,
                Gender = registraionRequestDTO.Gender,
                Phone = registraionRequestDTO.Phone,
                Address = registraionRequestDTO.Address
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
