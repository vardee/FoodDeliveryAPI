using backendTask.DataBase.Dto;

namespace backendTask.Repository.IRepository
{
    public interface IRatingRepository
    {
        public Task<bool> checkUserSetRating(string token, Guid Id);
        public Task setDishRating(string token, Guid Id, double rating);
    }
}
