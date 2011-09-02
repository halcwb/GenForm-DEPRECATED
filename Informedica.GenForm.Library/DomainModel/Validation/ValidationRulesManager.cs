using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Informedica.GenForm.Library.DomainModel.Validation
{
    public class ValidationRulesManager
    {
        private static ValidationRulesManager _instance;
        private static readonly object LockThis = new object();
        private readonly IDictionary<Type, object> _registry = new ConcurrentDictionary<Type, object>();

        private static ValidationRulesManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new ValidationRulesManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static IEnumerable<ValidationRule<TEnt>> GetRules<TEnt>() 
            where TEnt : Entity<TEnt>
        {
            return Instance.GetRulesFor<TEnt>();
        }

        private IEnumerable<ValidationRule<TEnt>> GetRulesFor<TEnt>()
            where TEnt : Entity<TEnt>
        {
            if (!_registry.ContainsKey(typeof(TEnt))) RegisterRules<TEnt>();
            var rules = (ValidationRules<TEnt>)_registry[typeof(TEnt)];
            return rules;
        }

        private void RegisterRules<TEnt>() where TEnt : Entity<TEnt>
        {
            _registry.Add(typeof(TEnt), new ValidationRules<TEnt>());
        }

        public static void RegisterRule<TEnt>(ValidationRule<TEnt> func)
            where TEnt : Entity<TEnt>
        {
            RegisterRule(func, String.Empty);
        }

        public static void RegisterRule<T>(ValidationRule<T> func, String description)
            where T : Entity<T>
        {
            ((ValidationRules<T>)Instance.GetRulesFor<T>()).RegisterRule(func, description);
        }

        internal static string GetBrokenRule<TEnt>(ValidationRule<TEnt> rule)
            where TEnt : Entity<TEnt>
        {
            return  ((IEnumerable<KeyValuePair<ValidationRule<TEnt>, string>>)
                     GetRules<TEnt>()).Single(x => x.Key == rule).Value;
        }

        internal static string CheckRules<TEnt>(TEnt dto) 
            where TEnt : Entity<TEnt>
        {
            foreach (var rule in GetValidationRules<TEnt>())
            {
                if (!rule.Invoke(dto)) return GetBrokenRule(rule);
            }
            return String.Empty;
        }

        internal static IEnumerable<ValidationRule<TEnt>> GetValidationRules<TEnt>() 
            where TEnt : Entity<TEnt>
        {
            return GetRules<TEnt>();
        }

        internal static IEnumerable<String> GetBrokenRules<TEnt>(TEnt dto)
            where TEnt : Entity<TEnt>
        {
            return (from rule in GetValidationRules<TEnt>()
                    where !rule.Invoke(dto)
                    select GetBrokenRule(rule)).ToList();
        }

    }
}