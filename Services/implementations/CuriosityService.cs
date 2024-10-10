using System;
using System.Threading.Tasks;
using Operacao_curiosidade_API.Models.DTO; // Certifique-se de que o namespace está correto
using Operacao_curiosidade_API.Services.Interfaces;
using Operacao_curiosidade_API.Models; // Para acesso aos modelos

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
                var curiosityDto = new CuriosityDTO();
                
                // Adiciona os interesses, sentimentos e valores ao CuriosityDTO
                foreach (var interest in user.Interests)
                {
                    curiosityDto.AddInterest(interest); // Usando o método para adicionar
                }

                foreach (var feeling in user.Feelings)
                {
                    curiosityDto.AddFeeling(feeling); // Usando o método para adicionar
                }

                foreach (var value in user.Values)
                {
                    curiosityDto.AddValue(value); // Usando o método para adicionar
                }

                return curiosityDto; // Retorna o CuriosityDTO preenchido
            }

            return null; // Retorna nulo se o usuário não for encontrado
        }

        // Atualizar curiosidades do usuário
        public async Task<bool> UpdateCuriositiesAsync(Guid userId, CuriosityDTO curiosityDto)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                // Limpa os interesses, sentimentos e valores existentes
                user.Interests.Clear(); 
                user.Feelings.Clear();
                user.Values.Clear();

                // Adiciona novos interesses, sentimentos e valores usando métodos do CuriosityDTO
                foreach (var interest in curiosityDto.Interests)
                {
                    user.AddInterest(interest); // Adiciona novo interesse via método
                }

                foreach (var feeling in curiosityDto.Feelings)
                {
                    user.AddFeeling(feeling); // Adiciona novo sentimento via método
                }

                foreach (var value in curiosityDto.Values)
                {
                    user.AddValue(value); // Adiciona novo valor via método
                }

                await _userService.UpdateUserAsync(userId, user); // Atualiza os dados do usuário
                return true;
            }

            return false; // Retorna falso se o usuário não for encontrado
        }
    }
}