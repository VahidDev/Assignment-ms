using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class CustomObjectCreator
    {
        public static object CreateCustomObject
           (this PropertyInfo propertyInfo, Dictionary<string, object> propValueDict,
           object parentObj)
        {
            object? item = Activator.CreateInstance(propertyInfo.PropertyType);

            IReadOnlyCollection<PropertyInfo> props = item.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                string? name = prop.GetCustomAttribute<DisplayAttribute>()?.Name;
                if (name == null)
                    continue;
                if (prop.IsInNamespace(nameof(DomainModels)))
                {
                    object customObj = prop.CreateCustomObject(propValueDict, parentObj);
                    prop?.SetValue(customObj,
                        Convert.ChangeType(parentObj, prop.PropertyType), null);
                }
                else
                {
                    prop.SetPropertyValue
                        (item, propValueDict.FirstOrDefault(p => p.Key == name).Value);
                }
            }
            return item;
        }

    }
}
