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

        public static IEnumerable<ValidationRule<TDto>> GetRules<TDto>() 
            where TDto : DataTransferObject<TDto>
        {
            return Instance.GetRulesFor<TDto>();
        }

        private IEnumerable<ValidationRule<TDto>> GetRulesFor<TDto>()
            where TDto : DataTransferObject<TDto>
        {
            if (!_registry.ContainsKey(typeof(TDto))) RegisterRules<TDto>();
            var rules = (ValidationRules<TDto>)_registry[typeof(TDto)];
            return rules;
        }

        private void RegisterRules<TDto>() where TDto : DataTransferObject<TDto>
        {
            _registry.Add(typeof(TDto), new ValidationRules<TDto>());
        }

        public static void RegisterRule<T>(ValidationRule<T> func)
            where T : DataTransferObject<T>
        {
            RegisterRule(func, String.Empty);
        }

        public static void RegisterRule<T>(ValidationRule<T> func, String description)
            where T : DataTransferObject<T>
        {
            ((ValidationRules<T>)Instance.GetRulesFor<T>()).RegisterRule(func, description);
        }

        internal static string GetBrokenRule<TDto>(ValidationRule<TDto> rule)
            where TDto : DataTransferObject<TDto>
        {
            return  ((IEnumerable<KeyValuePair<ValidationRule<TDto>, string>>)
                     GetRules<TDto>()).Single(x => x.Key == rule).Value;
        }

        internal static string CheckRules<TDto>(TDto dto) 
            where TDto : DataTransferObject<TDto>
        {
            foreach (var rule in GetValidationRules<TDto>())
            {
                if (!rule.Invoke(dto)) return GetBrokenRule(rule);
            }
            return String.Empty;
        }

        internal static IEnumerable<ValidationRule<TDto>> GetValidationRules<TDto>() 
            where TDto : DataTransferObject<TDto>
        {
            return GetRules<TDto>();
        }

        internal static IEnumerable<String> GetBrokenRules<TDto>(TDto dto)
            where TDto : DataTransferObject<TDto>
        {
            return (from rule in ValidationRulesManager.GetValidationRules<TDto>()
                    where !rule.Invoke(dto)
                    select ValidationRulesManager.GetBrokenRule(rule)).ToList();
        }

    }
}