using AwesomeDevEvents.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Swashbuckle.AspNetCore.Annotations;  // Para habilitar anotações do Swagger

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/curiosity")]
    [ApiController]
    public class CuriosityController : ControllerBase
    {
        private readonly string _curiosityFilePath = "curiosity_operations.json";  // Caminho do arquivo JSON das operações
        private readonly string _usersFilePath = "users.json";  // Caminho do arquivo JSON dos usuários

        // POST: api/curiosity
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova operação de curiosidade para um usuário existente.")]
        public IActionResult Post([FromQuery] Guid userId, [FromBody] CuriosityOperation input)
        {
            // Carrega os usuários cadastrados do arquivo users.json
            var users = LoadUsersFromFile();
            if (users == null || !users.Any())
            {
                return BadRequest("Nenhum usuário encontrado.");
            }

            // Verifica se o usuário existe pelo Id
            var user = users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }

            // Carrega as operações de curiosidade do arquivo curiosity_operations.json
            var curiosityOperations = LoadCuriosityOperationsFromFile();

            // Criação de uma nova operação de curiosidade
            var curiosityOperation = new CuriosityOperation
            {
                Id = Guid.NewGuid(),  // Gera um novo ID único
                FactsAndData = user.FactsAndData,  // Puxa os Fatos e Dados do usuário associado
                Interests = input.Interests ?? new List<Interests>(),  // Pode adicionar interesses mais tarde
                Feelings = input.Feelings ?? new List<Feelings>(),  // Pode adicionar sentimentos mais tarde
                Values = input.Values ?? new List<Values>(),  // Pode adicionar valores mais tarde
                IsDeleted = false  // Indica que a operação está ativa
            };

            // Adiciona a nova operação à lista
            curiosityOperations.Add(curiosityOperation);

            // Salva a lista atualizada de operações no arquivo curiosity_operations.json
            SaveCuriosityOperationsToFile(curiosityOperations);

            return CreatedAtAction("GetById", new { id = curiosityOperation.Id }, curiosityOperation);
        }

        // Método para carregar os usuários do arquivo users.json
        private List<UserModel> LoadUsersFromFile()
        {
            // Verifica se o arquivo existe
            if (!System.IO.File.Exists(_usersFilePath))
            {
                return new List<UserModel>();  // Retorna uma lista vazia se o arquivo não existir
            }

            // Lê o conteúdo do arquivo JSON
            var jsonData = System.IO.File.ReadAllText(_usersFilePath);

            // Desserializa os dados do JSON para uma lista de usuários
            return JsonSerializer.Deserialize<List<UserModel>>(jsonData);
        }

        // Método para carregar as operações de curiosidade do arquivo curiosity_operations.json
        private List<CuriosityOperation> LoadCuriosityOperationsFromFile()
        {
            // Verifica se o arquivo existe
            if (!System.IO.File.Exists(_curiosityFilePath))
            {
                return new List<CuriosityOperation>();  // Retorna uma lista vazia se o arquivo não existir
            }

            // Lê o conteúdo do arquivo JSON
            var jsonData = System.IO.File.ReadAllText(_curiosityFilePath);

            // Desserializa os dados do JSON para uma lista de operações de curiosidade
            return JsonSerializer.Deserialize<List<CuriosityOperation>>(jsonData);
        }

        // Método para salvar as operações de curiosidade no arquivo curiosity_operations.json
        private void SaveCuriosityOperationsToFile(List<CuriosityOperation> curiosityOperations)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Formata o JSON com indentação para facilitar a leitura
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Permitir caracteres Unicode
            };

            // Serializa a lista de operações de curiosidade de volta para JSON
            var jsonData = JsonSerializer.Serialize(curiosityOperations, options);

            // Escreve o conteúdo JSON no arquivo
            System.IO.File.WriteAllText(_curiosityFilePath, jsonData);
        }
    }
}
