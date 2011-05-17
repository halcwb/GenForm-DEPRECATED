using System;
using System.Collections;
using System.Data.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Ext.Direct.Mvc;
using Informedica.GenForm.Library.DomainModel.Formularia;
using Informedica.GenForm.Library.DomainModel.Administration;

namespace Informedica.GenForm.Mvc2.Controllers
{
    public class FormulariumController : Controller
    {

        public ActionResult GetFormulariumTree(string node)
        {
            FormulariumNameList list = FormulariumNameList.GetNameValueList();
            ArrayList tree = new ArrayList();
            if (node == "root")
            {
                foreach (var f in list)
                {
                    Hashtable treeNode = new Hashtable();
                    treeNode.Add("id", f.Key);
                    treeNode.Add("isLeaf", false);
                    treeNode.Add("text", f.Value);
                    treeNode.Add("type", "Formularium");
                    tree.Add(treeNode);
                }
            }
            return this.Direct(tree);
        }

        public ActionResult GetFormularium(int id)
        {
            Formularium formularium = null;
            if (Formularium.Exists(id)) formularium = Formularium.GetFormularium(id);

            return this.Direct(formularium);
        }

        [FormHandler]
        public ActionResult SaveFormularium(FormulariumData formulariumData)
        {
            Formularium formulariumToSave = null;

            // Look whether Formularium exists. Then get that Formularium, else create a new one
            if (Formularium.Exists(formulariumData.Id))
                formulariumToSave = Formularium.GetFormularium(formulariumData.Id);
            else formulariumToSave = Formularium.NewFormularium(formulariumData.FormulariumName, formulariumData.MainAuthorUserName);

            bool canBeMapped = true;
            if (!formulariumToSave.IsNew)
                canBeMapped = CheckVersion(formulariumToSave.TimeStamp, formulariumData.TimeStamp);
            
            if (canBeMapped)
            {
                // Map the properties of formulariumData and formulariumToSave, except the MainGroups collection 
                string[] exlude = { "MainGroups", "Texts", "DoseTexts", 
                                    "ProductTexts", "GenericTexts", 
                                    "Pharmacy", "PharmacyId" };

                Csla.Data.DataMapper.Map(formulariumData, formulariumToSave, exlude);
            }

            // Return the result
            return this.Direct(
                new
                {
                    success = formulariumToSave.IsSavable,
                    data = formulariumToSave.IsSavable ? formulariumToSave.Save(): formulariumToSave,
                    msg = formulariumToSave.BrokenRulesCollection.ToString()
                });
        }

        private bool CheckVersion(byte[] serverVersion, byte[] clientVersion)
        {
            return (new Binary(serverVersion)).ToString() == (new Binary(clientVersion)).ToString();
        }

        public ActionResult GetAuthors()
        {
            PharmacistUserNameList list = PharmacistUserNameList.GetNameValueList();
            return this.Direct(list);
        }

        public Newtonsoft.Json.Linq.JObject GetFormulariumById(Newtonsoft.Json.Linq.JObject request)
        {
            Formularium formularium = Formularium.GetFormularium(request.Value<int>("id"));
            Newtonsoft.Json.JsonSerializer ser = new JsonSerializer();
            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.FromObject(formularium, ser);

            return json;
        }

        [FormHandler]
        public Newtonsoft.Json.Linq.JObject UpdateFormulariumName(int id, string newName)
        {
            bool isSuccess = false;
            //int id = request.Value<int>("id");
            //string newName = request.Value<string>("newName");

            Formularium formularium = null;
            if (Formularium.Exists(id) && !string.IsNullOrEmpty(newName)) 
                formularium = Formularium.GetFormularium(id);

            if (formularium != null)
            {
                formularium.FormulariumName = newName;
                if (formularium.IsSavable)
                {
                    formularium = formularium.Save();
                    isSuccess = true;
                }
            }

            Newtonsoft.Json.JsonSerializer ser = new JsonSerializer();
            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.FromObject(new { success = isSuccess }, ser);

            return json;
        }

        [FormHandler]
        public Newtonsoft.Json.Linq.JObject InsertFormularium(string newName)
        {
            bool isSuccess = false;

            Newtonsoft.Json.JsonSerializer ser = new JsonSerializer();
            Newtonsoft.Json.Linq.JObject json = Newtonsoft.Json.Linq.JObject.FromObject(new { success = isSuccess }, ser);

            return json;
        }

        public class FormulariumData: IFormulariumValidation
        {
            private int _id;
            public int Id
            { get { return _id; } set { _id = value; } }

            private byte[] _timeStamp;
            public byte[] TimeStamp
            { get { return _timeStamp; } set { _timeStamp = value; } }

            #region IFormulariumValidation Members

            public string FormulariumName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string MainAuthorUserName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public string PharmacyName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            #endregion
        }
    }
}
