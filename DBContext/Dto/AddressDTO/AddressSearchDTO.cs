using backendTask.Enums;
using System.Numerics;

namespace backendTask.DBContext.Dto.AddressDTO
{
    public class AddressSearchDTO
    {
        public long objectId { get; set; }
        public Guid objectGuid { get; set; }
        public string text { get; set; }
        public Enum objectLevel { get; set; }
        public string objectLevelText { get; set; }
    }
}
