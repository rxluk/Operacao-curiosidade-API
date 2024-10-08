using Operacao_curiosidade_API.Models;

namespace Operacao_curiosidade_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync(); // Adicione esta linha
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task<UserModel> CreateUserAsync(UserModel user);
        Task<UserModel> UpdateUserAsync(Guid id, UserModel user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}