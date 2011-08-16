using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class SubstanceGroupDto: DataTransferObject<SubstanceGroupDto, Guid>
    {

        public override SubstanceGroupDto CloneDto()
        {
            return CreateClone();
        }

    }
}