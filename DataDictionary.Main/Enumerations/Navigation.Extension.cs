using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    static partial class NavigationExtention
    {
        /// <summary>
        /// Try/Get the Navigation information for the Scope enum.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="navigation"></param>
        /// <returns></returns>
        public static Boolean TryGetValue(this ScopeType scope, [NotNullWhen(true)] INavigationEnumeration? navigation)
        {
            if (Enumeration.TryGetValue(scope, out Enumeration? result))
            { navigation = result; return true; }
            else { navigation = null; return false; }
        }
    }
}
