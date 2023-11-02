using backendTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto
{
    public class RegistrationRequestDTO
    {
        public string FullName { get; set; }
        public DateOnly BirthDate { get; set; }

        public Gender Gender { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string Address { get; set; }
        public string Password { get; set; }
    }
}
