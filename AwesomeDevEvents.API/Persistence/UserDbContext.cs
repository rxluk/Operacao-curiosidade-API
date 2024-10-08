using AwesomeDevEvents.API.Models;
using System.Collections.Generic;

namespace AwesomeDevEvents.API.Persistence
{
    public class UserDbContext
    {
        public List<FactsAndData> FactsAndData { get; set; }
        public List<CuriosityOperation> CuriosityOperations { get; set; } // Lista de operações de curiosidade

        public UserDbContext()
        {
            FactsAndData = new List<FactsAndData>();  // Inicializa a lista de Fatos e Dados
            CuriosityOperations = new List<CuriosityOperation>();  // Inicializa a lista de operações de curiosidade
        }

        // Método para gerar o próximo DisplayId de UserModel
        //public int GetNextDisplayId()
        //{
        //    // Gera o próximo DisplayId a partir de 2400
        //    return FactsAndData.Count == 0 ? 2400 : FactsAndData.Max(d => d.User.DisplayId) + 1;
        //}
    }
}