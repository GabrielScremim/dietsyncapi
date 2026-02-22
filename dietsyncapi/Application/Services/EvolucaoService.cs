using dietsync.Domain.Entities;
using dietsyncapi.Application.Interfaces.IEvolucao;
using dietsyncapi.DTOs.Evolucao;

namespace dietsyncapi.Application.Services
{
    public class EvolucaoService : IEvolucaoService
    {
        private readonly IEvolucaoRepository _repo;

        public EvolucaoService(IEvolucaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseEvolucaoDto> Create(ulong userId, CreateEvolucaoDto dto)
        {
            var evolucao = new Evolucao
            {
                FkIdEvolucaos = userId,
                Data = dto.Data,
                Peso = dto.Peso,
                Altura = dto.Altura,
                Cintura = dto.Cintura
            };
            await _repo.AddAsync(evolucao);
            await _repo.SaveChangesAsync();

            var responseDto = new ResponseEvolucaoDto
            {
                Id = evolucao.Id,
                Peso = evolucao.Peso,
                Altura = evolucao.Altura,
                Cintura = evolucao.Cintura,
                Data = evolucao.Data
            };
            return await Task.FromResult(responseDto);
        }

        public async Task<bool> Delete(ulong userId, ulong id)
        {
            var eveolucao = await _repo.GetByIdAsync(userId, id);
            if (eveolucao == null)
                return false;
            await _repo.DeleteAsync(eveolucao);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<ResponseEvolucaoDto>> GetAll(ulong userId)
        {
            var evolucaos = await _repo.GetAllByUserIdAsync(userId);
            var responseDtos = evolucaos.Select(e => new ResponseEvolucaoDto
            {
                Id = e.Id,
                Data = e.Data,
                Peso = e.Peso,
                Altura = e.Altura,
                Cintura = e.Cintura
            }).ToList();
            return await Task.FromResult(responseDtos);
        }

        public async Task<ResponseEvolucaoDto?> GetById(ulong userId, ulong id)
        {
            var evolucao = await _repo.GetByIdAsync(userId, id);
            if (evolucao == null)
                return await Task.FromResult<ResponseEvolucaoDto?>(null);
            return new ResponseEvolucaoDto
            {
                Id = evolucao.Id,
                Peso = evolucao.Peso,
                Altura = evolucao.Altura,
                Cintura = evolucao.Cintura,
                Data = evolucao.Data
            };
        }

        public async Task<ResponseEvolucaoDto?> Update(ulong userId, ulong id, UpdateEvolucaoDto dto)
        {
            var evolucao = await _repo.GetByIdAsync(userId, id);
            if (evolucao == null)
                return await Task.FromResult<ResponseEvolucaoDto?>(null);

            evolucao.Data = dto.Data;
            evolucao.Peso = dto.Peso;
            evolucao.Altura = dto.Altura;
            evolucao.Cintura = dto.Cintura;

            await _repo.SaveChangesAsync();
            return new ResponseEvolucaoDto
            {
                Id = evolucao.Id,
                Peso = evolucao.Peso,
                Altura = evolucao.Altura,
                Cintura = evolucao.Cintura,
                Data = evolucao.Data
            };
        }
    }
}
