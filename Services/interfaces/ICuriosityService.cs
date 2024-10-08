using Operacao_curiosidade_API.Models;
using Operacao_curiosidade_API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Operacao_curiosidade_API.Services.Interfaces
{
    public interface ICuriosityService
    {
        // Buscar curiosidades associadas a um usuário (retorna CuriosityDTO)
        Task<CuriosityDTO> GetCuriositiesByUserAsync(Guid userId);
        
        // Atualizar curiosidades do usuário (recebe CuriosityDTO)
        Task<bool> UpdateCuriositiesAsync(Guid userId, CuriosityDTO curiosityDto);
    }
}