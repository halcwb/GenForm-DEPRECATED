using System;
using System.Web.Mvc;
using Informedica.GenForm.Presentation.Forms;

namespace Informedica.GenForm.Mvc2.Controllers
{
    public class ActionResultParser
    {
        public static bool GetSuccessValueFromActionResult(ActionResult response)
        {
            var json = CastToJsonResult(response);
            try
            {
                return (bool)GetValueFromProperty(json, "success");
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static object GetValueFromProperty(JsonResult json, String propertyName)
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

        public static T GetValueFromActionResult<T>(ActionResult result, System.String propertyName)
        {
            var json = CastToJsonResult(result);

            try
            {
                return (T) GetValueFromProperty(json, propertyName);
            }
            catch (Exception)
            {
                return default(T);                
            }  
        }

    }
}