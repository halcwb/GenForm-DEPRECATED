using System;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public class EmptyDatabaseDto: DataTransferObject<EmptyDatabaseDto, int>
    {
        public Boolean IsEmpty;

        public override EmptyDatabaseDto CloneDto()
        {
            return CreateClone();
        }
    }
}