using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public abstract class DataTransferObject<TClone>: ICloneable
    {
        public Guid Id { get; set; }
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
