using dietsync.Domain.Entities;
using dietsync.DTOs;
using dietsync.Infrastructure.Data;
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
        private readonly AppDbContext _context;
        public DietaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseDietaDto>>> GetAll()
        {
            var userId = GetUserId();

            var dietas = await _context.Dietas
            .Where(d => d.FkIdUsuarioDieta == userId)
            .Select(d => new ResponseDietaDto
            {
                IdDieta = d.IdDieta,
                NomeDieta = d.NomeDieta,
                TipoDieta = d.TipoDieta,
                Calorias = d.Calorias,
                Proteinas = d.Proteinas,
                Carboidratos = d.Carboidratos,
                Gorduras = d.Gorduras,
                DataDieta = d.DataDieta,
                Refeicao = d.Refeicao,
                Alimentos = d.Alimentos,
                Quantidade = d.Quantidade,
                Observacoes = d.Observacoes,
            }).ToListAsync();

            return Ok(dietas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDietaDto>> GetById(ulong id)
        {
            var userId = GetUserId();

            var dieta = await _context.Dietas
            .Where(d => d.FkIdUsuarioDieta == userId && d.IdDieta == id).
            Select(d => new ResponseDietaDto
            {
                IdDieta = d.IdDieta,
                NomeDieta = d.NomeDieta,
                TipoDieta = d.TipoDieta,
                Calorias = d.Calorias,
                Proteinas = d.Proteinas,
                Carboidratos = d.Carboidratos,
                Gorduras = d.Gorduras,
                DataDieta = d.DataDieta,
                Refeicao = d.Refeicao,
                Alimentos = d.Alimentos,
                Quantidade = d.Quantidade,
                Observacoes = d.Observacoes,
            }).ToListAsync();

            if (dieta == null)
                return NotFound();

            return Ok(dieta);

        }

        [HttpPost]
        public async Task<ActionResult<CreateDietaDto>> Create([FromBody] CreateDietaDto dto)
        {
            var userId = GetUserId();
            var dieta = new Dieta
            {
                NomeDieta = dto.NomeDieta,
                TipoDieta = dto.TipoDieta,
                Calorias = dto.Calorias,
                Proteinas = dto.Proteinas,
                Carboidratos = dto.Carboidratos,
                Gorduras = dto.Gorduras,
                DataDieta = dto.DataDieta,
                Refeicao = dto.Refeicao,
                Alimentos = dto.Alimentos,
                Quantidade = dto.Quantidade,
                Observacoes = dto.Observacoes,
                FkIdUsuarioDieta = dto.FkIdUsuarioDieta,
            };

            _context.Dietas.Add(dieta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dieta.IdDieta }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateDietaDto>> Update(ulong id, [FromBody] UpdateDietaDto dto)
        {
            var userId = GetUserId();

            var dieta = await _context.Dietas
            .FirstOrDefaultAsync(d => d.IdDieta == id && d.FkIdUsuarioDieta == userId);

            if (dieta == null)
                return NotFound();

            dieta.NomeDieta = dto.NomeDieta;
            dieta.TipoDieta = dto.TipoDieta;
            dieta.Calorias = dto.Calorias;
            dieta.Proteinas = dto.Proteinas;
            dieta.Carboidratos = dto.Carboidratos;
            dieta.Gorduras = dto.Gorduras;
            dieta.DataDieta = dto.DataDieta;
            dieta.Refeicao = dto.Refeicao;
            dieta.Alimentos = dto.Alimentos;
            dieta.Quantidade = dto.Quantidade;
            dieta.Observacoes = dto.Observacoes;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var userId = GetUserId();

            var dieta = await _context.Dietas.FirstOrDefaultAsync(d => d.FkIdUsuarioDieta == userId && d.IdDieta == id);

            if (dieta == null)
                return NotFound();

            _context.Dietas.Remove(dieta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private ulong GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim == null)
                throw new Exception("Usuário não autenticado");

            return ulong.Parse(userIdClaim.Value);
        }
    }
}
