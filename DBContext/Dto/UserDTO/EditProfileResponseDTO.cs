using backendTask.Enums;

namespace backendTask.DataBase.Dto.UserDTO
{
    public class EditProfileResponseDTO
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        public Guid Address { get; set; }
    }
}
