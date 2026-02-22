using dietsync.Domain.Entities;
using dietsync.Domain.Interfaces;
using dietsync.DTOs;

namespace dietsyncapi.Application.Services
{
    public class ReceitaService : IReceitaService
    {
        private readonly IReceitaRepository _repo;
        public ReceitaService(IReceitaRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseReceitaDto> Create(ulong userId, CreateReceitaDto dto)
        {
            var receita = new Receitum
            {
                NomeReceita = dto.NomeReceita,
                Ingredientes = dto.Ingredientes,
                ModoPreparo = dto.ModoPreparo,
                Calorias = dto.Calorias,
                Proteinas = dto.Proteinas,
                Carboidratos = dto.Carboidratos,
                Gordura = dto.Gordura,
                FkIdUserReceita = userId
            };
            await _repo.AddAsync(receita);
            return await Task.FromResult(new ResponseReceitaDto
            {
                IdReceitas = receita.IdReceitas,
                NomeReceita = dto.NomeReceita,
                Ingredientes = dto.Ingredientes,
                ModoPreparo = dto.ModoPreparo,
                Calorias = dto.Calorias,
                Proteinas = dto.Proteinas,
                Carboidratos = dto.Carboidratos,
                Gordura = dto.Gordura
            });
        }

        public async Task<bool> DeleteAsync(ulong userId, ulong id)
        {
            var receita = await _repo.GetByIdAsync(userId, id);
            if (receita == null)
                return false;
            await _repo.DeleteAsync(receita);
            return true;
        }

        public async Task<List<ResponseReceitaDto>> GetAll(ulong userId)
        {
            var receitas = await _repo.GetAllByUserIdAsync(userId);
            return receitas.Select(r => new ResponseReceitaDto
            {
                IdReceitas = r.IdReceitas,
                NomeReceita = r.NomeReceita,
                Ingredientes = r.Ingredientes,
                ModoPreparo = r.ModoPreparo,
                Calorias = r.Calorias,
                Proteinas = r.Proteinas,
                Carboidratos = r.Carboidratos,
                Gordura = r.Gordura
            }).ToList();
        }

        public async Task<ResponseReceitaDto?> GetById(ulong userId, ulong id)
        {
            var receita = await _repo.GetByIdAsync(userId, id);
            if (receita == null)
                return await Task.FromResult<ResponseReceitaDto?>(null);
            return await Task.FromResult<ResponseReceitaDto?>(new ResponseReceitaDto
            {
                IdReceitas = receita.IdReceitas,
                NomeReceita = receita.NomeReceita,
                Ingredientes = receita.Ingredientes,
                ModoPreparo = receita.ModoPreparo,
                Calorias = receita.Calorias,
                Proteinas = receita.Proteinas,
                Carboidratos = receita.Carboidratos,
                Gordura = receita.Gordura
            });
        }

        public async Task<ResponseReceitaDto> Update(ulong userId, ulong id, UpdateDto dto)
        {
            var receita = await _repo.GetByIdAsync(userId, id);
            if (receita == null)
                throw new Exception("Receita não encontrada");

            receita.NomeReceita = dto.NomeReceita;
            receita.Ingredientes = dto.Ingredientes;
            receita.ModoPreparo = dto.ModoPreparo;
            receita.Calorias = dto.Calorias;
            receita.Proteinas = dto.Proteinas;
            receita.Carboidratos = dto.Carboidratos;
            receita.Gordura = dto.Gordura;
            await _repo.UpdateAsync(receita);

            return await Task.FromResult(new ResponseReceitaDto
            {
                IdReceitas = receita.IdReceitas,
                NomeReceita = dto.NomeReceita,
                Ingredientes = dto.Ingredientes,
                ModoPreparo = dto.ModoPreparo,
                Calorias = dto.Calorias,
                Proteinas = dto.Proteinas,
                Carboidratos = dto.Carboidratos,
                Gordura = dto.Gordura
            });
        }
    }
}