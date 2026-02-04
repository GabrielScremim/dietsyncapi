using dietsync.Domain.Entities;
using dietsync.DTOs;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task Create(CreateUserRequestDTO dto)
    {
        var exists = await _repo.GetByEmailAsync(dto.Email);

        if (exists != null)
            throw new Exception("Email já cadastrado");

        var user = new User
        {
            Name = dto.Name,
            Sobrenome = dto.Sobrenome,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            DataNasc = dto.DataNasc,
            Peso = dto.Peso,
            Altura = dto.Altura,
            Sexo = dto.Sexo,
            Meta = dto.Meta
        };

        await _repo.CreateAsync(user);
    }

    public async Task<UserDto> GetById(ulong id)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Sobrenome = user.Sobrenome,
            Email = user.Email,
            DataNasc = user.DataNasc,
            Peso = user.Peso,
            Altura = user.Altura,
            Sexo = user.Sexo,
            Meta = user.Meta
        };
    }

    public async Task<UserDto> GetByEmail(string email)
    {
        var user = await _repo.GetByEmailAsync(email);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Sobrenome = user.Sobrenome,
            Email = user.Email,
            DataNasc = user.DataNasc,
            Peso = user.Peso,
            Altura = user.Altura,
            Sexo = user.Sexo,
            Meta = user.Meta
        };
    }

    public async Task Update(ulong id, UpdateUserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        user.Name = dto.Name;
        user.Sobrenome = dto.Sobrenome;
        user.Peso = dto.Peso;
        user.Altura = dto.Altura;
        user.Meta = dto.Meta;

        await _repo.UpdateAsync(user);
    }

    public async Task Delete(ulong id)
    {
        var user = await _repo.GetByIdAsync(id);

        if (user == null)
            throw new Exception("Usuário não encontrado");

        await _repo.DeleteAsync(user);
    }
}
