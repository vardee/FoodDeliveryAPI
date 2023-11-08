using backendTask.Enums;
using backendTask.InformationHelps;
using backendTask.InformationHelps.Attribute;
using System.ComponentModel.DataAnnotations;
using PhoneAttribute = backendTask.InformationHelps.Attribute.PhoneAttribute;

namespace backendTask.DataBase.Dto.UserDTO
{
    public class EditProfileRequestDTO
    {
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }
        public Guid? Address { get; set; }
    }
}
