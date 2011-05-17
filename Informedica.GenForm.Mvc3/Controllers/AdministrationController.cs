using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csla;
using Csla.Data;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Administration;
using Informedica.GenForm.Library.DomainModel.Formularia;
using Informedica.GenForm.Library.Security;
using Informedica.GenForm.Library.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Informedica.GenForm.Mvc2.Controllers
{
    public class AdministrationController : Controller
    {
        public ActionResult DeleteUser(int userId)
        {
            bool success = false;
            string msg = string.Empty;

            if (Library.DomainModel.Administration.User.Exists(userId))
            {
                try
                {
                    Library.DomainModel.Administration.User.DeleteUser(userId);
                }
                catch (Exception e)
                {
                    success = false;
                    msg = e.Message;
                }
            }
            success = !Library.DomainModel.Administration.User.Exists(userId);

            return this.Direct(new {success, responseText = msg});
        }

        public ActionResult GetUserNameMaxLength()
        {
            return this.Direct(UserValidationRules.UserNameMaxLength);
        }

        public ActionResult GetUserList()
        {
            UserInfoList list = UserInfoList.GetUserInfoList();
            return this.Direct(list);
        }

        public ActionResult GetUserFormularia(int userId)
        {
            User user = null;
            Pharmacist pharmacist = null;

            if (Library.DomainModel.Administration.User.Exists(userId))
                user = Library.DomainModel.Administration.User.GetUser(userId);
            if (user.IsPharmacist)
                pharmacist = Pharmacist.GetPharmacistByUserId(userId);

            // if user is not a pharmacist, return an empty formularia list
            // otherwise return the formularia list for the pharmacist user
            if (pharmacist == null) return this.Direct(new {});
            else return this.Direct(pharmacist.FormulariumList);
        }

        public ActionResult GetFormulariaList()
        {
            return this.Direct(FormulariumNameList.GetNameValueList());
        }

        public ActionResult GetUserRoles(int userId)
        {
            User user = GetUserById(userId);
            return this.Direct(user.Roles);
        }

        public ActionResult SaveUserRole(int userId, UserRole data)
        {
            bool success = false;
            string msg = string.Empty;
            User user = null;

            // Check whether userid is valid for user
            if (Library.DomainModel.Administration.User.Exists(userId))
            {
                user = Library.DomainModel.Administration.User.GetUser(userId);

                // add the userrole and check whether the user can be saved
                try
                {
                    user.Roles.AddRole(data.RoleId);

                    if (user.IsSavable)
                    {
                        success = true;
                        user = user.Save();
                        data = user.Roles.Single(o => o.RoleId == data.RoleId);
                    }
                    else
                    {
                        msg = user.BrokenRulesCollection.ToString();
                    }
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
            else
            {
                msg = "Gebruiker bestaat niet";
            }

            return this.Direct(new {success, userRoles = user == null ? null : data, msg});
        }

        public ActionResult DeleteUserRole(int userId, int userRoleId)
        {
            User user = null;
            bool success = true;
            string msg = string.Empty;

            try
            {
                user = Library.DomainModel.Administration.User.GetUser(userId);
                user.Roles.RemoveById(userRoleId);
                user.Save();
            }
            catch (Exception e)
            {
                success = false;
                msg = e.Message;
            }

            return this.Direct(new {success, msg});
        }

        private static User GetUserById(int userId)
        {
            User user =
                Library.DomainModel.Administration.User.GetUser(userId);
            return user;
        }

        public ActionResult GetUser(int userId)
        {
            User user = GetUserById(userId);
            return this.Direct(new {success = true, data = user});
        }

        /// <summary>
        /// Only saves the User parent object, saving the 
        /// children, i.e. needs another method
        /// </summary>
        /// <param name="userdata">New populated User object</param>
        /// <returns>Succes and Saved object</returns>
        [FormHandler]
        public ActionResult SaveUser(User userdata)
        {
            User userToSave = null;
            string msg = string.Empty;
            bool success = true;

            // Look whether the user exists. Then get that user, else create a new one
            if (Library.DomainModel.Administration.User.Exists(userdata.UserId))
            {
                userToSave = Library.DomainModel.Administration.User.GetUser(userdata.UserId);
                // check for concurrency errors
                if ((new Binary(userToSave.TimeStamp)).ToString() != (new Binary(userdata.TimeStamp)).ToString())
                {
                    userToSave = null;
                    success = false;
                    msg = "Concurrency Error";
                }
            }
            else userToSave = Library.DomainModel.Administration.User.NewUser();

            // Map the properties of userdata and userTosave, except the Roles collection
            // Roles collection is empty in userdata, needs a seperate method call
            if (userToSave != null)
            {
                DataMapper.Map(userdata, userToSave, "Roles");
            }

            try
            {
                userToSave = userToSave.Save();
            }
            catch (Exception e)
            {
                msg = e.Message;
                success = false;
            }

            // Return the result
            return this.Direct(
                new
                    {
                        success,
                        msg,
                        errors = userToSave == null ? string.Empty : userToSave.BrokenRulesCollection.ToString(),
                        data = userToSave == null ? null : userToSave
                    });
        }

        public ActionResult GetPharmacy()
        {
            Pharmacy pharmacy = Pharmacy.GetPharmacy();
            return this.Direct(pharmacy);
        }

        public ActionResult GetRolesList()
        {
            RoleNameList list = RoleNameList.GetNameValueList();
            return this.Direct(list);
        }

        [FormHandler]
        [ValidateInput(false)]
        public ActionResult GenFormLogin(string userName, string password)
        {
            ILoginServices loginServices = new LoginServices();
            LoginUser user = LoginUser.NewLoginUser(userName, password);
            loginServices.Login(user);

            if (loginServices.IsLoggedIn(user))
            {
                DateTime expires = DateTime.Now.AddHours(1);
                var loginCookie = new HttpCookie("loginCookie", ApplicationContext.User.Identity.Name);
                Session["user"] = ApplicationContext.User;
                loginCookie.Expires = expires;
                Response.AppendCookie(loginCookie);
            }

            return this.Direct(new
                                   {
                                       success = loginServices.IsLoggedIn(user),
                                       data = new
                                                  {
                                                      user = user.UserName,
                                                      password = user.Password
                                                  }
                                   });
        }

        [FormHandler]
        public JObject GenFormLogOut(string user)
        {
            Session.Remove("user");
            GenFormPrincipal.Logout();

            var ser = new JsonSerializer();
            JObject json = JObject.FromObject(new {success = true}, ser);

            return json;
        }

        #region Nested type: LoginResponse

        [Serializable]
        private class LoginResponse
        {
            private readonly bool _Success;
            private readonly string _Title;

            public LoginResponse(bool success, string title)
            {
                _Success = success;
                _Title = title;
            }

            public bool success
            {
                get { return _Success; }
                set { ; }
            }

            public string data
            {
                get { return _Title; }
                set { ; }
            }
        }

        #endregion

        #region Nested type: TestList

        public class TestList
        {
            private readonly List<string> _list = new List<string>();

            public TestList()
            {
                _list.Add("Test1");
                _list.Add("Test2");
                _list.Add("Test3");
            }
        }

        #endregion
    }
}