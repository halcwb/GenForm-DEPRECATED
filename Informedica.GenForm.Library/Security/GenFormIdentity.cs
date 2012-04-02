using System;
using System.Security.Principal;
using Informedica.GenForm.Library.DomainModel.Users;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;

namespace Informedica.GenForm.Library.Security
{
    internal class GenFormIdentity: GenericIdentity, IGenFormIdentity
    {
        private GenFormIdentity(String name) : base(name)
        {
        }

        internal static IGenFormIdentity GetIdentity(String name)
        {
            return CreateIdentity(name, String.Empty);
        }

        private static IGenFormIdentity CreateIdentity(String name, String password)
        {
            if (name == null) throw new ArgumentNullException("name");

            var user = GetUserRepository().GetByName(name);
            if (user == null || String.IsNullOrWhiteSpace(password) || user.Password != password) return new AnonymousIdentity();
            return new GenFormIdentity(name);
        }

        private static IRepository<User> GetUserRepository()
        {
            return RepositoryFactory.Create<User>();
        }

        internal static IGenFormIdentity GetIdentity(ILoginCriteria criteria)
        {
            return CreateIdentity(criteria.UserName, criteria.Password);
        }
    }
}
