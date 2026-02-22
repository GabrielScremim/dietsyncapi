using dietsync.Domain.Entities;
using dietsync.DTOs;

namespace dietsync.Domain.Interfaces
{
    public interface IReceitaRepository
    {
        Task<List<Receitum>> GetAllByUserIdAsync(ulong userId);
        Task<Receitum?> GetByIdAsync(ulong userId, ulong id);
        Task AddAsync(Receitum receita);
        Task UpdateAsync(Receitum receita);
        Task DeleteAsync(Receitum receita);
    }
}