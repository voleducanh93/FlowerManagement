using Flower_Management.Entities;

namespace Flower_Management.Repositories
{
    public interface IFlowerRepository
    {
        Task<IEnumerable<Flower>> GetAllFlowersAsync();
        Task<Flower?> GetFlowerByIdAsync(int id);
        Task AddFlowerAsync(Flower flower);
        Task UpdateFlowerAsync(Flower flower);
        Task DeleteFlowerAsync(int id);
        Task<bool> FlowerExistsAsync(int id);
    }
}
