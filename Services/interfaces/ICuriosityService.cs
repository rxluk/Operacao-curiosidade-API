using Operacao_curiosidade_API.Models.DTO;
using System;
using System.Threading.Tasks;

namespace Operacao_curiosidade_API.Services.Interfaces
{
    /// <summary>
    /// Interface para serviços de curiosidade relacionados a usuários.
    /// </summary>
    public interface ICuriosityService
    {
        /// <summary>
        /// Busca as curiosidades associadas a um usuário pelo seu ID.
        /// </summary>
        Task<CuriosityDTO> GetCuriositiesByUserAsync(Guid userId);
        
        /// <summary>
        /// Atualiza as curiosidades de um usuário.
        /// </summary>
        Task<bool> UpdateCuriositiesAsync(Guid userId, CuriosityDTO curiosityDto);
    }
}