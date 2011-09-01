using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit : Entity<Unit>, IUnit
    {
        private UnitGroup _unitGroup;
        private UnitDto _dto;

        #region Constructor

        static Unit()
        {
            RegisterValidationRules();
        }

        protected Unit()
        {
            _dto =  new UnitDto();
        }

        private Unit(UnitDto dto) : base()
        {
            ValidateDto(dto);

            var group = UnitGroup.Create(new UnitGroupDto
                {
                    AllowConversion = dto.AllowConversion,
                    Name = dto.UnitGroupName
                });
            SetUnitGroup(group);
        }

        public Unit(UnitDto dto, UnitGroup group) : base()
        {
            _dto = dto.CloneDto();
            SetUnitGroup(group);
        }

        private void SetUnitGroup(UnitGroup group)
        {
            ChangeUnitGroup(group);
        }

        #endregion

        #region Business

        public virtual String Abbreviation
        {
            get { return _dto.Abbreviation; }
            set { _dto.Abbreviation = value; }
        }

        public virtual Decimal Multiplier
        {
            get { return _dto.Multiplier; }
            set { _dto.Multiplier = value; }
        }

        public virtual Boolean IsReference
        {
            get { return _dto.IsReference; }
            set { _dto.IsReference = value; }
        }

        public virtual UnitGroup UnitGroup
        {
            get { return _unitGroup; }
            protected internal set { _unitGroup = value; }
        }

        public virtual void ChangeUnitGroup(UnitGroup newGroup)
        {
            newGroup.AddUnit(this);
        }

        public virtual bool CannotChangeGroup(UnitGroup newGroup)
        {
            return UnitGroup.Equals(newGroup, UnitGroup, new UnitGroupComparer());
        }

        internal protected virtual void RemoveFromGroup()
        {
            UnitGroup.RemoveUnit(this);
        }

        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<UnitDto>(x => !String.IsNullOrWhiteSpace(x.Name));
            ValidationRulesManager.RegisterRule<UnitDto>(x => !String.IsNullOrWhiteSpace(x.Abbreviation));
            ValidationRulesManager.RegisterRule<UnitDto>(x => x.Multiplier > 0);
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<UnitDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }

        #endregion

        #region Factory

        public static Unit Create(UnitDto dto)
        {
            return new Unit(dto);
        }

        public static Unit Create(UnitDto dto, UnitGroup group)
        {
            return new Unit(dto, @group);
        }

        public static Unit Create(UnitDto dto, UnitGroupDto groupDto)
        {
            return new Unit(dto, UnitGroup.Create(groupDto));
        }

        #endregion

        internal protected virtual void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}
