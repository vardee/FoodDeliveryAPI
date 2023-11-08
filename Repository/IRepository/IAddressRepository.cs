using backendTask.DBContext.Dto.AddressDTO;
using System.Numerics;

namespace backendTask.Repository.IRepository
{
    public interface IAddressRepository
    {
        public Task<List<AddressSearchDTO>> addressSearchDTO(long parentObjectId, string query);
    }
}
