using dietsync.Domain.Entities;
using dietsync.DTOs;
using dietsync.Infrastructure.Data;
using dietsyncapi.Application.Interfaces.IDieta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dietsync.Controllers
{
    [ApiController]
    [Route("/api/dieta")]
    [Authorize]
    public class DietaController : Controller
    {
        private readonly IDietaService _context;
        public DietaController(IDietaService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseDietaDto>>> GetAll()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();
            var dietas = await _context.GetAll(userId.Value);
            return Ok(dietas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDietaDto>> GetById(ulong id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();
            var dieta = await _context.GetById(userId.Value, id);
            return Ok(dieta);

        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDietaDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();
            var dieta = await _context.Create(userId.Value, dto);
            return CreatedAtAction(nameof(GetById), new { id = dieta.IdDieta }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ulong id, [FromBody] UpdateDietaDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();
            await _context.Update(userId.Value, id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();
            await _context.Delete(id, userId.Value);
            return Ok(new { message = "Dieta removida" });
        }
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
