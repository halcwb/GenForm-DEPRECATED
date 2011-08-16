using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Equality;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Exceptions;
using StructureMap;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Product : Entity<Guid, ProductDto>, IProduct
    {
        #region Private Fields
        
        private IList<ProductSubstance> _substances;
        private IList<Route> _routes;

        #endregion

        #region Constructor
        
        protected Product() : base(new ProductDto()) { }

        [DefaultConstructor]
        public Product(ProductDto dto)
            : base(dto.CloneDto())
        {
            foreach (var substanceDto in Dto.Substances)
            {
                GetSubstances().Add(NewSubstance(substanceDto));
            }
        }

        private static ProductSubstance NewSubstance(ProductSubstanceDto productSubstanceDto)
        {
            return new ProductSubstance(productSubstanceDto);
        }

        #endregion
        
        #region Implementation of IProduct

        public virtual string ProductCode { get { return Dto.ProductCode; } protected set { Dto.ProductCode = value; } }

        public virtual string GenericName { get { return Dto.GenericName; } protected set { Dto.GenericName = value; } }

        public virtual UnitValue Quantity { get; protected set; }

        public virtual string DisplayName { get { return Dto.DisplayName ?? Dto.Name; } protected set { Dto.DisplayName = value; } }

        public virtual Brand Brand { get; protected set; }

        public virtual Package Package { get; protected set; }

        public virtual Shape Shape { get; protected set; }

        public virtual ProductSubstance AddSubstance(ProductSubstanceDto productSubstanceDto)
        {
            var substance = new ProductSubstance(productSubstanceDto);
            GetSubstances().Add(substance);
            return substance;
        }

        private IList<ProductSubstance> GetSubstances()
        {
            return _substances ?? (_substances = new List<ProductSubstance>());
        }

        public virtual IEnumerable<ProductSubstance> Substances
        {
            get
            {
                return GetSubstances();
            }

            protected set { _substances = value.ToList(); }
        }

        public virtual void AddRoute(Route route)
        {
            if (_routes.Contains(route, new RouteComparer())) throw new CannotAddItemException<Route>(route);
            _routes.Add(route);
            route.AddProduct(this);
        }

        public virtual IEnumerable<Route> Routes
        {
            get { return _routes ?? (_routes = new List<Route>()); }  
            protected set { _routes = value.ToList(); }
        }

        #endregion

        public override bool IdIsDefault(Guid id)
        {
            return id == Guid.Empty;
        }
    }
}