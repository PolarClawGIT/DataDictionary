using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    static partial class NavigationExtention
    {
        /// <summary>
        /// Gets the Image for the Scope and Status
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static Image GetImage(this ScopeType scope, StatusType status)
        {
            if (NavigationExtention.Enumeration.TryGetValue(scope, out Enumeration? value))
            {
                if (value.StatusImages.TryGetValue(status, out Func<Image>? image))
                { return image(); }
                else if (value.CommandImages.TryGetValue(CommandType.Default, out Func<Image>? defaultImage))
                { return defaultImage(); }
                else
                { throw new IndexOutOfRangeException(String.Format("Status Type unkown: {0}", status.ToString())); }
            }
            else
            { throw new IndexOutOfRangeException(String.Format("Scope Type unkown: {0}", scope.ToString())); }
        }

        /// <summary>
        /// Try/Get the Image for the Scope and Status.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="status"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ScopeType scope, StatusType status, [NotNullWhen(true)] out Image? result)
        {
            if (Enumeration.TryGetValue(scope, out Enumeration? value)
                && value.StatusImages.TryGetValue(status, out Func<Image>? image))
            { result = image(); return true; }
            else { result = null; return false; }
        }
    }
}
