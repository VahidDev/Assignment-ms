using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class CustomObjectCreatorAndSetter
    {
        public static void CreateCustomObjectAndSetToProperty
          (this PropertyInfo propertyInfo, 
            IDictionary<string, object> propNameAndValueDict,object parentObj)
        {
            object? newObj = Activator.CreateInstance(propertyInfo.PropertyType);
            if (newObj != null)
            {
                object customObj = propertyInfo
                                   .CreateCustomObject(propNameAndValueDict, newObj);
                propertyInfo?.SetValue(parentObj,
                    Convert.ChangeType(customObj, propertyInfo.PropertyType), null);
            }
        }
    }
}
