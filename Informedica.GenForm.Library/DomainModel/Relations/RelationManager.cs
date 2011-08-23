using System;
using System.Collections.Generic;
using System.Threading;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    public class RelationManager
    {
        private static RelationManager _instance;
        private static readonly object LockThis = new object();

        private readonly IDictionary<Type, IRelation<IRelationPart, IRelationPart>> _registry = new Dictionary<Type, IRelation<IRelationPart, IRelationPart>>();

        public RelationManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            _registry.Add(typeof(IRelation<UnitGroup, Unit>), new OneToManyRelation<UnitGroup, Unit>());
            _registry.Add(typeof(IRelation<Shape, Product>), new OneToManyRelation<Shape, Product>());
            _registry.Add(typeof(IRelation<Package, Product>), new OneToManyRelation<Package, Product>());
            _registry.Add(typeof(IRelation<Brand, Product>), new OneToManyRelation<Brand, Product>());
            _registry.Add(typeof(IRelation<SubstanceGroup, Substance>), new OneToManyRelation<SubstanceGroup, Substance>());
            _registry.Add(typeof(IRelation<Shape, UnitGroup>), new ManyToManyRelation<Shape, UnitGroup>());
            _registry.Add(typeof(IRelation<Shape, Package>), new ManyToManyRelation<Shape, Package>());
            _registry.Add(typeof(IRelation<Shape, Route>), new ManyToManyRelation<Shape, Route>());
            _registry.Add(typeof(IRelation<Route, Product>), new ManyToManyRelation<Route, Product>());
        }

        public static void Add<TLeft,TRight>(Type type, IRelation<TLeft, TRight> relation)
            where TLeft : IRelationPart
            where TRight : IRelationPart
        {
            if (Instance._registry.ContainsKey(type)) return;
            Instance._registry.Add(type, (IRelation<IRelationPart, IRelationPart>)relation);
        }

        public static RelationManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            var instance = new RelationManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }
                return _instance;
            }
        }

        public static OneToManyRelation<T1, T2> OneToMany<T1, T2>()
            where T1 : class, IRelationPart
            where T2 : class, IRelationPart
        {
            var type = typeof (IRelation<T1, T2>);
            if (!Instance._registry.ContainsKey(type)) throw new CannotFindRelationException(type);
            var result = Instance._registry[typeof (IRelation<T1, T2>)];
            return (OneToManyRelation<T1, T2>)result;
        }

        public static OneToOneRelation<T1, T2> OneToOne<T1, T2>()
            where T1 : class, IRelationPart
            where T2 : class, IRelationPart
        {
            var result = Instance._registry[typeof (IRelation<T1, T2>)];
            return (OneToOneRelation<T1, T2>)result;
        }

        public static ManyToManyRelation<T1, T2> ManyToMany<T1, T2>()
            where T1 : class, IRelationPart
            where T2 : class, IRelationPart
        {
            var result = Instance._registry[typeof (IRelation<T1, T2>)];
            return (ManyToManyRelation<T1, T2>)result;
        }
    }

    public class CannotFindRelationException : Exception
    {
        public CannotFindRelationException(Type type) : base (type.ToString())
        {
            
        }
    }
}