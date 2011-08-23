using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Services.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class FactoryManager
    {

        private readonly IDictionary<Type, Type> _factories = 
            new Dictionary<Type, Type>();

        private static FactoryManager _instance;
        private static readonly object LockThis = new object();

        public FactoryManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            _factories.Add(typeof(EntityFactory<Unit, Guid, UnitDto>), typeof(UnitFactory));
            _factories.Add(typeof(EntityFactory<UnitGroup, Guid, UnitGroupDto>), typeof(UnitGroupFactory));
            _factories.Add(typeof(EntityFactory<Brand, Guid, BrandDto>), typeof(BrandFactory));
            _factories.Add(typeof(EntityFactory<Shape, Guid, ShapeDto>), typeof(ShapeFactory));
            _factories.Add(typeof(EntityFactory<Package, Guid, PackageDto>), typeof(PackageFactory));
            _factories.Add(typeof(EntityFactory<Route, Guid, RouteDto>), typeof(RouteFactory));
            _factories.Add(typeof(EntityFactory<Substance, Guid, SubstanceDto>), typeof(SubstanceFactory));
            _factories.Add(typeof(EntityFactory<Product, Guid, ProductDto>), typeof(ProductFactory));
        }

        private static FactoryManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new FactoryManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                        
                    }
                return _instance;
            }
        }

        private EntityFactory<TEnt, TId, TDto> GetFromInstance<TEnt, TId, TDto>(TDto dto)
            where TEnt : Entity<TId,TDto>
            where TDto : DataTransferObject<TDto, TId>
        {
            Type type = _factories[typeof(EntityFactory<TEnt, TId, TDto>)];
            return (EntityFactory<TEnt, TId, TDto>)Activator.CreateInstance(type, new object[] { dto });            
        }

        public static EntityFactory<TEnt, TId, TDto> Get<TEnt, TId, TDto>(TDto dto)
            where TEnt : Entity<TId,TDto>
            where TDto : DataTransferObject<TDto, TId>

        {
            return Instance.GetFromInstance<TEnt,TId,TDto>(dto);
        }
    }
}