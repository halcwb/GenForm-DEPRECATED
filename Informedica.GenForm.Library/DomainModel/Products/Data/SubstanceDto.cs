using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class SubstanceDto: DataTransferObject<SubstanceDto, Guid>
    {
        public Guid SubstanceGroupId;
        public String SubstanceGroupName;
        public String Name { get; set; }

        public int SubstanceId { get; set; }

        public override SubstanceDto CloneDto()
        {
            return CreateClone();
        }

    }
}