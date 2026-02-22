using dietsyncapi.DTOs.Evolucao;

namespace dietsyncapi.Application.Interfaces.IEvolucao
{
    public interface IEvolucaoService
    {
        Task<List<ResponseEvolucaoDto>> GetAll(ulong userId);
        Task<ResponseEvolucaoDto?> GetById(ulong userId, ulong id);
        Task<ResponseEvolucaoDto> Create(ulong userId, CreateEvolucaoDto dto);
        Task<ResponseEvolucaoDto?> Update(ulong userId, ulong id, UpdateEvolucaoDto dto);
        Task<bool> Delete(ulong userId, ulong id);
    }
}
