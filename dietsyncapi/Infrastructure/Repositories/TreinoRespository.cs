using Microsoft.EntityFrameworkCore;
using dietsync.Domain.Entities;
using dietsync.Infrastructure.Data;

namespace dietsync.Infrastructure.Repositories
{
    public class TreinoRepository : ITreinoRepository
    {
        private readonly AppDbContext _context;

        public TreinoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Treino>> GetAllByUserAsync(ulong userId)
        {
            return await _context.Treinos
                .Where(t => t.FkIdUsuarioTreino == userId)
                .ToListAsync();
        }

        public async Task<Treino?> GetByIdAsync(ulong id, ulong userId)
        {
            return await _context.Treinos
                .FirstOrDefaultAsync(t => t.Id == id && t.FkIdUsuarioTreino == userId);
        }

        public async Task AddAsync(Treino treino)
        {
            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Treino treino)
        {
            _context.Treinos.Update(treino);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Treino treino)
        {
            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();
        }
    }
}
