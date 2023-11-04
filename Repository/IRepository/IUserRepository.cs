using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;
using Microsoft.Extensions.Primitives;

namespace backendTask.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<RegistrationResponseDTO> Register(RegistrationRequestDTO registraionRequestDTO);
        Task<GetProfileDTO> GetProfileDto(string token);
        Task EditProfile(string token, EditProfileRequestDTO editProfileRequestDTO);
        Task Logout(string token); 
    }
}
