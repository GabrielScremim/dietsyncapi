using dietsync.DTOs;
using dietsync.Models;
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
        private ulong GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

            if (userIdClaim == null)
                throw new Exception("Usuário não autenticado");

            return ulong.Parse(userIdClaim.Value);
        }
    }
}
