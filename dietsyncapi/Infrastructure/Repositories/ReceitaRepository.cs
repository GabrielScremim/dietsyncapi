using dietsync.Domain.Entities;
using dietsync.Domain.Interfaces;
using dietsync.DTOs;
using dietsync.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace dietsyncapi.Infrastructure.Repositories
{
    public class ReceitaRepository : IReceitaRepository
    {
        private readonly AppDbContext _context;
        public ReceitaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Receitum receita)
        {
            await _context.Receita.AddAsync(receita);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Receitum receita)
        {
            _context.Receita.Remove(receita);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Receitum>> GetAllByUserIdAsync(ulong userId)
        {
            return await _context.Receita.Where(r => r.FkIdUserReceita == userId).ToListAsync();
        }

        public Task<Receitum?> GetByIdAsync(ulong userId, ulong id)
        {
            return _context.Receita.FirstOrDefaultAsync(r => r.FkIdUserReceita == userId && r.IdReceitas == id);
        }

        public async Task UpdateAsync(Receitum receita)
        {
            _context.Receita.Update(receita);
            await _context.SaveChangesAsync();
        }
    }
}
