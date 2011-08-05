using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Data
{
    public class SubstanceDto: DataTransferObject
    {
        public String SubstanceName { get; set; }

        public int SubstanceId { get; set; }

        public SubstanceDto CloneDto()
        {
            return CloneDto<SubstanceDto>();
        }
    }
}