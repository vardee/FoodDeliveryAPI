using backendTask.Enums;
using backendTask.InformationHelps;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [StringLength(1, ErrorMessage = "Имя должно содержать минимум 1 символ.")]
        public string FullName { get; set; }


        [DateOfBirthAttribute]
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [InformationHelps.PhoneAttribute]
        public string Phone { get; set; }


        [EmailAddress(ErrorMessage = "Неправильный формат email")]
        public string Email { get; set; }

        public Guid Address { get; set; }
        [PasswordAttribute]
        public string Password { get; set; }
    }
}
