using dietsync.Applicattion.DTOs.User;

public interface IUserService
{
    Task<UserResponseDto> GetById(ulong id);

    Task Create(UserCreateDto dto);

    Task Update(ulong id, UserUpdateDto dto);

    Task Delete(ulong id);
}