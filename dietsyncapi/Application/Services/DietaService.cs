using dietsync.Domain.Entities;
using dietsync.DTOs;
using dietsyncapi.Application.Interfaces.IDieta;

namespace dietsyncapi.Application.Services
{
    public class DietaService : IDietaService
    {
        private readonly IDietaRepository _repo;
        public DietaService(IDietaRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseDietaDto> Create(ulong userId, CreateDietaDto dto)
        {
            var dieta = new Dieta
            {
                NomeDieta = dto.NomeDieta,
                TipoDieta = dto.TipoDieta,
                Calorias = dto.Calorias,
                Proteinas = dto.Proteinas,
                Carboidratos = dto.Carboidratos,
                Gorduras = dto.Gorduras,
                DataDieta = dto.DataDieta,
                Refeicao = dto.Refeicao,
                Alimentos = dto.Alimentos,
                Quantidade = dto.Quantidade,
                Observacoes = dto.Observacoes,
                FkIdUsuarioDieta = userId
            };
            var created = await _repo.CreateAsync(dieta);

            return new ResponseDietaDto
            {
                IdDieta = dieta.IdDieta,
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
        }

        public async Task Delete(ulong userId, ulong id)
        {
            var dieta = await _repo.GetByIdAsync(id, userId);
            if (dieta == null)
                throw new Exception("Dieta não encontrada");
            await _repo.DeleteAsync(dieta);
        }

        public async Task<List<ResponseDietaDto>> GetAll(ulong userId)
        {
            var dietas = await _repo.GetAllByUserIdAsync(userId);
            return dietas.Select(d => new ResponseDietaDto
            {
                IdDieta = d.IdDieta,
                NomeDieta = d.NomeDieta,
                TipoDieta = d.TipoDieta,
                Calorias = d.Calorias,
                Proteinas = d.Proteinas,
                Carboidratos = d.Carboidratos,
                Gorduras = d.Gorduras,
                DataDieta = d.DataDieta,
                Refeicao = d.Refeicao,
                Alimentos = d.Alimentos,
                Quantidade = d.Quantidade,
                Observacoes = d.Observacoes
            }).ToList();
        }

        public async Task<ResponseDietaDto> GetById(ulong userId, ulong id)
        {
            var dieta = await _repo.GetByIdAsync(id, userId);

            if (dieta == null)
                throw new Exception("Dieta não encontrada");

            return new ResponseDietaDto
            {
                IdDieta = dieta.IdDieta,
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
        }

        public async Task Update(ulong userId, ulong id, UpdateDietaDto dto)
        {
            var dieta = await _repo.GetByIdAsync(id, userId);

            if (dieta == null)
                throw new Exception("Dieta não encontrada");

            dieta.NomeDieta = dto.NomeDieta;
            dieta.TipoDieta = dto.TipoDieta;
            dieta.Calorias = dto.Calorias;
            dieta.Proteinas = dto.Proteinas;
            dieta.Carboidratos = dto.Carboidratos;
            dieta.Gorduras = dto.Gorduras;
            dieta.DataDieta = dto.DataDieta;
            dieta.Refeicao = dto.Refeicao;
            dieta.Alimentos = dto.Alimentos;
            dieta.Quantidade = dto.Quantidade;
            dieta.Observacoes = dto.Observacoes;

            await _repo.UpdateAsync(dieta);
        }
    }
}
