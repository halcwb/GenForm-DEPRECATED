using System;
using Informedica.GenForm.Library.DomainModel.Identification;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : ISubstance, IDomainModel<Substance, Int32>
    {
        private SubstanceDto _dto;
        private IIdentifier<Int32> _identifier;

        public Substance(SubstanceDto substanceDto)
        {
            _dto = substanceDto.CloneDto();
        }

        public int SubstanceId
        {
            get { return _dto.SubstanceId; }
            set { _dto.SubstanceId = value; }
        }

        public String SubstanceName
        {
            get { return _dto.SubstanceName; }
            set { _dto.SubstanceName = value; }
        }

        public IIdentifier<Int32> Identifier
        {
            get { return _identifier ?? (CreateIdentifier()); }
        }

        private IIdentifier<Int32> CreateIdentifier()
        {
            return _identifier = new Identifier<Int32, String>(SubstanceId, SubstanceName, GetEquals());
        }

        private IdentifierEquals<Int32> GetEquals()
        {
            return (s => s.Id == SubstanceId && s.Name == SubstanceName);
        }

        public Boolean Equals(Substance substance)
        {
            return substance.Identifier.Equals(Identifier);
        }

    }
}