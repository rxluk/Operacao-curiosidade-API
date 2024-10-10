using System;

namespace Operacao_curiosidade_API.Models
{
    public class FactsAndDataSpeaker
    {
        // Propriedades privadas para encapsulamento
        private Guid _id;
        private string _name;

        public Guid Id
        {
            get => _id;
            private set => _id = value != Guid.Empty ? value : throw new ArgumentException("O ID não pode ser vazio.");
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome do palestrante não pode ser vazio.");
                _name = value;
            }
        }

        // Construtor padrão gerando ID automaticamente
        public FactsAndDataSpeaker()
        {
            Id = Guid.NewGuid();
        }

        // Construtor adicional para facilitar a criação com nome
        public FactsAndDataSpeaker(string name) : this() // Chama o construtor padrão
        {
            Name = name;
        }
    }
}