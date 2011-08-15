using System;

namespace Informedica.GenForm.Library.DomainModel
{
    public abstract class DataTransferObject<TClone, TId>: ICloneable
    {
        public TId Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public abstract TClone CloneDto();

        protected TClone CreateClone()
        {
            return (TClone)Clone();
        }
    }
}
