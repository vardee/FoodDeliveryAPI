using backendTask.Enums;
using System.ComponentModel.DataAnnotations;

namespace backendTask.DataBase.Dto.UserDTO
{
    public class EditProfileRequestDTO
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
