using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class TypeSetterExtension
    {
        public static object ChangeTypeToValueType(this PropertyInfo prop,object value)
            =>Convert.ChangeType(value, prop.PropertyType);
        public static object ChangeTypeToNullableType(this PropertyInfo prop, object value)
        {
            Type? t = Nullable.GetUnderlyingType(prop.PropertyType);
            return Convert.ChangeType(value, t); 
        }
    }
}
