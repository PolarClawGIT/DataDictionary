using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    static partial class NavigationExtension
    {
        /// <summary>
        /// ScopeEnumeration with Images and Icons.
        /// Used to hold Navigation Icons and Images.
        /// </summary>
        partial class NavigationValue : Enumeration<ScopeType, NavigationValue>,
            INavigationValue
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
            NavigationValue(ScopeType scope) : base()
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
