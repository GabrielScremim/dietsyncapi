using dietsync.Domain.Entities;
using dietsync.Infrastructure.Data;
using dietsyncapi.Application.Interfaces.IEvolucao;
using Microsoft.EntityFrameworkCore;

namespace dietsyncapi.Infrastructure.Repositories
{
    public class EvolucaoRepository : IEvolucaoRepository
    {
        private readonly AppDbContext _context;

        public EvolucaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Evolucao evolucao)
        {
            await _context.Evolucaos.AddAsync(evolucao);
        }

        public Task DeleteAsync(Evolucao evolucao)
        {
            _context.Evolucaos.Remove(evolucao);
            return Task.CompletedTask;
        }

        public async Task<List<Evolucao>> GetAllByUserIdAsync(ulong userId)
        {
            return await _context.Evolucaos.Where(e => e.FkIdEvolucaos == userId).ToListAsync();
        }

        public async Task<Evolucao?> GetByIdAsync(ulong userId, ulong id)
        {
            return await _context.Evolucaos.FirstOrDefaultAsync(e => e.FkIdEvolucaos == userId && e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
