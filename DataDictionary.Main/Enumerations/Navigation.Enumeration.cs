using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Drawing.Drawing2D;

namespace DataDictionary.Main.Enumerations
{
    interface INavigationEnumeration : IScopeEnumeration
    {
        /// <summary>
        /// Grouping behavior.
        /// When True, items with the same ScopeType will be group together.
        /// When False, items will not be group together and appear as individual entries.
        /// This effects navigation components.
        /// </summary>
        Boolean GroupBy { get; }
    }

    static partial class NavigationExtention
    {
        /// <summary>
        /// ScopeEnumeration with Images and Icons.
        /// Used to hold Navigation Icons and Images.
        /// </summary>
        partial class Enumeration : Enumeration<ScopeType, Enumeration>,
            INavigationEnumeration
        {
            // This class could not be placed in base ScopeEnumeration because framework agnostic.
            // This version is Windows WinForms specific.
            // The System.Drawing.Icon and System.Drawing.Image does not exist in all frameworks.

            /// <inheritdoc/>
            public ScopeType? Parent { get; init; } = null;

            /// <inheritdoc/>
            public Boolean GroupBy { get; init; } = true;

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            Enumeration(ScopeType scope) : base()
            {
                IScopeEnumeration source = scope.GetEnumeration();
                DisplayName = source.DisplayName;
                Name = source.Name;
                Value = source.Value;
                Parent = source.Parent;
            }
        }
    }
}
