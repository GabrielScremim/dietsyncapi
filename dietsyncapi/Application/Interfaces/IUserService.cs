using dietsync.DTOs;

public interface IUserService
{
    Task<UserDto> GetById(ulong id);
    Task<UserDto> GetByEmail(string email);

    Task Create(CreateUserRequestDTO dto);

    Task Update(ulong id, UpdateUserDto dto);

    Task Delete(ulong id);
}