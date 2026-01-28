using Microsoft.EntityFrameworkCore;
using dietsync.DTOs;
using dietsync.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dietsync.Controllers
{
    [ApiController]
    [Route("api/treino")]
    [Authorize] // Garante que só usuário autenticado acessa
    public class TreinoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TreinoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TreinoResponseDTO>>> GetAll()
        {
            var userId = GetUserId();

            var treinos = await _context.Treinos
                .Where(t => t.FkIdUsuarioTreino == userId)
                .Select(t => new TreinoResponseDTO
                {
                    Data = t.Data,
                    Tipo = t.Tipo,
                    Exercicios = t.Exercicios,
                    Repeticoes = t.Repeticoes,
                    Series = t.Series,
                    Objetivo = t.Objetivo,
                    Duracao = t.Duracao,
                    Frequencia = t.Frequencia,
                    NomeTreino = t.NomeTreino,
                })
                .ToListAsync();

            return Ok(treinos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TreinoResponseDTO>>> GetById(ulong id)
        {
            var userId = GetUserId();

            var treino = await _context.Treinos
             .Where(t => t.Id == id && t.FkIdUsuarioTreino == userId)
                .Select(t => new TreinoResponseDTO
                {
                    Data = t.Data,
                    Tipo = t.Tipo,
                    Exercicios = t.Exercicios,
                    Repeticoes = t.Repeticoes,
                    Series = t.Series,
                    Objetivo = t.Objetivo,
                    Duracao = t.Duracao,
                    Frequencia = t.Frequencia,
                    NomeTreino = t.NomeTreino,
                })
                .FirstOrDefaultAsync();

            return Ok(treino);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTreinoDTO dto)
        {
            var userId = GetUserId();
            var treino = new Treino
            {
                Data = dto.Data,
                Tipo = dto.Tipo,
                Exercicios = dto.Exercicios,
                Repeticoes = dto.Repeticoes,
                Series = dto.Series,
                Objetivo = dto.Objetivo,
                Duracao = dto.Duracao,
                Frequencia = dto.Frequencia,
                NomeTreino = dto.NomeTreino,
                DiaTreino = dto.DiaTreino,
                FkIdUsuarioTreino = userId
            };

            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = treino.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ulong id, [FromBody] UpdateTreinoDto dto)
        {
            var userId = GetUserId();

            var treino = await _context.Treinos.
            FirstOrDefaultAsync(t => t.Id == id && t.FkIdUsuarioTreino == userId);

            if (treino == null)
                return NotFound("Treino não encontrado");

            treino.Data = dto.Data;
            treino.Tipo = dto.Tipo;
            treino.Exercicios = dto.Exercicios;
            treino.Repeticoes = dto.Repeticoes;
            treino.Series = dto.Series;
            treino.Objetivo = dto.Objetivo;
            treino.Duracao = dto.Duracao;
            treino.Frequencia = dto.Frequencia;
            treino.NomeTreino = dto.NomeTreino;
            treino.DiaTreino = dto.DiaTreino;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var userId = GetUserId();

            var treino = await _context.Treinos.
            FirstOrDefaultAsync(t => t.Id == id && t.FkIdUsuarioTreino == userId);

            if (treino == null)
                return NotFound();

            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ================= CLAIM USER ID =================
        private ulong GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim == null)
                throw new Exception("Usuário não autenticado");

            return ulong.Parse(userIdClaim.Value);
        }

        // ================= TESTE =================
        [HttpGet("test-user")]
        public IActionResult TestUser()
        {
            var userId = GetUserId();

            return Ok(new
            {
                UserId = userId,
                IsAuthenticated = User.Identity.IsAuthenticated
            });
        }
    }
}
