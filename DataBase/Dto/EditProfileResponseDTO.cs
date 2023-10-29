using backendTask.Enums;

namespace backendTask.DataBase.Dto
{
    public class EditProfileResponseDTO
    {
        public string FullName { get; set; }
        public DateOnly BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
