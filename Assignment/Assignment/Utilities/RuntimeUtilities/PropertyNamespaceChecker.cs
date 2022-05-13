using System.Reflection;

namespace Assignment.Utilities.RuntimeUtilities
{
    public static class PropertyNamespaceChecker
    {
        public static bool IsInNamespace(this PropertyInfo prop,string namespaceName)
        {
            if (prop.PropertyType.FullName.Contains(namespaceName))
                return true;
            return false;
        }
    }
}
