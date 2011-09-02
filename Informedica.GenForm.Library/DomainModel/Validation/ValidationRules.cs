using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Validation
{
    public class ValidationRules<TEnt> : 
        IEnumerable<ValidationRule<TEnt>>, 
        IEnumerable<String> , 
        IEnumerable<KeyValuePair<ValidationRule<TEnt>, String>> 
        where TEnt : Entity<TEnt>
    {
        private readonly IDictionary<ValidationRule<TEnt>, String> _rules = new ConcurrentDictionary<ValidationRule<TEnt>, string>();

        IEnumerator<KeyValuePair<ValidationRule<TEnt>, string>> IEnumerable<KeyValuePair<ValidationRule<TEnt>, string>>.GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return _rules.Values.GetEnumerator();
        }

        public IEnumerator<ValidationRule<TEnt>> GetEnumerator()
        {
            return _rules.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal void RegisterRule(ValidationRule<TEnt> rule)
        {
            RegisterRule(rule, String.Empty);
        }

        internal void RegisterRule(ValidationRule<TEnt> rule, String description)
        {
            if (_rules.ContainsKey(rule)) return;
            _rules.Add(rule,description);            
        }
    }
}