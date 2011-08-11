using System;
using NHibernate;
using NHibernate.Context;

namespace Informedica.GenForm.Assembler.Contexts
{
    public class SessionContext: ICurrentSessionContext, IDisposable
    {
        public SessionContext()
        {
            CurrentSessionContext.Bind(GenFormApplication.Instance.SessionFactoryFromInstance.OpenSession());   
        }

        public void Dispose()
        {
            var session = CurrentSessionContext.Unbind(GenFormApplication.Instance.SessionFactoryFromInstance);
            session.Close();
        }

        public ISession CurrentSession()
        {
            return GenFormApplication.Instance.SessionFactoryFromInstance.GetCurrentSession();
        }

        public static SessionContext UseContext()
        {
            return new SessionContext();
        }
    }
}