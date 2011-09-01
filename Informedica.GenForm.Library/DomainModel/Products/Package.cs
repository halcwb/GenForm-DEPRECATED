using System;
using System.Linq;
using Iesi.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Package>, IPackage
    {
        private ISet<Product> _products = new HashedSet<Product>();
        private ISet<Shape> _shapes = new HashedSet<Shape>();
        private readonly ShapeComparer _shapeComparer = new ShapeComparer();
        private PackageDto _dto;

        #region Implementation of IPackage

        static Package()
        {
            RegisterValidationRules();
        }

        protected Package() : base()
        {
            _dto = new PackageDto();
        }

        private Package(PackageDto dto) : base()
        {
            ValidateDto(dto);
        }

        public virtual String Abbreviation
        {
            get { return _dto.Abbreviation ?? GetAbbreviatedName(); }
            set { _dto.Abbreviation = value; }
        }

        private string GetAbbreviatedName()
        {
            int maxlength = Name.Length > 30 ? 30 : Name.Length;
            return _dto.Name.Substring(0, maxlength);
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


        public override Guid Id { get { return _dto.Id; } protected set { _dto.Id = value; } }

        public override string Name { get { return _dto.Name; } protected set { _dto.Name = value; } }

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<PackageDto>(x => !String.IsNullOrWhiteSpace(x.Name));
        }

        protected override void SetDto<TDto>(TDto dto)
        {
            var dataTransferObject = dto as DataTransferObject<PackageDto>;
            if (dataTransferObject != null) _dto = dataTransferObject.CloneDto();
        }
    }
}
