using Microsoft.AspNetCore.Mvc;
using Operacao_curiosidade_API.Models;
using Operacao_curiosidade_API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Operacao_curiosidade_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuriosityController : ControllerBase
    {
        private readonly ICuriosityService _curiosityService;

        public CuriosityController(ICuriosityService curiosityService)
        {
            _curiosityService = curiosityService;
        }

        // GET: api/Curiosity
        [HttpGet]
        public async Task<ActionResult<List<CuriosityOperation>>> GetAllCuriosities()
        {
            var curiosities = await _curiosityService.GetAllCuriositiesAsync();
            return Ok(curiosities);
        }

        // GET: api/Curiosity/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<CuriosityOperation>>> GetCuriositiesByUser(Guid userId)
        {
            var curiosities = await _curiosityService.GetCuriositiesByUserAsync(userId);
            if (curiosities == null || curiosities.Count == 0)
            {
                return NotFound($"No curiosities found for user {userId}");
            }

            return Ok(curiosities);
        }

        // POST: api/Curiosity
        [HttpPost]
        public async Task<ActionResult<CuriosityOperation>> CreateCuriosity(CuriosityOperation curiosity)
        {
            if (curiosity == null)
            {
                return BadRequest("Curiosity is null");
            }

            // Defina o UserId a partir do objeto User
            curiosity.UserId = curiosity.User.Id; // Adiciona o ID do usuário

            var createdCuriosity = await _curiosityService.CreateCuriosityAsync(curiosity);
            return CreatedAtAction(nameof(GetCuriositiesByUser), new { userId = createdCuriosity.UserId }, createdCuriosity);
        }

        // PUT: api/Curiosity/user/{userId}
        [HttpPut("user/{userId}")]
        public async Task<ActionResult<CuriosityOperation>> UpdateCuriosity(Guid userId, CuriosityOperation curiosity)
        {
            var updatedCuriosity = await _curiosityService.UpdateCuriosityAsync(userId, curiosity);
            if (updatedCuriosity == null)
            {
                return NotFound($"Curiosity for user {userId} not found");
            }

            return Ok(updatedCuriosity);
        }

        // DELETE: api/Curiosity/user/{userId}
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> DeleteCuriosity(Guid userId)
        {
            var result = await _curiosityService.DeleteCuriosityAsync(userId);
            if (!result)
            {
                return NotFound($"Curiosity for user {userId} not found");
            }

            return NoContent();
        }
    }
}