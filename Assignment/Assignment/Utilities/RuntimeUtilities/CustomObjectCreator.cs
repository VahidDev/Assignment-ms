using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class CustomObjectCreator
    {
        public static object CreateCustomObject
           (this PropertyInfo propertyInfo, Dictionary<string, object> propValueDict,
           object item)
        {
            IReadOnlyCollection<PropertyInfo> props = item.GetType().GetProperties();

            foreach (PropertyInfo prop in props)
            {
                string? name = prop.GetCustomAttribute<DisplayAttribute>()?.Name;
                if (name == null)
                    continue;
                if (prop.IsInNamespace(nameof(DomainModels)))
                {
                    object? newItem = Activator.CreateInstance(prop.PropertyType);
                    object customObj = prop.CreateCustomObject(propValueDict, newItem);
                    prop?.SetValue(item,
                        Convert.ChangeType(customObj, prop.PropertyType), null);
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
