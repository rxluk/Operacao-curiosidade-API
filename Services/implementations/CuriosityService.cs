using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Operacao_curiosidade_API.Models;
using Operacao_curiosidade_API.Services.Interfaces;

namespace Operacao_curiosidade_API.Services.Implementations
{
    public class CuriosityService : ICuriosityService
    {
        private readonly string _filePath = Path.Combine("D:\\Área de Trabalho\\help\\Operacao-curiosidade-API", "Data", "curiosities.json");

        public CuriosityService()
        {
            // Certifique-se de que o diretório exista
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory); // Cria o diretório se não existir
            }

            // Certifique-se de que o arquivo JSON exista
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Cria um novo arquivo JSON com um array vazio
            }
        }

        // Buscar todas as curiosidades
        public async Task<List<CuriosityOperation>> GetAllCuriositiesAsync()
        {
            var curiosities = await ReadFromFileAsync();
            return curiosities;
        }

        // Buscar curiosidades associadas a um usuário
        public async Task<List<CuriosityOperation>> GetCuriositiesByUserAsync(Guid userId)
        {
            var curiosities = await ReadFromFileAsync();
            return curiosities.Where(c => c.UserId == userId).ToList(); // Use UserId
        }

        // Criar uma nova curiosidade
        public async Task<CuriosityOperation> CreateCuriosityAsync(CuriosityOperation curiosity)
        {
            var curiosities = await ReadFromFileAsync();
            curiosities.Add(curiosity); // Adiciona a curiosidade com o UserModel
            await WriteToFileAsync(curiosities);
            return curiosity;
        }

        // Atualizar curiosidade com base no usuário
        public async Task<CuriosityOperation> UpdateCuriosityAsync(Guid userId, CuriosityOperation curiosity)
        {
            var curiosities = await ReadFromFileAsync();
            var existingCuriosity = curiosities.FirstOrDefault(c => c.UserId == userId); // Use UserId

            if (existingCuriosity != null)
            {
                existingCuriosity.Update(curiosity.FactsAndData, curiosity.Interests, curiosity.Feelings, curiosity.Values);
                await WriteToFileAsync(curiosities);
                return existingCuriosity;
            }

            return null; // Retorna nulo se não encontrar a curiosidade
        }

        // Deletar curiosidade associada ao usuário
        public async Task<bool> DeleteCuriosityAsync(Guid userId)
        {
            var curiosities = await ReadFromFileAsync();
            var curiosityToDelete = curiosities.FirstOrDefault(c => c.UserId == userId); // Use UserId

            if (curiosityToDelete != null)
            {
                curiosities.Remove(curiosityToDelete); // Remove a curiosidade
                await WriteToFileAsync(curiosities);
                return true;
            }

            return false; // Retorna falso se não encontrar a curiosidade
        }

        // Leitura do arquivo JSON
        private async Task<List<CuriosityOperation>> ReadFromFileAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<CuriosityOperation>(); // Retorna uma lista vazia se o arquivo não existir
            }

            using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            return await JsonSerializer.DeserializeAsync<List<CuriosityOperation>>(fileStream) ?? new List<CuriosityOperation>();
        }

        // Escrita no arquivo JSON
        private async Task WriteToFileAsync(List<CuriosityOperation> curiosities)
        {
            using var fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            await JsonSerializer.SerializeAsync(fileStream, curiosities);
        }
    }
}