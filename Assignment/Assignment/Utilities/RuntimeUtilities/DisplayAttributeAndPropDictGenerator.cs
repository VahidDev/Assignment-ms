using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    internal static class DisplayAttributeAndPropDictGenerator
    {
        public static Dictionary<string, PropertyInfo> CreateDict
            (
            IReadOnlyCollection<PropertyInfo> props, 
            Dictionary<string, PropertyInfo> displayAttributeNameAndPropDict
            )
        {
            foreach (PropertyInfo prop in props)
            {
                string? name = prop.GetCustomAttribute<DisplayAttribute>()?
                    .Name?.Trim().ToLower();
                if (name == null)
                    continue;
                if (prop.IsInNamespace(nameof(DomainModels)))
                {
                    CreateDict(prop.PropertyType.GetProperties(), 
                        displayAttributeNameAndPropDict);
                }
                displayAttributeNameAndPropDict.Add(name, prop);
            }
            return displayAttributeNameAndPropDict;
        }
    }
}
