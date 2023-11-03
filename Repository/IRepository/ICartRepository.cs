using backendTask.DataBase.Dto;

namespace backendTask.Repository.IRepository
{
    public interface ICartRepository
    {
        public Task<List<GetUserCartResponseDTO>> GetUserCartDTO(string token);
        public Task AddToUserCartDTO(string token, Guid Id);
        public Task DeleteFromUserCartDTO(string token, Guid Id);

    }
}
