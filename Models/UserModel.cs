using System;
using System.ComponentModel.DataAnnotations;

namespace Operacao_curiosidade_API.Models
{
    public class UserModel
    {
        [Required]
        public Guid Id { get; set; }  // ID do usuário

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Password { get; set; }  // Senha

        [Required(ErrorMessage = "Dados associados ao usuário são obrigatórios.")]
        public FactsAndData FactsAndData { get; set; }  // Dados associados ao usuário

        // Construtor
        public UserModel()
        {
            Id = Guid.NewGuid();
            FactsAndData = new FactsAndData(); // Inicializa FactsAndData
        }
    }
}