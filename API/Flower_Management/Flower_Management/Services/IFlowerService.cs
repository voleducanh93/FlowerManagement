using Flower_Management.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flower_Management.Services
{
    public interface IFlowerService
    {
        Task<IEnumerable<Flower>> GetAllFlowersAsync();
        Task<Flower> GetFlowerByIdAsync(int id);
        Task AddFlowerAsync(Flower flower);
        Task UpdateFlowerAsync(Flower flower);
        Task DeleteFlowerAsync(int id);
    }
}
