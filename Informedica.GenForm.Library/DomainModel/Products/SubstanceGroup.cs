using System;
using System.Collections.Generic;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class SubstanceGroup: Entity<Guid, SubstanceGroupDto>, ISubstanceGroup
    {
        private ISubstanceGroup _mainSubstanceGroup;
        private IList<Substance> _substances;

        protected SubstanceGroup(): base(new SubstanceGroupDto()) {}

        [DefaultConstructor]
        public SubstanceGroup(SubstanceGroupDto dto): base(dto.CloneDto()) {}

        #region Implementation of ISubstanceGroup

        public virtual String Name
        {
            get { return Dto.Name; }
            set { Dto.Name = value; }
        }

        public virtual ISubstanceGroup MainSubstanceGroup
        {
            get { return _mainSubstanceGroup; }
            set { _mainSubstanceGroup = value; }
        }
        
        public virtual IEnumerable<Substance> Substances
        {
            get { return _substances ?? (_substances = new List<Substance>()); }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }

    public class SubstanceGroupDto: DataTransferObject<SubstanceGroupDto, Guid>
    {
        public string Name;

        public override SubstanceGroupDto CloneDto()
        {
            return CreateClone();
        }

    }

    public interface ISubstanceGroup
    {
        Guid Id { get; }
        String Name { get; set; }
        ISubstanceGroup MainSubstanceGroup { get; set; }
    }
}
