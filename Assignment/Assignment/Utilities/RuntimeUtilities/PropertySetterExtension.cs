using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class PropertySetterExtension
    {
        public static void SetPropertyValue<T>
            (this PropertyInfo propertyInfo,T item,object value)
        {

            if (propertyInfo.IsInNamespace(nameof(Nullable)))
            {
                value=propertyInfo.ChangeTypeToNullableType(value);
            }
            else 
            {
                value=propertyInfo.ChangeTypeToValueType(value);
            }
            propertyInfo.SetValue(item, value);
        }
    }
}
