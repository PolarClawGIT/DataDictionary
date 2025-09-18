using DataDictionary.Resource.Enumerations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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

        /// <summary>
        /// Gets the Image for the Scope and Command
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Scope or Command is not in Navigation Enumeration</exception>
        public static Image GetImage(this ScopeType scope, CommandType command)
        { return Enumeration.GetValue(scope).Images[command]; }

        /// <summary>
        /// Try/Get the Image for the Scope and Command.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="command"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ScopeType scope, CommandType command, [NotNullWhen(true)] out Image? result)
        {
            if (Enumeration.TryGetValue(scope, out Enumeration? value)
                && value.Images.TryGetValue(command, out Image? image))
            { result = image; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Sets the Image List for a TreeView.
        /// </summary>
        /// <param name="target"></param>
        public static void SetImageList(this TreeView target)
        {
            ImageList result = new ImageList();

            foreach (ScopeType item in Enum.GetValues<ScopeType>())
            {
                if (Enumeration.TryGetValue(item, out Enumeration? value)
                    && value.Images.TryGetValue(CommandType.Default, out Image? image))
                { result.Images.Add(value.Name, image); }
            }

            target.ImageList = result;
        }

        /// <summary>
        /// Sets the Image List for a ListView.
        /// </summary>
        /// <param name="target"></param>
        public static void SetImageList(this ListView target)
        {
            ImageList result = new ImageList();

            foreach (ScopeType item in Enum.GetValues<ScopeType>())
            {
                if (Enumeration.TryGetValue(item, out Enumeration? value)
                    && value.Images.TryGetValue(CommandType.Default, out Image? image))
                { result.Images.Add(value.Name, image); }
            }

            target.SmallImageList = result; 
        }
    }
}
