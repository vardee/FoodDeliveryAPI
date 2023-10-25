using backendTask.DataBase.Dto;
using backendTask.DataBase.Models;

namespace backendTask.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string email);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegistraionRequestDTO registraionRequestDTO);
    }
}
