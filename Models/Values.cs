namespace Operacao_curiosidade_API.Models{
    public class Values
    {
        // Identificador único para o valor
        public int Id { get; set; }

        // Conteúdo ou descrição do valor
        public string Content { get; set; }

        // Data da última atualização do valor
        public DateTime? LastUpdated { get; set; }

        // Indica se o valor foi "deletado" (soft delete)
        public bool IsDeleted { get; set; }

        // Construtor padrão inicializando valores default
        public Values()
        {
            LastUpdated = DateTime.Now;  // Define a data de criação ou atualização
            IsDeleted = false;           // Valor ativo por padrão
        }

        // Método para atualizar o conteúdo do valor
        public void UpdateContent(string newContent)
        {
            Content = newContent;
            LastUpdated = DateTime.Now;  // Atualiza a data de modificação
        }

        // Método para marcar o valor como "deletado" (soft delete)
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            LastUpdated = DateTime.Now;  // Atualiza a data de modificação
        }
    }
}