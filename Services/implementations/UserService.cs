using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Operacao_curiosidade_API.Models; // Assegure-se de que o namespace correto está sendo usado
using Operacao_curiosidade_API.Services.Interfaces;

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
            UserModel foundUser = null; // Inicializa a variável

            // Usando um loop foreach para encontrar o usuário
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    foundUser = user;
                    break; // Sai do loop ao encontrar o usuário
                }
            }

            return foundUser; // Retorna o usuário encontrado ou null
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            var users = await ReadFromFileAsync();
            users.Add(user);
            await WriteToFileAsync(users);
            return user;
        }

        public async Task<UserModel> UpdateUserAsync(Guid id, UserModel user)
        {
            var users = await ReadFromFileAsync();
            UserModel existingUser = null; // Inicializa a variável

            // Usando um loop foreach para encontrar o usuário
            foreach (var u in users)
            {
                if (u.Id == id)
                {
                    existingUser = u;
                    break; // Sai do loop ao encontrar o usuário
                }
            }

            if (existingUser != null)
            {
                existingUser.SetPassword(user.Password); // Atualiza a senha
                existingUser.UpdateFactsAndData(user.FactsAndData); // Atualiza os dados
                existingUser.UpdateInterests(user.Interests); // Atualiza os interesses
                existingUser.UpdateFeelings(user.Feelings); // Atualiza os sentimentos
                existingUser.UpdateValues(user.Values); // Atualiza os valores

                await WriteToFileAsync(users);
                return existingUser;
            }

            return null; // Retorna nulo se não encontrar o usuário
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var users = await ReadFromFileAsync();
            UserModel userToDelete = null; // Inicializa a variável

            // Usando um loop foreach para encontrar o usuário a ser deletado
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    userToDelete = user;
                    break; // Sai do loop ao encontrar o usuário
                }
            }

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

            using var fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, users, options);
        }
    }
}
