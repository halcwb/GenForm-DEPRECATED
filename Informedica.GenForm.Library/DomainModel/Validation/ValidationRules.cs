using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Validation
{
    public class ValidationRules<TDto> : 
        IEnumerable<ValidationRule<TDto>>, 
        IEnumerable<String> , 
        IEnumerable<KeyValuePair<ValidationRule<TDto>, String>> 
        where TDto : DataTransferObject<TDto>
    {
        private readonly IDictionary<ValidationRule<TDto>, String> _rules = new ConcurrentDictionary<ValidationRule<TDto>, string>();

        IEnumerator<KeyValuePair<ValidationRule<TDto>, string>> IEnumerable<KeyValuePair<ValidationRule<TDto>, string>>.GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _rules.Values.GetEnumerator();
        }

        public IEnumerator<ValidationRule<TDto>> GetEnumerator()
        {
            return _rules.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal void RegisterRule(ValidationRule<TDto> rule)
        {
            RegisterRule(rule, String.Empty);
        }

        internal void RegisterRule(ValidationRule<TDto> rule, String description)
        {
            if (_rules.ContainsKey(rule)) return;
            _rules.Add(rule,description);            
        }
    }
}