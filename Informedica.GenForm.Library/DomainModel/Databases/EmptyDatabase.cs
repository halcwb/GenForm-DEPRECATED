using System;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public class EmptyDatabase
    {
        public virtual Guid Id { get; protected set; }
        public virtual bool IsEmpty { get; set; }

    }
}