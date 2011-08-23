using System;
using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Relations;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Route: Entity<Guid, RouteDto>, IRelationPart
    {
        protected Route() : base(new RouteDto()){}

        private Route(RouteDto dto) : base(dto.CloneDto())
        {
            AddShapes();
        }

        private void AddShapes()
        {
            foreach (var shape in Dto.Shapes)
            {
                AddShape(Shape.Create(shape));
            }
        }

        public virtual void AddShape(Shape shape)
        {
            RelationProvider.ShapeRoute.Add(shape, this);
        }

        public virtual bool CanNotAddShape(Shape shape)
        {
            throw new NotImplementedException();
        }

        public virtual String Abbreviation 
        { 
            get { return Dto.Abbreviation ?? (Dto.Abbreviation = CreateAbbreviationFromName()); } 
            set { Dto.Abbreviation = value; } 
        }

        public virtual IEnumerable<Shape> Shapes
        {
            get { return RelationProvider.ShapeRoute.GetManyPartLeft(this); }
            protected set { RelationProvider.ShapeRoute.Add(value, this); }
        }

        private string CreateAbbreviationFromName()
        {
            int maxLength = Name.Length > 30 ? 30 : Name.Length;
            return Dto.Name.Substring(0, maxLength);
        }

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }

        public virtual IEnumerable<Product> Products
        {
            get { return RelationProvider.RouteProduct.GetManyPartRight(this); }
            protected set { RelationProvider.RouteProduct.Add(this, value);}
        }

        public static Route Create(RouteDto dto)
        {
            return new Route(dto);
        }

        internal protected  virtual void RemoveAllShapes()
        {
            RelationProvider.ShapeRoute.Clear(this);
        }

        public virtual void RemoveShape(Shape shape)
        {
            RelationProvider.ShapeRoute.Remove(shape, this);
        }
    }
}