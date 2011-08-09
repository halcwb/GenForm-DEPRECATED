using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Shape: Entity<Guid, ShapeDto>, IShape
    {
        #region Implementation of IShape

        protected Shape():base(new ShapeDto()) {}

        public Shape(ShapeDto dto) : base(dto.CloneDto())
        {
        }


        public virtual String Name
        {
            get { return Dto.Name; }
            set { Dto.Name = value; }
        }

        #endregion
    }

    public class ShapeDto: DataTransferObject<ShapeDto, Guid>
    {
        public string Name;

        #region Overrides of DataTransferObject<ShapeDto,Guid>

        public override ShapeDto CloneDto()
        {
            return CreateClone();
        }

        #endregion
    }
}
