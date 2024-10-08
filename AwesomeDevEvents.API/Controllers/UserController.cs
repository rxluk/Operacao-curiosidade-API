using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;  // Para habilitar anotações do Swagger

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/user-controller")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly string _filePath = "users.json";  // Caminho do arquivo JSON

        public UserController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/user-controller
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém todos os usuários cadastrados.")]
        public IActionResult GetAll()
        {
            var users = _context.FactsAndData.Where(d => !d.IsDeleted).ToList();
            return Ok(users);
        }

        // GET: api/user-controller/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém um usuário por ID.")]
        public IActionResult GetById(Guid id)
        {
            var factsAndData = _context.FactsAndData.SingleOrDefault(d => d.Id == id);

            if (factsAndData == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            return Ok(factsAndData);
        }

        // POST: api/user-controller
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra um novo usuário.")]
        public IActionResult Post([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gera um novo UserModel com um ID e preenche FactsAndData
            userModel.Id = Guid.NewGuid();  // Atribui um novo ID
            if (userModel.FactsAndData != null)
            {
                userModel.FactsAndData.Id = userModel.Id;  // Associa o ID ao FactsAndData
            }

            // Adiciona o usuário ao contexto
            _context.FactsAndData.Add(userModel.FactsAndData);
            SaveDataToFile();  // Salva os dados no arquivo JSON

            return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel);
        }

        // PUT: api/user-controller/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza os dados de um usuário.")]
        public IActionResult Update(Guid id, [FromBody] FactsAndData input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var factsAndData = _context.FactsAndData.SingleOrDefault(d => d.Id == id);

            if (factsAndData == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            // Atualiza os dados
            factsAndData.Update(input.Age, input.UserName, input.Email, input.MaritalStatus, input.Address, input.Occupation, input.AcademicFormation);
            SaveDataToFile();  // Atualiza o arquivo JSON após a modificação

            return NoContent();
        }

        // DELETE: api/user-controller/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário por ID.")]
        public IActionResult Delete(Guid id)
        {
            var factsAndData = _context.FactsAndData.SingleOrDefault(d => d.Id == id);

            if (factsAndData == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            _context.FactsAndData.Remove(factsAndData);
            SaveDataToFile();  // Atualiza o arquivo JSON após a remoção

            return NoContent();
        }

        // Método que salva os dados no arquivo JSON
        private void SaveDataToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Permitir caracteres Unicode
            };
            var jsonData = JsonSerializer.Serialize(_context.FactsAndData, options);

            // Escreve os dados no arquivo JSON
            System.IO.File.WriteAllText(_filePath, jsonData);
        }
    }
}
