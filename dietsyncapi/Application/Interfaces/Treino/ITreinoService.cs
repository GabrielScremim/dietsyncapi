using dietsync.Domain.Entities;
using dietsync.DTOs;
public interface ITreinoService
{
    Task<List<TreinoResponseDTO>> GetAllAsync(ulong userId);
    Task<TreinoResponseDTO?> GetByIdAsync(ulong id, ulong userId);
    Task<TreinoResponseDTO> CreateAsync(CreateTreinoDTO dto, ulong userId);
    Task<bool> UpdateAsync(ulong id, UpdateTreinoDto dto, ulong userId);
    Task<bool> DeleteAsync(ulong id, ulong userId);
}