using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: Entity<Guid, PackageDto>, IPackage
    {
        private HashSet<Shape> _shapes = new HashSet<Shape>(new ShapeComparer());

        #region Implementation of IPackage

        protected Package() : base(new PackageDto()) {}

        public Package(PackageDto dto) : base(dto.CloneDto()) {}

        public virtual string Name
        {
            get { return Dto.Name; }
            set { Dto.Name = value; }
        }

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
            if (!AssociateShape.CanNotAddShape(shape, _shapes)) return;
            AssociateShape.WithPackage(shape, this, _shapes);
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return _shapes; }
            protected set { _shapes = new HashSet<Shape>(value); }
        }
        
    }
}
