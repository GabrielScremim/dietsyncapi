using dietsync.DTOs;
using dietsync.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace dietsync.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/api/user")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Sobrenome = u.Sobrenome,
                Email = u.Email,
                Sexo = u.Sexo,
                DataNasc = u.DataNasc,
                Peso = u.Peso,
                Altura = u.Altura,
                Meta = u.Meta
            }).ToListAsync();
            return Ok(users);
        }

        [HttpGet("/api/user/{id}")]
        public async Task<ActionResult<List<UserDto>>> GetById(ulong id)
        {
            var user = await _context.Users
         .Where(u => u.Id == id)
         .Select(u => new UserDto
         {
             Id = u.Id,
             Name = u.Name,
             Sobrenome = u.Sobrenome,
             Email = u.Email,
             Sexo = u.Sexo,
             DataNasc = u.DataNasc,
             Peso = u.Peso,
             Altura = u.Altura,
             Meta = u.Meta
         }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }

        [HttpPost("/api/user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO dto)
        {
            // Verifica email duplicado
            bool emailExiste = await _context.Users
                .AnyAsync(u => u.Email == dto.Email);

            if (emailExiste)
                return BadRequest("Email já cadastrado");

            var user = new User
            {
                Name = dto.Name,
                Sobrenome = dto.Sobrenome,
                Meta = dto.Meta,
                Sexo = dto.Sexo,
                DataNasc = dto.DataNasc,
                Peso = dto.Peso,
                Altura = dto.Altura,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Sobrenome = user.Sobrenome,
                Email = user.Email,
                Sexo = user.Sexo,
                DataNasc = user.DataNasc,
                Peso = user.Peso,
                Altura = user.Altura,
                Meta = user.Meta
            };

            return CreatedAtAction(nameof(GetById),
                new { id = user.Id },
                response);
        }

        [HttpPut("/api/user/{id}")]
        public async Task<IActionResult> UpdateUser(ulong id, UpdateUserDto user)
        {
            var usrToUpdate = await _context.Users.FindAsync(id);

            if (usrToUpdate == null)
            {
                return NotFound(new { message = "User not found." });
            }

            usrToUpdate.Name = user.Name;
            usrToUpdate.Sobrenome = user.Sobrenome;
            usrToUpdate.Email = user.Email;
            usrToUpdate.DataNasc = user.DataNasc;
            usrToUpdate.Altura = user.Altura;
            usrToUpdate.Meta = user.Meta;
            usrToUpdate.Sexo = user.Sexo;
            usrToUpdate.Peso = user.Peso;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("/api/user/{id}")]
        public async Task<IActionResult> DeleteUser(ulong id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}