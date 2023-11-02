using backendTask.AdditionalService;
using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using backendTask.Enums;
using backendTask.InformationHelps;

namespace backendTask.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _db;
        private string secretKey;
        private string issuer;
        private string audience;

        public UserRepository(AppDBContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("AppSettings:Secret");
            issuer = configuration.GetValue<string>("AppSettings:Issuer");
            audience = configuration.GetValue<string>("AppSettings:Audience");
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
            var user = _db.Users.FirstOrDefault(u => u.Email == loginRequestDTO.email);

            if (user == null || !HashPassword.VerifyPassword(loginRequestDTO.password, user.Password))
            {
                return new LoginResponseDTO()
                {
                    token = "",
                    email = null
                };
            }
            var claims = new List<Claim>{new Claim(ClaimTypes.Email, user.Email) };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
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

        public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO registraionRequestDTO)
        {
            var IsTrueUser = _db.Users.FirstOrDefault(u => registraionRequestDTO.Email == u.Email);
            if (IsTrueUser != null)
            {
                throw new Exception(message: "Email is already in use");
            }
            User user = new User()
            {
                FullName = registraionRequestDTO.FullName,
                BirthDate = registraionRequestDTO.BirthDate,
                Gender = registraionRequestDTO.Gender,
                Phone = registraionRequestDTO.Phone,
                Email = registraionRequestDTO.Email,
                Address = registraionRequestDTO.Address,
                Password = HashPassword.HashingPassword(registraionRequestDTO.Password),
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var claims = new List<Claim>{new Claim(ClaimTypes.Email, user.Email)};

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            RegistrationResponseDTO registrationResponseDTO = new RegistrationResponseDTO()
            {
                token = tokenHandler.WriteToken(token),
                email = user
            };

            return registrationResponseDTO;
        }
        public async Task Logout(string token)
        {
            await _db.BlackListTokens.AddAsync(new BlackListTokens{BlackToken = token});
            await _db.SaveChangesAsync();
        }
        public async Task<GetProfileDTO> GetProfileDto(string token)
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
                    return new GetProfileDTO
                    {
                        FullName = user.FullName,
                        BirthDate = user.BirthDate,
                        Gender = user.Gender,
                        Phone = user.Phone,
                        Email = user.Email,
                        Address = user.Address,
                    };
                }
            }

            return null;
        }
        public async Task EditProfile(string token, EditProfileRequestDTO editProfileRequestDTO)
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
                    if (editProfileRequestDTO.FullName != null)
                    {
                        user.FullName = editProfileRequestDTO.FullName;
                    }

                    if (editProfileRequestDTO.BirthDate != null)
                    {
                        user.BirthDate = (DateOnly)editProfileRequestDTO.BirthDate;
                    }

                    if (editProfileRequestDTO.Gender != null)
                    {
                        user.Gender = (Gender)editProfileRequestDTO.Gender;
                    }

                    if (!string.IsNullOrEmpty(editProfileRequestDTO.Phone))
                    {
                        user.Phone = editProfileRequestDTO.Phone;
                    }

                    if (!string.IsNullOrEmpty(editProfileRequestDTO.Address))
                    {
                        user.Address = editProfileRequestDTO.Address;
                    }

                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
