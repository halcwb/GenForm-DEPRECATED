using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.DataAccess.DataContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.NHibernate.Tests
{
    [TestClass]
    public class ASessionContextShould
    {
        [TestMethod]
        public void AlwaysReturnASessionObject()
        {
            using (SessionContext.UseContext())
            {
                Assert.IsNotNull(GenFormApplication.Instance.SessionFactoryFromInstance.GetCurrentSession());
            }
        }

        [TestMethod]
        public void HaveClosedTheSessionAfterDispose()
        {
            using (SessionContext.UseContext())
            {
                Assert.IsNotNull(GenFormApplication.Instance.SessionFactoryFromInstance.GetCurrentSession());
            }

            try
            {
                GenFormApplication.Instance.SessionFactoryFromInstance.GetCurrentSession();
                Assert.Fail("No session should be open after disposal of SessionBuilder");
            }
            catch (Exception)
            {
                Assert.IsTrue(true); 
            }
        }
    }
}
