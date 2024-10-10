using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Operacao_curiosidade_API.Models
{
    public class FactsAndData
    {
        public FactsAndData()
        {
            Speakers = new List<FactsAndDataSpeaker>();
            IsDeleted = false;
        }

        // Propriedades privadas para encapsulamento
        private int _age;
        private string _userName;
        private string _email;
        private string _maritalStatus;
        private string _address;
        private string _occupation;
        private string _academicFormation;
        private List<FactsAndDataSpeaker> _speakers;
        private bool _isDeleted;

        [Required(ErrorMessage = "A idade é obrigatória.")]
        public int Age
        {
            get => _age;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A idade deve ser maior que zero.");
                _age = value;
            }
        }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string UserName
        {
            get => _userName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome de usuário não pode ser vazio.");
                _userName = value;
            }
        }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O email não pode ser vazio.");
                _email = value;
            }
        }

        public string MaritalStatus
        {
            get => _maritalStatus;
            set => _maritalStatus = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Occupation
        {
            get => _occupation;
            set => _occupation = value;
        }

        public string AcademicFormation
        {
            get => _academicFormation;
            set => _academicFormation = value;
        }

        [Required(ErrorMessage = "Deve haver pelo menos um Usuário.")]
        public List<FactsAndDataSpeaker> Speakers
        {
            get => _speakers;
            set => _speakers = value ?? new List<FactsAndDataSpeaker>();
        }

        public bool IsDeleted
        {
            get => _isDeleted;
            private set => _isDeleted = value;
        }

        public Guid Id { get; private set; } = Guid.NewGuid(); // Gera um novo ID por padrão

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
        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
