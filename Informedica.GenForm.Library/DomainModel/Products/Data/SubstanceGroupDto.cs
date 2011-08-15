using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class SubstanceGroupDto: DataTransferObject<SubstanceGroupDto, Guid>
    {

        public override SubstanceGroupDto CloneDto()
        {
            return CreateClone();
        }

    }
}