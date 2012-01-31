using System;
using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class ActionResultParser
    {
        public static bool GetSuccessValue(ActionResult response)
        {
            var json = CastToJsonResult(response);
            try
            {
                return (bool)GetPropertyValue(json, "success");
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static object GetPropertyValue(JsonResult json, String propertyName)
        {
            return json.Data.GetType().GetProperty(propertyName).GetValue(json.Data, null);
        }

        private static JsonResult CastToJsonResult(ActionResult response)
        {
            try
            {
                return (JsonResult)(response);

            }
            catch (Exception e)
            {
                throw new InvalidCastException(e.ToString());
            }
        }

        public static T GetPropertyValue<T>(ActionResult result, String propertyName)
        {
            var json = CastToJsonResult(result);

            try
            {
                return (T) GetPropertyValue(json, propertyName);
            }
            catch (Exception)
            {
                return default(T);                
            }  
        }

    }
}