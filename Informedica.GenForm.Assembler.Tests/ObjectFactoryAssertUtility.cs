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

        public static void AssertRegistrationWith<T,TC>(T parameter)
        {
            var instance = ObjectFactory.With(parameter).GetInstance<TC>();
            Assert.IsNotNull(instance);
        }

        public static String GetMessageFor<T>()
        {
            return "No implementation was found for " + typeof(T).FullName;
        }
    }
}
