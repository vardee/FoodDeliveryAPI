using backendTask.AdditionalService;
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
using backendTask.Enums;
using backendTask.InformationHelps;
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DataBase;
using backendTask.DBContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;

namespace backendTask.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _db;
        private readonly AddressDBContext _adb;
        private readonly TokenHelper _tokenHelper;
        private string secretKey;
        private string issuer;
        private string audience;


        public UserRepository(AppDBContext db, IConfiguration configuration, TokenHelper tokenHelper,AddressDBContext adb)
        {
            _db = db;
            _adb = adb;
            secretKey = configuration.GetValue<string>("AppSettings:Secret");
            issuer = configuration.GetValue<string>("AppSettings:Issuer");
            audience = configuration.GetValue<string>("AppSettings:Audience");
            _tokenHelper = tokenHelper;
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
                throw new BadRequestException("Неправильный Email или пароль");
            }

            var token = _tokenHelper.GenerateToken(user);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                token = token
            };
            return loginResponseDTO;
        }

        public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO registraionRequestDTO)
        {
            var IsTrueUser = _db.Users.FirstOrDefault(u => registraionRequestDTO.Email == u.Email);
            if (IsTrueUser != null)
            {
                throw new BadRequestException("Данный Email уже используется");
            }
            if (registraionRequestDTO.Address != null && !await AddressChecker.IsAddressNormal(_adb, registraionRequestDTO.Address))
            {
                throw new BadRequestException("Данный адрес не найден, повторите еще раз");
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

            var token = _tokenHelper.GenerateToken(user);

            RegistrationResponseDTO registrationResponseDTO = new RegistrationResponseDTO()
            {
                token = token
            };

            return registrationResponseDTO;
        }
        public async Task Logout(string token)
        {
            string email = _tokenHelper.GetUserEmailFromToken(token);
            if (!string.IsNullOrEmpty(email))
            {
                await _db.BlackListTokens.AddAsync(new BlackListTokens { BlackToken = token });
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
        }
        public async Task<GetProfileDTO> GetProfileDto(string token)
        {
            string email = _tokenHelper.GetUserEmailFromToken(token);

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
                else
                {
                    throw new BadRequestException("Пользователь не найден");
                }
            }
            else
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }

        }
        public async Task EditProfile(string token, EditProfileRequestDTO editProfileRequestDTO)
        {
            string email = _tokenHelper.GetUserEmailFromToken(token);
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
                        user.BirthDate = (DateTime)editProfileRequestDTO.BirthDate;
                    }

                    if (editProfileRequestDTO.Gender != null)
                    {
                        user.Gender = (Gender)editProfileRequestDTO.Gender;
                    }

                    if (!string.IsNullOrEmpty(editProfileRequestDTO.Phone))
                    {
                        user.Phone = editProfileRequestDTO.Phone;
                    }

                    if (!string.IsNullOrEmpty((editProfileRequestDTO.Address).ToString()))
                    {
                        if (editProfileRequestDTO.Address != null && !await AddressChecker.IsAddressNormal(_adb, editProfileRequestDTO.Address))
                        {
                            throw new BadRequestException("Данный адрес не найден, повторите еще раз");
                        }
                        else
                        {
                            user.Address = editProfileRequestDTO.Address;
                        }
                    }

                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                throw new UnauthorizedException("Данный пользователь не авторизован");
            }
        }
    }
}
