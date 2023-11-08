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
using backendTask.DataBase.Dto.UserDTO;
using backendTask.DataBase;
using backendTask.DBContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using backendTask.DBContext.Models;
using backendTask.InformationHelps.Validator;

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

            if (user == null)
            {
                throw new BadRequestException("Неправильный Email или пароль");
            }
            else if(!HashPassword.VerifyPassword(loginRequestDTO.password, user.Password))
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
            if (!DateOfBirthValidator.ValidateDateOfBirth(registraionRequestDTO.BirthDate))
            {
                throw new BadRequestException("Неверная дата рождения. Вам должно быть не менее 13 лет и не более 100 лет.");
            }
            if (!PasswordValidator.ValidatePassword(registraionRequestDTO.Password))
            {
                throw new BadRequestException("Пароль не соответсвует требованиям, должна быть минимум одна заглавная буква, семь обычных букв, минимум одна цифра и один спец.символ");
            }
            if (!PhoneValidator.IsValidPhoneNumber(registraionRequestDTO.Phone))
            {
                throw new BadRequestException("Данный формат номера телефона не валиден");
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
                        Id = user.Id,
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
                    throw new BadRequestException("Данный пользователь не найден");
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

                    user.FullName = editProfileRequestDTO.FullName ?? user.FullName;

                    if (editProfileRequestDTO.Address != null)
                    {
                        if (editProfileRequestDTO.Address != null && !await AddressChecker.IsAddressNormal(_adb, editProfileRequestDTO.Address))
                        {
                            throw new BadRequestException("Данный адрес не найден, повторите еще раз");
                        }
                        user.Address = (Guid)editProfileRequestDTO.Address;
                    }

                    if (editProfileRequestDTO.BirthDate != null)
                    {
                        if (!DateOfBirthValidator.ValidateDateOfBirth((DateTime)(editProfileRequestDTO.BirthDate)))
                        {
                            throw new BadRequestException("Неверная дата рождения. Вам должно быть не менее 13 лет и не более 100 лет.");
                        }
                        user.BirthDate = (DateTime)(editProfileRequestDTO?.BirthDate);
                    }

                    if (editProfileRequestDTO.Phone != null)
                    {
                        if (!PhoneValidator.IsValidPhoneNumber(editProfileRequestDTO.Phone))
                        {
                            throw new BadRequestException("Данный формат номера телефона не валиден");
                        }
                        user.Phone = editProfileRequestDTO.Phone;
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
