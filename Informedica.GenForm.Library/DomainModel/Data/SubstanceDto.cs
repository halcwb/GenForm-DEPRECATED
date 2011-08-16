using System;

namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class SubstanceDto: DataTransferObject<SubstanceDto, Guid>
    {
        public Guid SubstanceGroupId;
        public String SubstanceGroupName;

        public int SubstanceId { get; set; }

        public override SubstanceDto CloneDto()
        {
            return CreateClone();
        }

    }
}