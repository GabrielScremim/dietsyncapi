using dietsync.DTOs;
using dietsync.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace dietsync.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequestDTO dto)
        {
            await _service.Create(dto);
            return Ok(new { message = "Usuário criado com sucesso" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(ulong id)
        {
            var user = await _service.GetById(id);
            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> getByEmail(string email)
        {
            var user = await _service.GetByEmail(email);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, UpdateUserDto dto)
        {
            await _service.Update(id, dto);
            return Ok(new { message = "Usuário atualizado" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            await _service.Delete(id);
            return Ok(new { message = "Usuário removido" });
        }
    }
}