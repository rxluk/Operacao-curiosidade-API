using System;
using System.Threading.Tasks;
using Operacao_curiosidade_API.Models.DTO;
using Operacao_curiosidade_API.Services.Interfaces;

namespace Operacao_curiosidade_API.Services.Implementations
{
    public class CuriosityService : ICuriosityService
    {
        private readonly IUserService _userService; // Referência ao serviço de usuários

        public CuriosityService(IUserService userService)
        {
            _userService = userService;
        }

        // Buscar curiosidades associadas a um usuário (retorna como CuriosityDTO)
        public async Task<CuriosityDTO> GetCuriositiesByUserAsync(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                return new CuriosityDTO
                {
                    Interests = user.Interests,
                    Feelings = user.Feelings,
                    Values = user.Values
                };
            }

            return null; // Retorna nulo se o usuário não for encontrado
        }

        // Atualizar curiosidades do usuário
        public async Task<bool> UpdateCuriositiesAsync(Guid userId, CuriosityDTO curiosityDto)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                user.Interests = curiosityDto.Interests;
                user.Feelings = curiosityDto.Feelings;
                user.Values = curiosityDto.Values;

                await _userService.UpdateUserAsync(userId, user); // Atualiza os dados do usuário
                return true;
            }

            return false; // Retorna falso se o usuário não for encontrado
        }
    }
}