using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.Assembler.Tests
{
    public static class ObjectFactoryAssertUtility
    {
        public static void AssertRegistration<T>(string message)
        {
            try
            {
                Assert.IsInstanceOfType(ObjectFactory.GetInstance<T>(), typeof(T), message);

            }
            catch (Exception e)
            {
                Assert.Fail(message + ": " + e);
            }
        }

        public static void AssertRegistationWith<T,C>(T connection)
        {
            var context = ObjectFactory.With(connection).GetInstance<C>();
            Assert.IsNotNull(context);
        }

        public static String GetMessageFor<T>()
        {
            return "No implementation was found for " + typeof(T).FullName;
        }
    }
}
