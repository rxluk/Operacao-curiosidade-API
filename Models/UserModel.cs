using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Operacao_curiosidade_API.Models
{
    public class UserModel
    {
        [Required]
        public Guid Id { get; private set; }

        private string _password;

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Password 
        {
            get => _password; 
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A senha não pode ser nula ou em branco.");
                _password = value;
            }
        }

        [Required(ErrorMessage = "Dados associados ao usuário são obrigatórios.")]
        public FactsAndData FactsAndData { get; private set; } 

        public List<Interests> Interests { get; private set; } = new List<Interests>();
        public List<Feelings> Feelings { get; private set; } = new List<Feelings>();
        public List<Values> Values { get; private set; } = new List<Values>();
        public bool IsDeleted { get; private set; }

        public UserModel()
        {
            Id = Guid.NewGuid();
            FactsAndData = new FactsAndData();
            IsDeleted = false;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

        public void AddInterest(Interests interest)
        {
            if (interest == null) throw new ArgumentNullException(nameof(interest));
            Interests.Add(interest);
        }

        public void AddFeeling(Feelings feeling)
        {
            if (feeling == null) throw new ArgumentNullException(nameof(feeling));
            Feelings.Add(feeling);
        }

        public void AddValue(Values value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Values.Add(value);
        }

        // Novos métodos para atualizar dados
        public void UpdateFactsAndData(FactsAndData factsAndData)
        {
            FactsAndData = factsAndData ?? throw new ArgumentNullException(nameof(factsAndData));
        }

        public void UpdateInterests(List<Interests> interests)
        {
            Interests = interests ?? throw new ArgumentNullException(nameof(interests));
        }

        public void UpdateFeelings(List<Feelings> feelings)
        {
            Feelings = feelings ?? throw new ArgumentNullException(nameof(feelings));
        }

        public void UpdateValues(List<Values> values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }
    }
}