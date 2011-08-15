using System;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenForm.Assembler.Contexts
{
    public class SessionContext: ICurrentSessionContext, IDisposable
    {
        public SessionContext()
        {
            CurrentSessionContext.Bind(GenFormApplication.SessionFactory.OpenSession());   
        }

        public void Dispose()
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