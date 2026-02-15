using dietsync.Domain.Entities;
using dietsync.DTOs;
using dietsync.Infrastructure.Data;
using dietsyncapi.Application.Interfaces.IDieta;
using Microsoft.EntityFrameworkCore;

namespace dietsyncapi.Infrastructure.Repositories.DietaRepository
{
    public class DietaRepository : IDietaRepository
    {
        private readonly AppDbContext _context;
        public DietaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dieta> CreateAsync(Dieta dieta)
        {
            await _context.Dietas.AddAsync(dieta);
            await _context.SaveChangesAsync();
            var createdDieta = new CreateDietaDto
            {
                NomeDieta = dieta.NomeDieta,
                TipoDieta = dieta.TipoDieta,
                Calorias = dieta.Calorias,
                Proteinas = dieta.Proteinas,
                Carboidratos = dieta.Carboidratos,
                Gorduras = dieta.Gorduras,
                DataDieta = dieta.DataDieta,
                Refeicao = dieta.Refeicao,
                Alimentos = dieta.Alimentos,
                Quantidade = dieta.Quantidade,
                Observacoes = dieta.Observacoes
            };
            return dieta;
        }

        public async Task DeleteAsync(Dieta dieta)
        {
            _context.Dietas.Remove(dieta);
            await _context.SaveChangesAsync();
        }

        public Task<List<Dieta>> GetAllByUserIdAsync(ulong userId)
        {
            return _context.Dietas.Where(d => d.FkIdUsuarioDieta == userId).ToListAsync();

        }

        public Task<Dieta> GetByIdAsync(ulong id, ulong userId)
        {
            return _context.Dietas
                .FirstOrDefaultAsync(d => d.IdDieta == id && d.FkIdUsuarioDieta == userId);
        }

        public async Task<Dieta> UpdateAsync(Dieta dieta)
        {
            _context.Dietas.Update(dieta);
            await _context.SaveChangesAsync();

            return dieta;
        }
    }
}
