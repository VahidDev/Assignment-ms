using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class CustomObjectCreatorAndSetter
    {
        public static void CreateCustomObjectAndSetToProperty
          (this PropertyInfo propertyInfo, Dictionary<string, object> propNameAndValueDict,
          object parentObj)
        {
            object customObj = propertyInfo
                               .CreateCustomObject(propNameAndValueDict, parentObj);
            propertyInfo?.SetValue(parentObj,
                Convert.ChangeType(customObj, propertyInfo.PropertyType), null);
        }
    }
}
