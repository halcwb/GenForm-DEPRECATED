using System;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenForm.Assembler.Contexts
{
    public class SessionContext: ICurrentSessionContext, IDisposable
    {
        public SessionContext()
        {
            try
            {
                CurrentSession();
                DisposeSession();
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {}
            finally
            {
                CurrentSessionContext.Bind(GenFormApplication.SessionFactory.OpenSession());
            }
        }

        public void Dispose()
        {
            DisposeSession();
        }

        private static void DisposeSession()
        {
            var session = CurrentSessionContext.Unbind(GenFormApplication.SessionFactory);
            session.Close();
            session.Dispose();
        }

        public ISession CurrentSession()
        {
            return GenFormApplication.SessionFactory.GetCurrentSession();
        }

        public static SessionContext UseContext()
        {
            return new SessionContext();
        }
    }
}