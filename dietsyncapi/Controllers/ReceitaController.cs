using dietsync.Domain.Entities;
using dietsync.Domain.Interfaces;
using dietsync.DTOs;
using dietsync.Infrastructure.Data;
using dietsync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dietsync.Controllers
{
    [ApiController]
    [Route("/api/receita")]
    [Authorize]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaService _service;

        public ReceitaController(IReceitaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseReceitaDto>>> GetAll()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var receitas = await _service.GetAll(userId.Value);
            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ResponseReceitaDto>>> GetById(ulong id)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var receita = await _service.GetById(userId.Value, id);
            if (receita == null) return NotFound();

            return Ok(receita);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReceitaDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var id = await _service.Create(userId.Value, dto);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] UpdateDto dto)
        {
            var userId = GetUserId();

            if (userId == null) return Unauthorized();

            var updated = await _service.Update(userId.Value, id, dto);
            if (updated == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var deleted = await _service.DeleteAsync(userId.Value, id);
            if (!deleted) return NotFound();

            return NoContent();
        }
        // ================= CLAIM USER ID =================
        private ulong? GetUserId()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier) ??
                User.FindFirst("sub");

            if (userIdClaim == null)
                return null;

            if (!ulong.TryParse(userIdClaim.Value, out var userId))
                return null;

            return userId;
        }
    }
}
