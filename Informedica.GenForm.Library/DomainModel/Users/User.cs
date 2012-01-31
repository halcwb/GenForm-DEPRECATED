using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Users.Interfaces;
using Informedica.GenForm.Library.DomainModel.Validation;

namespace Informedica.GenForm.Library.DomainModel.Users
{
    public class User: Entity<User>, IUser
    {
        #region Private

        #endregion

        #region Construction

        static User()
        {
            RegisterValidationRules();
        }

        protected User()
        {
        }

        #endregion

        #region Business

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string Email { get; set; }

        public virtual string Pager { get; set; }

        public virtual string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public virtual bool IsAuthenticated
        {
            get { throw new NotImplementedException(); }

        }

        #endregion

        #region Factory Methods

        public static User Create(UserDto userDto)
        {
            var user = new User
                       {
                           Email = userDto.Email,
                           FirstName = userDto.FirstName,
                           LastName = userDto.LastName,
                           Pager = userDto.Pager,
                           UserName = userDto.Name,
                           Name = userDto.Name,
                           Password = userDto.Password,
                       };
            Validate(user);
            return user;
        }

        #endregion

        #region Validation

        private static void RegisterValidationRules()
        {
            ValidationRulesManager.RegisterRule<User>(x => !String.IsNullOrWhiteSpace(x.Email), "Email is verplicht");
            ValidationRulesManager.RegisterRule<User>(x => !String.IsNullOrWhiteSpace(x.Name), "Naam is verplicht");
            ValidationRulesManager.RegisterRule<User>(x => !String.IsNullOrWhiteSpace(x.LastName), "Achternaam is verplicht");
            ValidationRulesManager.RegisterRule<User>(x => !String.IsNullOrWhiteSpace(x.FirstName), "Voornaam is verplicht");
            ValidationRulesManager.RegisterRule<User>(x => !String.IsNullOrWhiteSpace(x.Password), "Paswoord is verplicht");
        }

        #endregion
    }
}
