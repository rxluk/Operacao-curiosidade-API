using System;
using System.Collections.Generic;

namespace Operacao_curiosidade_API.Models
{
    public class CuriosityOperation
    {
        public Guid UserId { get; set; } // ID do usuário associado
        public required UserModel User { get; set; } // Referência ao modelo de usuário

        public required string FactsAndData { get; set; } // Campo obrigatório
        public required List<Interests> Interests { get; set; } // Campo obrigatório
        public required List<Feelings> Feelings { get; set; } // Campo obrigatório
        public required List<Values> Values { get; set; } // Campo obrigatório

        public bool IsDeleted { get; set; }

        public CuriosityOperation()
        {
            IsDeleted = false; // Curiosidade ativa por padrão
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true; // Marca como deletada
        }

        public void Update(string factsAndData, List<Interests> interests, List<Feelings> feelings, List<Values> values)
        {
            FactsAndData = factsAndData;
            Interests = interests;
            Feelings = feelings;
            Values = values;
        }
    }
}