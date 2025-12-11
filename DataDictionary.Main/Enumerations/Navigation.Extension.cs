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

        /// <summary>
        /// Gets the Icon for the Scope
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Scope is not in Navigation Enumeration</exception>
        public static Icon GetIcon(this ScopeType scope)
        { return Enumeration.GetValue(scope).WindowIcon; }

        public static ImageList AddImages(this ImageList target, params IEnumerable<ScopeType> scopes)
        {
            foreach (ScopeType item in scopes.Distinct())
            {
                if (Enumeration.TryGetValue(item, out Enumeration? value)
                    && value.CommandImages.TryGetValue(CommandType.Default, out Func<Image>? image))
                { target.Images.Add(value.Name, image()); }
            }

            return target;
        }

        /// <summary>
        /// Creates an ImageList using the defined images for all the Scopes.
        /// </summary>
        /// <returns></returns>
        public static ImageList CreateImageList()
        {
            ImageList result = new ImageList();

            foreach (ScopeType item in Enum.GetValues<ScopeType>())
            {
                if (Enumeration.TryGetValue(item, out Enumeration? value)
                    && value.CommandImages.TryGetValue(CommandType.Default, out Func<Image>? image))
                { result.Images.Add(value.Name, image()); }
            }

            return result;
        }
    }
}
