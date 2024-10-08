using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AwesomeDevEvents.API.Models
{
    public class FactsAndData
    {
        public FactsAndData()
        {
            Speakers = new List<FactsAndDataSpeaker>();
            IsDeleted = false;
        }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string AcademicFormation { get; set; }

        [Required(ErrorMessage = "Deve haver pelo menos um palestrante.")]
        public List<FactsAndDataSpeaker> Speakers { get; set; }

        public bool IsDeleted { get; set; }

        public Guid Id { get; internal set; }

        // Método de atualização para manter dados consistentes
        public void Update(int age, string userName, string email, string maritalStatus, string address, string occupation, string academicFormation)
        {
            Age = age;
            UserName = userName;
            Email = email;
            MaritalStatus = maritalStatus;
            Address = address;
            Occupation = occupation;
            AcademicFormation = academicFormation;
        }

        // Método para marcar como excluído
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}