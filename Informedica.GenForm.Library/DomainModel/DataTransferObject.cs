using System;

namespace Informedica.GenForm.Library.DomainModel
{
    public abstract class DataTransferObject: ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        protected T CloneDto<T>()
        {
            return (T)Clone();
        }
    }
}
