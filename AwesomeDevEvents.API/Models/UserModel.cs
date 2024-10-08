using System;
using System.ComponentModel.DataAnnotations;

namespace AwesomeDevEvents.API.Models
{
    public class UserModel
    {
        [Required]
        public Guid Id { get; set; }  // ID do usuário

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public string Password { get; set; }  // Senha

        [Required]
        public FactsAndData FactsAndData { get; set; }  // Dados associados ao usuário
    }
}
