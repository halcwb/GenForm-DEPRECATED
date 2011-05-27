using System;
using System.Reflection;

namespace Informedica.Utilities
{
    public static class ObjectExaminer
    {
        private const string EmptyNumericalValue = "0";
        private const string EmptyBooleanValue = "false";

        public static Boolean ObjectHasEmptyProperties(Object obj)
        {
            if (obj == null) return false;

            foreach (var property in obj.GetType().GetProperties())
            {
                if (!PropertyIsEmpty(property, obj)) return false;
            }
            return true;
        }

        public static Boolean PropertyIsEmpty(PropertyInfo property, Object product)
        {

            if (String.IsNullOrEmpty(GetStringValueFromPropertyInfo(property, product))) return true;
            if (GetStringValueFromPropertyInfo(property, product) == EmptyNumericalValue) return true;
            return GetStringValueFromPropertyInfo(property, product) == EmptyBooleanValue;
        }

        public static string GetStringValueFromPropertyInfo(PropertyInfo property, object obj)
        {
            if (property.GetValue(obj, new object[] { }) == null) return String.Empty;
            return property.GetValue(obj, new object[] { }).ToString();
        }
    
    }
}
