using backendTask.Enums;
using backendTask.InformationHelps;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto.UserDTO
{
    public class EditProfileRequestDTO
    {
        [StringLength(4, ErrorMessage = "Имя должно содержать минимум 4 символа.")]
        public string FullName { get; set; }
        [DateOfBirthAttribute]
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [InformationHelps.PhoneAttribute]
        public string Phone { get; set; }

        public Guid Address { get; set; }
    }
}
