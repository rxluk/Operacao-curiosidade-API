using Operacao_curiosidade_API.Models;
namespace Operacao_curiosidade_API.Services.Interfaces
{
    public interface ICuriosityService
    {
        Task<List<CuriosityOperation>> GetAllCuriositiesAsync();
        Task<List<CuriosityOperation>> GetCuriositiesByUserAsync(Guid userId); // Buscar curiosidades por usuário
        Task<CuriosityOperation> CreateCuriosityAsync(CuriosityOperation curiosity);
        Task<CuriosityOperation> UpdateCuriosityAsync(Guid userId, CuriosityOperation curiosity); // Atualizar curiosidade por usuário
        Task<bool> DeleteCuriosityAsync(Guid userId); // Deletar curiosidade por usuário
    }
}