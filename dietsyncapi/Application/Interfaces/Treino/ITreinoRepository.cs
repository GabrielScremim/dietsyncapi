using dietsync.Domain.Entities;

public interface ITreinoRepository
{
    Task<List<Treino>> GetAllByUserAsync(ulong userId);
    Task<Treino?> GetByIdAsync(ulong id, ulong userId);
    Task AddAsync(Treino treino);
    Task UpdateAsync(Treino treino);
    Task DeleteAsync(Treino treino);
}