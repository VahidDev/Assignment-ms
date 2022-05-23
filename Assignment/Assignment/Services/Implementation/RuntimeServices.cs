using Assignment.Services.Abstraction;
using Assignment.Utilities.RuntimeUtilities;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment.Services.Implementation
{
    public class RuntimeServices : IRuntimeServices
    {
        public T CreateCustomObject<T>(IDictionary<string, object> propNameAndValueDict)
        {
            T parentObj = Activator.CreateInstance<T>();
            IReadOnlyCollection<PropertyInfo> allProprs = typeof(T).GetProperties()
                .ToList();
            foreach (PropertyInfo prop in allProprs)
            {
                // ignore if prop doesn't have DisplayAttribute
                if (prop.GetCustomAttribute<DisplayAttribute>() == null)
                    continue;

                if (prop.IsInNamespace(nameof(DomainModels)))
                {
                    if (parentObj != null)
                        prop.CreateCustomObjectAndSetToProperty
                            (propNameAndValueDict, parentObj);
                }
                else
                {
                    prop.SetPropertyValue<T>(parentObj,
                        propNameAndValueDict
                        .FirstOrDefault(p => p.Key.Trim().ToLower() 
                        == prop.GetCustomAttribute<DisplayAttribute>()?
                        .Name?.Trim().ToLower()).Value);
                }
            }
            return parentObj;
        }
    }
}
