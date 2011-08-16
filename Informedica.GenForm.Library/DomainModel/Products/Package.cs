using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Guid, PackageDto>, IPackage
    {
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());
        private readonly HashSet<Product> _products = new HashSet<Product>(new ProductComparer());

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

        public virtual IEnumerable<Product> Products
        {
            get { return _products; }
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public static Package Create(PackageDto dto)
        {
            return new Package(dto);
        }

        internal protected virtual void AddProduct(Product product)
        {
            product.SetPackage(this, AddProductToPackage);
        }

        private void AddProductToPackage(Product product)
        {
            _products.Add(product);
        }

        internal protected virtual void Remove(Product product)
        {
            _products.Remove(product);
        }
    }
}
