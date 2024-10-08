namespace AwesomeDevEvents.API.Models
{
    public class CuriosityOperation
    {
        public Guid Id { get; set; }
        public FactsAndData FactsAndData { get; set; }
        public List<Interests> Interests { get; set; }
        public List<Feelings> Feelings { get; set; }
        public List<Values> Values { get; set; }
        public bool IsDeleted { get; set; }

        // Construtor padrão
        public CuriosityOperation()
        {
            Interests = new List<Interests>();
            Feelings = new List<Feelings>();
            Values = new List<Values>();
            IsDeleted = false; // Por padrão, a operação não está deletada
        }

        // Método de atualização, se necessário
        public void Update(FactsAndData factsAndData, List<Interests> interests, List<Feelings> feelings, List<Values> values)
        {
            FactsAndData = factsAndData;
            Interests = interests;
            Feelings = feelings;
            Values = values;
        }
    }
}