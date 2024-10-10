using System.Collections.Generic;

namespace Operacao_curiosidade_API.Models.DTO
{
    public class CuriosityDTO
    {
        private readonly List<Interests> _interests;
        private readonly List<Feelings> _feelings;
        private readonly List<Values> _values;

        public CuriosityDTO()
        {
            _interests = new List<Interests>();
            _feelings = new List<Feelings>();
            _values = new List<Values>();
        }

        public IReadOnlyList<Interests> Interests => _interests.AsReadOnly();
        public IReadOnlyList<Feelings> Feelings => _feelings.AsReadOnly();
        public IReadOnlyList<Values> Values => _values.AsReadOnly();

        public void AddInterest(Interests interest)
        {
            if (interest != null)
            {
                _interests.Add(interest);
            }
        }

        public void RemoveInterest(Interests interest)
        {
            _interests.Remove(interest);
        }

        public void AddFeeling(Feelings feeling)
        {
            if (feeling != null)
            {
                _feelings.Add(feeling);
            }
        }

        public void RemoveFeeling(Feelings feeling)
        {
            _feelings.Remove(feeling);
        }

        public void AddValue(Values value)
        {
            if (value != null)
            {
                _values.Add(value);
            }
        }

        public void RemoveValue(Values value)
        {
            _values.Remove(value);
        }
    }
}
