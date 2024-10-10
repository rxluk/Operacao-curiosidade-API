using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Operacao_curiosidade_API.Models;
using Operacao_curiosidade_API.Services.Interfaces;

namespace Operacao_curiosidade_API.Services.Implementations
{
    public class CuriosityService : ICuriosityService
    {
        private readonly string _filePath = "curiosities.json"; // Exemplo de caminho do arquivo

        public async Task<List<CuriosityModel>> GetAllCuriositiesAsync()
        {
            var curiosities = await ReadFromFileAsync();
            return curiosities;
        }

        public async Task<CuriosityModel> GetCuriosityByIdAsync(Guid id)
        {
            var curiosities = await ReadFromFileAsync();
            return curiosities.FirstOrDefault(c => c.Id == id);
        }

        public async Task<CuriosityModel> AddCuriosityAsync(CuriosityModel curiosity)
        {
            var curiosities = await ReadFromFileAsync();
            curiosities.Add(curiosity);
            await WriteToFileAsync(curiosities);
            return curiosity;
        }

        public async Task<CuriosityModel> UpdateCuriosityAsync(Guid id, CuriosityModel updatedCuriosity)
        {
            var curiosities = await ReadFromFileAsync();
            var existingCuriosity = curiosities.FirstOrDefault(c => c.Id == id);

            if (existingCuriosity != null)
            {
                existingCuriosity.Title = updatedCuriosity.Title; // Exemplo de propriedade direta
                existingCuriosity.Description = updatedCuriosity.Description; // Exemplo de propriedade direta

                await WriteToFileAsync(curiosities);
                return existingCuriosity;
            }

            return null; // Retorna nulo se não encontrar a curiosidade
        }

        public async Task<bool> DeleteCuriosityAsync(Guid id)
        {
            var curiosities = await ReadFromFileAsync();
            var existingCuriosity = curiosities.FirstOrDefault(c => c.Id == id);

            if (existingCuriosity != null)
            {
                curiosities.Remove(existingCuriosity);
                await WriteToFileAsync(curiosities);
                return true;
            }

            return false; // Retorna falso se não encontrar a curiosidade
        }

        private async Task<List<CuriosityModel>> ReadFromFileAsync()
        {
            // Implementação para ler o arquivo e deserializar as curiosidades
            return new List<CuriosityModel>();
        }

        private async Task WriteToFileAsync(List<CuriosityModel> curiosities)
        {
            // Implementação para serializar e escrever as curiosidades no arquivo
        }
    }
}