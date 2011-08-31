using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

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
            _factories.Add(typeof(EntityFactory<Unit, UnitDto>), typeof(UnitFactory));
            _factories.Add(typeof(EntityFactory<UnitGroup, UnitGroupDto>), typeof(UnitGroupFactory));
            _factories.Add(typeof(EntityFactory<Brand, BrandDto>), typeof(BrandFactory));
            _factories.Add(typeof(EntityFactory<Shape, ShapeDto>), typeof(ShapeFactory));
            _factories.Add(typeof(EntityFactory<Package, PackageDto>), typeof(PackageFactory));
            _factories.Add(typeof(EntityFactory<Route, RouteDto>), typeof(RouteFactory));
            _factories.Add(typeof(EntityFactory<Substance, SubstanceDto>), typeof(SubstanceFactory));
            _factories.Add(typeof(EntityFactory<Product, ProductDto>), typeof(ProductFactory));
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

        private EntityFactory<TEnt, TDto> GetFromInstance<TEnt, TDto>(TDto dto)
            where TEnt : Entity<TEnt>
            where TDto : DataTransferObject<TDto>
        {
            Type type = _factories[typeof(EntityFactory<TEnt, TDto>)];
            return (EntityFactory<TEnt, TDto>)Activator.CreateInstance(type, new object[] { dto });            
        }

        public static EntityFactory<TEnt, TDto> Get<TEnt, TDto>(TDto dto)
            where TEnt : Entity<TEnt>
            where TDto : DataTransferObject<TDto>

        {
            return Instance.GetFromInstance<TEnt, TDto>(dto);
        }
    }
}