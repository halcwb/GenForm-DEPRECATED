using System;
using System.Linq;
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

            return obj.GetType().GetProperties().All(property => PropertyIsEmpty(property, obj));
        }

        public static Boolean PropertyIsEmpty(PropertyInfo property, Object product)
        {
            var value = GetStringValueFromPropertyInfo(property, product);

            if (String.IsNullOrEmpty(value)) return true;
            if (value == EmptyNumericalValue) return true;
            // ToDo solve examining objects with collections
            if (value.Contains("System.Collections")) return true;
            return value == EmptyBooleanValue;
        }

        public static string GetStringValueFromPropertyInfo(PropertyInfo property, object obj)
        {
            if (property.GetValue(obj, new object[] { }) == null) return String.Empty;
            return property.GetValue(obj, new object[] { }).ToString();
        }
    
    }
}
