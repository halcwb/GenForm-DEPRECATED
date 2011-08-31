using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Validation;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel
{
    public abstract class Entity<TEnt> 
        where TEnt : Entity<TEnt>
    {
        public const int NameLength = 255;

        private readonly IEqualityComparer<TEnt> _comparer;

        protected Entity(IEqualityComparer<TEnt> comparer)
        {
            _comparer = comparer;
        }

        public abstract Guid Id { get; protected set; }

        public abstract string Name { get; protected set; }

        public virtual int Version  { get; protected set; }

        public virtual bool IsTransient()
        {
            return Id == Guid.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return _comparer.Equals((TEnt)this, (TEnt)obj);
        }

        public override int GetHashCode()
        {
            return  _comparer.GetHashCode((TEnt)this);
        }

        protected abstract void SetDto<TDto>(TDto dto) where TDto : DataTransferObject<TDto>;

        protected void ValidateDto<TDto>(TDto dto) 
            where TDto : DataTransferObject<TDto>
        {
            var brokenRule = ValidationRulesManager.CheckRules(dto);
            if (!String.IsNullOrEmpty(brokenRule)) throw new BrokenValidationRuleException(brokenRule);

            SetDto(dto);
        }

        public static IEnumerable<String> GetBrokenRules<TDto>(TDto dto) where TDto : DataTransferObject<TDto>
        {
            return ValidationRulesManager.GetBrokenRules(dto);
        }
    }

}
