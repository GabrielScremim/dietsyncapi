using dietsync.Domain.Entities;

public interface IUserRepository
{
    Task<User> GetByIdAsync(ulong id);

    Task<User> GetByEmailAsync(string email);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(User user);
}
