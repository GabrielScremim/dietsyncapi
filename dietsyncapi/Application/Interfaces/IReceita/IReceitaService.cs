using dietsync.DTOs;

namespace dietsync.Domain.Interfaces
{
    public interface IReceitaService
    {
        Task<List<ResponseReceitaDto>> GetAll(ulong userId);
        Task<ResponseReceitaDto?> GetById(ulong userId, ulong id);
        Task<ResponseReceitaDto> Create(ulong userId, CreateReceitaDto dto);
        Task<ResponseReceitaDto> Update(ulong userId, ulong id, UpdateDto dto);
        Task<bool> DeleteAsync(ulong userId, ulong id);
    }
}