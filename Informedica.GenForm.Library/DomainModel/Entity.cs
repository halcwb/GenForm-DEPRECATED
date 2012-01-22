using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Validation;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel
{
    public abstract class Entity<TEnt> 
        where TEnt : Entity<TEnt>
    {
        protected Entity()
        {
        } 

        public const int NameLength = 255;

        public virtual Guid Id { get; protected set; }

        public virtual string Name { get; set; }

        public virtual int Version  { get; protected set; }

        public virtual bool IsTransient()
        {
            return Id == Guid.Empty;
        }

        public virtual IEnumerable<String> GetBrokenRules(TEnt entity)
        {
            return ValidationRulesManager.GetBrokenRules(entity);
        }

        public static void Validate(TEnt entity)
        {
            var broken = entity.GetBrokenRules(entity).ToList();
            var count = broken.Count();
            if (broken != null && count > 0) throw new InvalidEntityException<TEnt>(entity, broken);
        }
    }

}
