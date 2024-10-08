using Microsoft.AspNetCore.Mvc;
using Operacao_curiosidade_API.Models.DTO;
using Operacao_curiosidade_API.Services.Interfaces;

namespace Operacao_curiosidade_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuriosityController : ControllerBase
    {
        private readonly ICuriosityService _curiosityService;

        public CuriosityController(ICuriosityService curiosityService)
        {
            _curiosityService = curiosityService;
        }

        // Buscar curiosidades associadas a um usuário
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCuriositiesByUser(Guid userId)
        {
            var curiosity = await _curiosityService.GetCuriositiesByUserAsync(userId);
            
            if (curiosity == null)
            {
                return NotFound(); // Retorna 404 se não encontrar o usuário
            }

            return Ok(curiosity); // Retorna 200 com os dados da curiosidade
        }

        // Atualizar curiosidades do usuário
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCuriosities(Guid userId, [FromBody] CuriosityDTO curiosityDto)
        {
            var result = await _curiosityService.UpdateCuriositiesAsync(userId, curiosityDto);

            if (!result)
            {
                return NotFound(); // Retorna 404 se não encontrar o usuário
            }

            return NoContent(); // Retorna 204 se a atualização foi bem-sucedida
        }
    }
}