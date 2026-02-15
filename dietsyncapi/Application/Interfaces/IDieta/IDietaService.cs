using dietsync.DTOs;

namespace dietsyncapi.Application.Interfaces.IDieta
{
    public interface IDietaService
    {
        Task<List<ResponseDietaDto>> GetAll(ulong userId);
        Task<ResponseDietaDto> GetById(ulong userId, ulong id);
        Task<ResponseDietaDto> Create(ulong userId, CreateDietaDto dto);
        Task Update(ulong userId, ulong id, UpdateDietaDto dto);
        Task Delete(ulong userId, ulong id);
    }
}
