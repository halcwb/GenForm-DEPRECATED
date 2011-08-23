using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Guid, PackageDto>, IPackage, IRelationPart
    {
        #region Implementation of IPackage

        protected Package() : base(new PackageDto()) {}

        private Package(PackageDto dto) : base(dto.CloneDto()) {}

        public virtual String Abbreviation
        {
            get { return Dto.Abbreviation ?? GetAbbreviatedName(); }
            set { Dto.Abbreviation = value; }
        }

        private string GetAbbreviatedName()
        {
            int maxlength = Name.Length > 30 ? 30 : Name.Length;
            return Dto.Name.Substring(0, maxlength);
        }

        #endregion

        public virtual void AddShape(Shape shape)
        {
            RelationProvider.ShapePackage.Add(shape, this);
        }

        public virtual void RemoveShape(Shape shape)
        {
            RelationProvider.ShapePackage.Remove(shape, this);
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return RelationProvider.ShapePackage.GetManyPartLeft(this); }
            protected set { RelationProvider.ShapePackage.Add(value, this); }
        }

        public virtual IEnumerable<Product> Products
        {
            get { return RelationProvider.PackageProduct.GetManyPart(this); }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public static Package Create(PackageDto dto)
        {
            return new Package(dto);
        }

        internal protected virtual void RemoveAllShapes()
        {
            RelationProvider.ShapePackage.Clear(this);
        }
    }
}
