using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using dietsync.DTOs;

namespace dietsync.API.Controllers
{
    [ApiController]
    [Route("api/treino")]
    [Authorize] // Ative quando estiver usando autenticação
    public class TreinoController : ControllerBase
    {
        private readonly ITreinoService _service;

        public TreinoController(ITreinoService service)
        {
            _service = service;
        }

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<ActionResult<List<TreinoResponseDTO>>> GetAll()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var result = await _service.GetAllAsync(userId.Value);

            return Ok(result);
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<TreinoResponseDTO>> GetById(ulong id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var result = await _service.GetByIdAsync(id, userId.Value);

            if (result == null)
                return NotFound("Treino não encontrado");

            return Ok(result);
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<ActionResult<TreinoResponseDTO>> Create(CreateTreinoDTO dto)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var result = await _service.CreateAsync(dto, userId.Value);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            ulong id,
            [FromBody] UpdateTreinoDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var success = await _service.UpdateAsync(id, dto, userId.Value);

            if (!success)
                return NotFound("Treino não encontrado");

            return NoContent();
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ulong id)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var success = await _service.DeleteAsync(id, userId.Value);

            if (!success)
                return NotFound("Treino não encontrado");

            return NoContent();
        }

        // =========================
        // CLAIM USER ID
        // =========================
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
