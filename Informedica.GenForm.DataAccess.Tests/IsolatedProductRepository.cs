using System;
using Informedica.GenForm.DataAccess.DataMappers;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using StructureMap;
using TypeMock.ArrangeActAssert;
using Product = Informedica.GenForm.Database.Product;

namespace Informedica.GenForm.DataAccess.Tests
{
    public class IsolatedProductRepository
    {
        private ProductMapper _productMapper;
        private IProduct _product;
        private Product _productDao;
        private GenFormDataContext _dataContext;
        private ProductRepository _repository;

        private static IsolatedProductRepository _instance;
        private static readonly Object LockThis = new Object();

        public static IsolatedProductRepository Instance
        {
            get
            {
                if (_instance == null)
                    lock (LockThis)
                    {
                        if (_instance == null)
                        {
                            _instance = new IsolatedProductRepository();
                        }
                    }
                return _instance;
            }
        }

        public ProductMapper ProductMapper
        {
            get { return _productMapper; }
        }

        public IProduct ProductBO
        {
            get { return _product; }
        }

        public Product ProductDao
        {
            get { return _productDao; }
        }

        public GenFormDataContext DataContext
        {
            get { return _dataContext; }
        }

        public ProductRepository Repository
        {
            get { return _repository; }
        }

        public void IsolateProductRepository()
        {
            _repository = CreateProductRepository();
            _product = ObjectFactory.GetInstance<IProduct>();

            _productMapper = CreateFakeProductMapper();
            _productDao = CreateFakeProductDao();
            Isolate.WhenCalled(() => ProductMapper.MapFromBoToDao(ProductBO, ProductDao)).IgnoreCall();

            _dataContext = CreateFakeDatabaseContext();
            Isolate.WhenCalled(() => DataContext.SubmitChanges()).IgnoreCall();
        }

        private  static Product CreateFakeProductDao()
        {
            return Isolate.Fake.Instance<Product>();
        }

        private static ProductMapper CreateFakeProductMapper()
        {
            var mapper = Isolate.Fake.Instance<ProductMapper>();
            ObjectFactory.Inject(mapper);
            return mapper;
        }

        private static GenFormDataContext CreateFakeDatabaseContext()
        {
            var context  = Isolate.Fake.Instance<GenFormDataContext>();
            ObjectFactory.Inject(context);

            return context;
        }

        private static ProductRepository CreateProductRepository()
        {
            return new ProductRepository();
        }

    }
}