using backendTask.Enums;
using backendTask.InformationHelps;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto.UserDTO
{
    public class RegistrationRequestDTO
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Неправильный формат email")]
        public string Email { get; set; }

        public Guid Address { get; set; }
        public string Password { get; set; }
    }
}
