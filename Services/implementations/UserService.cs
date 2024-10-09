using System.IO;
using System.Text.Json;
using Operacao_curiosidade_API.Models;
using Operacao_curiosidade_API.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Operacao_curiosidade_API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly string _filePath = Path.Combine("C:\\Users\\LeonardoFerreiraGiro\\Documents\\Operacao-curiosidade-API", "Data", "users.json");


        public UserService()
        {
            // Certifique-se de que o arquivo JSON exista
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Cria um novo arquivo JSON com um array vazio
            }
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await ReadFromFileAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            var users = await ReadFromFileAsync();
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            var users = await ReadFromFileAsync();
            user.Id = Guid.NewGuid(); // Gera um novo ID
            users.Add(user);
            await WriteToFileAsync(users);
            return user;
        }

        public async Task<UserModel> UpdateUserAsync(Guid id, UserModel user)
        {
            var users = await ReadFromFileAsync();
            var existingUser = users.FirstOrDefault(u => u.Id == id);

            if (existingUser != null)
            {
                existingUser.Password = user.Password; // Atualiza a senha
                existingUser.FactsAndData = user.FactsAndData; // Atualiza os dados

                // Atualiza as curiosidades
                existingUser.Interests = user.Interests; // Atualiza os interesses
                existingUser.Feelings = user.Feelings; // Atualiza os sentimentos
                existingUser.Values = user.Values; // Atualiza os valores

                await WriteToFileAsync(users);
                return existingUser;
            }

            return null; // Retorna nulo se não encontrar o usuário
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var users = await ReadFromFileAsync();
            var userToDelete = users.FirstOrDefault(u => u.Id == id);

            if (userToDelete != null)
            {
                users.Remove(userToDelete); // Remove o usuário
                await WriteToFileAsync(users);
                return true;
            }

            return false; // Retorna falso se não encontrar o usuário
        }

        private async Task<List<UserModel>> ReadFromFileAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<UserModel>(); // Retorna uma lista vazia se o arquivo não existir
            }

            // Garante a leitura do arquivo com a codificação UTF-8
            using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Garante que acentos e caracteres especiais sejam aceitos
            };
            return await JsonSerializer.DeserializeAsync<List<UserModel>>(fileStream, options) ?? new List<UserModel>();
        }

        private async Task WriteToFileAsync(List<UserModel> users)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Formata o JSON com indentação
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Garante que acentos e caracteres especiais sejam aceitos
            };

            // Garante a escrita do arquivo com a codificação UTF-8
            using var fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, users, options);
        }

    }
}