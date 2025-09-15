using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;

namespace DataDictionary.Main.Enumerations
{
    static partial class NavigationExtention
    {
        /// <summary>
        /// Gets the Navigation information for the Scope enum.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        //public static INavigationEnumeration GetNavigation(this ScopeType scope)
        //{ return Enumeration.GetValue(scope); }

        /// <summary>
        /// Gets the default Image for the Scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static Image GetImage(this ScopeType scope)
        { return Enumeration.GetValue(scope).GetImage(CommandType.Default); }

        /// <summary>
        /// Get the Image for the scope for the specfic command.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Image GetImage(this ScopeType scope, CommandType command)
        { return Enumeration.GetValue(scope).GetImage(command); }

        /// <summary>
        /// Gets the Icon for the Scope
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static Icon GetIcon(this ScopeType scope)
        { return Enumeration.GetValue(scope).WindowIcon; }

        /// <summary>
        /// Sets the Image List for a TreeView.
        /// </summary>
        /// <param name="target"></param>
        public static void SetImageList(this TreeView target)
        { target.ImageList = Enumeration.AsImageList(); }

        /// <summary>
        /// Sets the Image List for a ListView.
        /// </summary>
        /// <param name="target"></param>
        public static void SetImageList(this ListView target)
        { target.SmallImageList = Enumeration.AsImageList(); }
    }
}
