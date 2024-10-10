using System;

namespace Operacao_curiosidade_API.Models
{
    public class Values
    {
        // Propriedades privadas para encapsulamento
        private string _content;
        private DateTime? _lastUpdated;

        // Identificador único para o valor
        public int Id { get; set; } 

        public string Content
        {
            get => _content;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O conteúdo do valor não pode ser vazio.");
                _content = value;
                LastUpdated = DateTime.Now; // Atualiza a data de modificação quando o conteúdo é alterado
            }
        }

        public DateTime? LastUpdated
        {
            get => _lastUpdated;
            private set => _lastUpdated = value ?? DateTime.Now; // Define a data atual se for null
        }

        // Construtor padrão inicializando valores default
        public Values()
        {
            Content = string.Empty; // Inicializa com string vazia
            LastUpdated = DateTime.Now; // Define a data de criação ou atualização
        }

        // Método para atualizar o conteúdo do valor
        public void UpdateContent(string newContent)
        {
            Content = newContent; // Atribui o novo conteúdo e atualiza LastUpdated
        }

        // Método para marcar o valor como "deletado" (soft delete)
        public void MarkAsDeleted()
        {
            LastUpdated = DateTime.Now; // Atualiza a data de modificação
        }
    }
}