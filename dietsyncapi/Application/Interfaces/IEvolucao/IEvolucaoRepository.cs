using dietsync.Domain.Entities;

namespace dietsyncapi.Application.Interfaces.IEvolucao
{
    public interface IEvolucaoRepository
    {
        Task<List<Evolucao>> GetAllByUserIdAsync(ulong userId);
        Task<Evolucao?> GetByIdAsync(ulong userId, ulong id);
        Task AddAsync(Evolucao evolucao);
        Task DeleteAsync(Evolucao evolucao);
        Task SaveChangesAsync();
    }
}
