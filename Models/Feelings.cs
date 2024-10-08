namespace Operacao_curiosidade_API.Models{
     public class Feelings
    {
        // Identificador único do sentimento
        public int Id { get; set; }

        // Conteúdo ou descrição do sentimento
        public string Content { get; set; }

        // Data da última atualização do sentimento
        public DateTime? LastUpdated { get; set; }

        // Indica se o sentimento foi "deletado" (soft delete)
        public bool IsDeleted { get; set; }

        // Construtor padrão inicializando valores default
        public Feelings()
        {
            LastUpdated = DateTime.Now;  // Define a data de criação ou atualização
            IsDeleted = false;           // Sentimento ativo por padrão
        }

        // Método para atualizar o conteúdo do sentimento
        public void UpdateContent(string newContent)
        {
            Content = newContent;
            LastUpdated = DateTime.Now;  // Atualiza a data de modificação
        }

        // Método para marcar o sentimento como "deletado" (soft delete)
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            LastUpdated = DateTime.Now;  // Atualiza a data de modificação
        }
    }
}