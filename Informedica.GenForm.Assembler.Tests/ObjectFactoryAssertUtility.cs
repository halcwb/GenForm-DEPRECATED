using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Assembler.Tests
{
    public static class ObjectFactoryAssertUtility
    {
        public static void AssertRegistration<T>(string message)
        {
            Assert.IsInstanceOfType(ObjectFactory.GetInstance<T>(), typeof(T), message);
        }

        public static void AssertRegistationWith<T,C>(T connection)
        {
            var context = ObjectFactory.With(connection).GetInstance<C>();
            Assert.IsNotNull(context);
        }
    }
}
