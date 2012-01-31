using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Factories
{
    /// <summary>
    /// Summary description for FluentFactoryTest
    /// </summary>
    [TestClass]
    public class FluentFactoryTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ThatFactoryShouldStartWithInit()
        {

        }
    }

    public static class GenericFluentFactory<T>
    {
        public static IGenericFactory<T> Init(T entity)
        {
            return new GenericFactory<T>(entity);
        }
    }

    public class GenericFactory<T> : IGenericFactory<T>
    {
        private readonly T _entity;

        public GenericFactory(T entity)
        {
            _entity = entity;
        }

        public IGenericFactory<T> AddPropertyValue(Expression<Func<T, object>> property, object value)
        {
            PropertyInfo propertyInfo = null;
            if (property.Body is MemberExpression)
            {
                var memberExpression= property.Body as MemberExpression;
                if (memberExpression != null)
                    propertyInfo = memberExpression.Member as PropertyInfo;
            }
            else
            {
                var memberExpression = ((UnaryExpression) property.Body).Operand as MemberExpression;
                if (memberExpression != null)
                    propertyInfo = memberExpression.Member as PropertyInfo;
            }
            if (propertyInfo != null) propertyInfo.SetValue(_entity, value, null);

            return this;
        }

        public T Create()
        {
            return _entity;
        }
    }

    public interface IGenericFactory<T>
    {
        IGenericFactory<T> AddPropertyValue(Expression<Func<T, object>> property, object value);
        T Create();
    }
}
