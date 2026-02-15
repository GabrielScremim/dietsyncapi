using dietsync.Domain.Entities;
using dietsync.DTOs;

namespace dietsyncapi.Application.Interfaces.IDieta
{
    public interface IDietaRepository
    {
        Task<Dieta> GetByIdAsync(ulong id, ulong userId);
        Task<List<Dieta>> GetAllByUserIdAsync(ulong userId);
        Task<Dieta> CreateAsync(Dieta dieta);
        Task<Dieta> UpdateAsync(Dieta dieta);
        Task DeleteAsync(Dieta dieta);
    }
}
