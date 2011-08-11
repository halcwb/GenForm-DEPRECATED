using System;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : Entity<Guid, SubstanceDto>, ISubstance
    {
        private SubstanceGroup _group; 

        protected Substance(): base(new SubstanceDto()) {}

        [DefaultConstructor]
        public Substance(SubstanceDto substanceDto): base(substanceDto.CloneDto())
        {
            if (Dto.SubstanceGroupName == null) return;
            
            _group = new SubstanceGroup(new SubstanceGroupDto
                                            {
                                                Id =  Dto.SubstanceGroupId,
                                                Name = Dto.SubstanceGroupName
                                            });
        }

        public virtual int SubstanceId
        {
            get { return Dto.SubstanceId; }
            set { Dto.SubstanceId = value; }
        }

        public virtual String Name
        {
            get { return Dto.Name; }
            set { Dto.Name = value; }
        }

        public virtual SubstanceGroup SubstanceGroup
        {
            get { return _group; }
            set { _group = value; }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }
}