using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    partial class ScopeIcons
    {
        /// <summary>
        /// Gets the Icon for the Scope. Used to set the Window Icon.
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static Icon GetIcon(this ScopeType scope)
        {
            if (scope.TryGetIcon(out Icon? result))
            { return result; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(scope), scope);
                throw ex;
            }
        }

        /// <summary>
        /// Try/Get the Icon for the Scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetIcon(this ScopeType scope, [NotNullWhen(true)] out Icon? value)
        {
            if (scopeIconMap.ContainsKey(scope))
            { value = scopeIconMap[scope]; return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Try/Get the Icon converted to a Image (16x16) for the Scope.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean TryGetImage(this ScopeType scope, [NotNullWhen(true)] out Image? value)
        {
            if (scope.TryGetIcon(out Icon? result))
            { value = result.GetSmallImage(); return true; }
            else { value = null; return false; }
        }

        /// <summary>
        /// Adds a list of Images to an Image List for the Scopes listed.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="scopes"></param>
        public static void AddImages(this ImageList target, params IEnumerable<ScopeType> scopes)
        {
            foreach (var item in scopes)
            {
                if (!target.Images.ContainsKey(item.GetName())
                    && item.TryGetImage(out Image? value))
                { target.Images.Add(item.GetName(), value); }
            }
        }

        /// <summary>
        /// Adds a list of Iamges to and ImageList for all the Scopes.
        /// </summary>
        /// <param name="target"></param>
        public static void AddImages(this ImageList target)
        { target.AddImages(Enum.GetValues<ScopeType>().ToList()); }
    }
}
