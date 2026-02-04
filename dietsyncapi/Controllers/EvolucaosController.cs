using dietsync.Domain.Entities;
using dietsync.Infrastructure.Data;
using dietsyncapi.DTOs.Evolucao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dietsyncapi.Controllers
{

    [ApiController]
    [Route("api/evolucao")]
    public class EvolucaosController : Controller
    {
        private readonly AppDbContext _context;

        public EvolucaosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseEvolucaoDto>>> GetEvolucaos()
        {
            var UserId = GetUserId();

            var evolucaos = await _context.Evolucaos
                .Where(e => e.FkIdEvolucaos == UserId)
                .Select(e => new ResponseEvolucaoDto
                {
                    Id = e.Id,
                    Data = e.Data,
                    Peso = e.Peso,
                    Altura = e.Altura,
                    Cintura = e.Cintura
                }).ToListAsync();

            return Ok(evolucaos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseEvolucaoDto>> GetEvolucaoById(ulong id)
        {
            var UserId = GetUserId();
            var evolucao = await _context.Evolucaos
                .Where(e => e.FkIdEvolucaos == UserId && e.Id == id)
                .Select(e => new ResponseEvolucaoDto
                {
                    Id = e.Id,
                    Data = e.Data,
                    Peso = e.Peso,
                    Altura = e.Altura,
                    Cintura = e.Cintura
                }).FirstOrDefaultAsync();

            if (evolucao == null)
                return NotFound();

            return Ok(evolucao);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseEvolucaoDto>> CreateEvolucao([FromBody] CreateEvolucaoDto dto)
        {
            var userId = GetUserId();
            var evolucao = new Evolucao
            {
                FkIdEvolucaos = userId,
                Data = dto.Data,
                Peso = dto.Peso,
                Altura = dto.Altura,
                Cintura = dto.Cintura
            };
            _context.Evolucaos.Add(evolucao);
            await _context.SaveChangesAsync();
            var responseDto = new ResponseEvolucaoDto
            {
                Id = evolucao.Id,
                Data = evolucao.Data,
                Peso = evolucao.Peso,
                Altura = evolucao.Altura,
                Cintura = evolucao.Cintura
            };
            return CreatedAtAction(nameof(GetEvolucaoById), new { id = evolucao.Id }, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseEvolucaoDto>> UpdateEvolucao(ulong id, [FromBody] UpdateEvolucaoDto dto)
        {
            var userId = GetUserId();
            var evolucao = await _context.Evolucaos
                .FirstOrDefaultAsync(e => e.Id == id && e.FkIdEvolucaos == userId);
            if (evolucao == null)
                return NotFound();
            evolucao.Data = dto.Data;
            evolucao.Peso = dto.Peso;
            evolucao.Altura = dto.Altura;
            evolucao.Cintura = dto.Cintura;
            await _context.SaveChangesAsync();
            var responseDto = new ResponseEvolucaoDto
            {
                Id = evolucao.Id,
                Data = evolucao.Data,
                Peso = evolucao.Peso,
                Altura = evolucao.Altura,
                Cintura = evolucao.Cintura
            };
            return Ok(responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvolucao(ulong id)
        {
            var userId = GetUserId();
            var evolucao = await _context.Evolucaos
                .FirstOrDefaultAsync(e => e.Id == id && e.FkIdEvolucaos == userId);
            if (evolucao == null)
                return NotFound();
            _context.Evolucaos.Remove(evolucao);
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
