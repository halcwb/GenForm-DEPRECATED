using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Exceptions;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Guid, PackageDto>, IPackage
    {
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());

        #region Implementation of IPackage

        protected Package() : base(new PackageDto()) {}

        public Package(PackageDto dto) : base(dto.CloneDto()) {}

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
            shape.AddPackage(this, AddShapeToPackage);
        }

        private void AddShapeToPackage(Shape shape)
        {
            ShapeAssociation.AddShape(_shapes, shape);
        }

        public virtual void RemoveShape(Shape shape)
        {
            shape.RemovePackage(this);
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = new HashSet<Shape>(value); }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }

    public static class ShapeAssociation
    {
        public static void AddShape(HashSet<Shape> shapes, Shape shape)
        {
            if (shapes.Contains(shape, new ShapeComparer())) throw new CannotAddItemException<Shape>(shape);
            shapes.Add(shape);
        }
    }
}
