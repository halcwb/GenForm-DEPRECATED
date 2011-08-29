using System;

namespace Informedica.GenForm.Library.DomainModel
{
    public abstract class Entity<TId, TDto> where TDto: DataTransferObject<TDto, TId>
    {
        private int? _cachedHashCode;

        protected readonly TDto Dto;

        public virtual TId Id { get { return Dto.Id; } protected set { Dto.Id = value; } }
        public virtual string Name 
        { 
            get { return Dto.Name; }
            set { Dto.Name = value; } 
        }

        public virtual int Version
        {
            get; protected set;
        }
        
        public abstract bool IdIsDefault(TId id);

        protected Entity(TDto dto)
        {
            Dto = dto;
        }

        private int CreateHashCode()
        {
            if (IdIsDefault(Dto.Id)) return base.GetHashCode(); 
            return Dto.Id.GetHashCode();
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
                !IsTransient(other) &&
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
            return (int)(_cachedHashCode ?? (_cachedHashCode = CreateHashCode()));
        }
    }

}
