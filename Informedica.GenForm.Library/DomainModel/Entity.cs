using System;

namespace Informedica.GenForm.Library.DomainModel
{
    public abstract class Entity<TId, TDto> where TDto: DataTransferObject<TDto, TId>
    {
        protected readonly TDto Dto;
        public virtual TId Id { get { return Dto.Id; } protected set { Dto.Id = value; } }
        public virtual int Version { get; set; }
        public abstract bool IdIsDefault(TId id);

        protected Entity(TDto dto)
        {
            Dto = dto;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId, TDto>);
        }

        private static bool IsTransient(Entity<TId, TDto> obj)
        {
            return obj != null &&
                   Equals(obj.Id, default(TId));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity<TId, TDto> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) && 
                !IsTransient(this) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Equals(Id, default(TId)) ? base.GetHashCode() : Id.GetHashCode();
        }
    }

}
