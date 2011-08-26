using System;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Guid, PackageDto>, IPackage
    {
        private ISet<Product> _products = new HashedSet<Product>();
        private ISet<Shape> _shapes = new HashedSet<Shape>();
        private readonly ShapeComparer _shapeComparer = new ShapeComparer();

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
            if (ContainsShape(shape)) return;

            _shapes.Add(shape);
            shape.AddPackage(this);
        }

        public virtual bool ContainsShape(Shape shape)
        {
            return _shapes.Contains(shape, _shapeComparer);
        }

        public virtual void RemoveShape(Shape shape)
        {
            if (!ContainsShape(shape)) return;

            _shapes.Remove(shape);
            shape.RemovePackage(this);
        }

        public virtual ISet<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = value; }
        }

        public virtual ISet<Product> Products
        {
            get { return _products; }
            protected set { _products = value; }
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
            var list = new HashedSet<Shape>(Shapes);
            foreach (var shape in list)
            {
                RemoveShape(shape);
            }
        }

        internal protected virtual void RemoveProduct(Product product)
        {
            if(_products.Contains(product))
            {
                _products.Remove(product);
            }
        }

        public virtual void AddProduct(Product product)
        {
            if (_products.Contains(product)) return;

            _products.Add(product);
            product.SetPackage(this);
        }
    }
}
