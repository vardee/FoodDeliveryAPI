using backendTask.DataBase.Models;

namespace backendTask.DataBase.Dto.UserDTO
{
    public class LoginResponseDTO
    {
        public User email { get; set; }
        public string token { get; set; }
    }
}
