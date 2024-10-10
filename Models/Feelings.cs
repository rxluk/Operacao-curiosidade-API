using System;

namespace Operacao_curiosidade_API.Models
{
    public class Feelings
    {
        // Propriedades privadas para encapsulamento
        private int _id;
        private string _content;
        private DateTime? _lastUpdated;
        private bool _isDeleted;

        public int Id
        {
            get => _id;
            private set => _id = value >= 0 ? value : throw new ArgumentException("O ID deve ser um número não negativo.");
        }

        public string Content
        {
            get => _content;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O conteúdo do sentimento não pode ser vazio.");
                _content = value;
                LastUpdated = DateTime.Now; // Atualiza a data de modificação quando o conteúdo é alterado
            }
        }

        public DateTime? LastUpdated
        {
            get => _lastUpdated;
            private set => _lastUpdated = value ?? DateTime.Now; // Define a data atual se for null
        }

        public bool IsDeleted
        {
            get => _isDeleted;
            private set => _isDeleted = value;
        }

        // Construtor padrão inicializando valores default
        public Feelings()
        {
            Id = 0; // O ID pode ser gerado pelo sistema, ajustado conforme necessário
            Content = string.Empty; // Inicializa com string vazia
            IsDeleted = false; // Sentimento ativo por padrão
            LastUpdated = DateTime.Now; // Define a data de criação ou atualização
        }

        // Método para atualizar o conteúdo do sentimento
        public void UpdateContent(string newContent)
        {
            Content = newContent; // Atribui o novo conteúdo e atualiza LastUpdated
        }

        // Método para marcar o sentimento como "deletado" (soft delete)
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            LastUpdated = DateTime.Now; // Atualiza a data de modificação
        }
    }
}