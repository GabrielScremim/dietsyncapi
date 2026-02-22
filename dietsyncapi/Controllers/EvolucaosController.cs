using dietsync.Domain.Entities;
using dietsync.Infrastructure.Data;
using dietsync.Models;
using dietsyncapi.Application.Interfaces.IEvolucao;
using dietsyncapi.DTOs.Evolucao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dietsyncapi.Controllers
{

    [ApiController]
    [Route("api/evolucao")]
    [Authorize]
    public class EvolucaosController : Controller
    {
        private readonly IEvolucaoService _service;

        public EvolucaosController(IEvolucaoService context)
        {
            _service = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseEvolucaoDto>>> GetEvolucaos()
        {
            var UserId = GetUserId();
            if (UserId == null) return Unauthorized();

            return Ok(await _service.GetAll(UserId.Value));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseEvolucaoDto>> GetEvolucaoById(ulong id)
        {
            var UserId = GetUserId();
            if (UserId == null) return Unauthorized();

            var evolucao = await _service.GetById(UserId.Value, id);
            if (evolucao == null) return NotFound();
            return Ok(evolucao);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseEvolucaoDto>> CreateEvolucao([FromBody] CreateEvolucaoDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            var evolucao = await _service.Create(userId.Value, dto);
            return CreatedAtAction(nameof(GetEvolucaoById), new { id = evolucao.Id }, evolucao);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseEvolucaoDto>> UpdateEvolucao(ulong id, [FromBody] UpdateEvolucaoDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            var responseDto = await _service.Update(userId.Value, id, dto);
            return Ok(responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvolucao(ulong id)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            var deleted = await _service.Delete(userId.Value, id);
            if (!deleted) return NotFound();

            return NoContent();
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
