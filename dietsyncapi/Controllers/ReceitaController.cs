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
    [Route("/api/receita")]
    //[Authorize]
    public class ReceitaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReceitaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseReceitaDto>>> GetAll()
        {
            var userId = GetUserId();

            var receitas = await _context.Receita
            .Where(r => r.FkIdUserReceita == userId)
            .Select(r => new ResponseReceitaDto
            {
                IdReceitas = r.IdReceitas,
                NomeReceita = r.NomeReceita,
                Ingredientes = r.Ingredientes,
                ModoPreparo = r.ModoPreparo,
                Calorias = r.Calorias,
                Proteinas = r.Proteinas,
                Carboidratos = r.Carboidratos,
                Gordura = r.Gordura,
            }).ToListAsync();

            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ResponseReceitaDto>>> GetById(ulong id)
        {
            var UserId = GetUserId();

            var receita = await _context.Receita
            .Where(r => r.FkIdUserReceita == UserId && r.IdReceitas == id)
            .Select(r => new ResponseReceitaDto
            {
                IdReceitas = r.IdReceitas,
                NomeReceita = r.NomeReceita,
                Ingredientes = r.Ingredientes,
                ModoPreparo = r.ModoPreparo,
                Calorias = r.Calorias,
                Proteinas = r.Proteinas,
                Carboidratos = r.Carboidratos,
                Gordura = r.Gordura,
            }).FirstOrDefaultAsync();

            return Ok(receita);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReceitaDto dto)
        {
            var userId = GetUserId();
            var receita = new Receitum
            {
                NomeReceita = dto.NomeReceita,
                Ingredientes = dto.Ingredientes,
                ModoPreparo = dto.ModoPreparo,
                Calorias = dto.Calorias,
                Proteinas = dto.Proteinas,
                Carboidratos = dto.Carboidratos,
                Gordura = dto.Gordura,
                FkIdUserReceita = userId,
            };
            _context.Receita.Add(receita);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = receita.IdReceitas }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] UpdateDto dto)
        {
            var userId = GetUserId();

            if (userId == null)
                return Unauthorized("Usuário não atualizado");
            var receita = await _context.Receita
            .FirstOrDefaultAsync(r => r.FkIdUserReceita == userId && r.IdReceitas == id);

            if (receita == null)
                return NotFound();

            receita.NomeReceita = dto.NomeReceita;
            receita.Ingredientes = dto.Ingredientes;
            receita.ModoPreparo = dto.ModoPreparo;
            receita.Calorias = dto.Calorias;
            receita.Proteinas = dto.Proteinas;
            receita.Carboidratos = dto.Carboidratos;
            receita.Gordura = dto.Gordura;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var userId = GetUserId();
            var receita = await _context.Receita.FirstOrDefaultAsync(r => r.FkIdUserReceita == userId && r.IdReceitas == id);

            if (receita == null)
                return NotFound();

            _context.Receita.Remove(receita);

            await _context.SaveChangesAsync();
            return NoContent()
;
        }
        // ================= CLAIM USER ID =================
         private ulong? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub");

            if (userIdClaim == null)
                return null;

            if (!ulong.TryParse(userIdClaim.Value, out var userId))
                return null;

            return userId;
        }
    }
}
