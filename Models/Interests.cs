namespace Operacao_curiosidade_API.Models{
    public class Interests
    {
        // Conteúdo ou descrição do interesse
        public string Content { get; set; }

        // Data da última atualização do interesse
        public DateTime? LastUpdated { get; set; }

        // Construtor padrão inicializando valores default
        public Interests()
        {
            LastUpdated = DateTime.Now;  // Define a data de criação ou atualização
        }

        // Método para atualizar o conteúdo do interesse
        public void UpdateContent(string newContent)
        {
            Content = newContent;
            LastUpdated = DateTime.Now;  // Atualiza a data de modificação
        }

    }
}