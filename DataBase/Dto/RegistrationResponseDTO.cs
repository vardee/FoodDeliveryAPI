using backendTask.DataBase.Models;

namespace backendTask.DataBase.Dto
{
    public class RegistrationResponseDTO
    {
        public User email { get; set; }
        public string token { get; set; }
    }
}
