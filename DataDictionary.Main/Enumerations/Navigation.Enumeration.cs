using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    interface INavigationEnumeration : IScopeEnumeration
    {
        /// <summary>
        /// Icon used for the ScopeType
        /// </summary>
        Icon WindowIcon { get; }

        /// <summary>
        /// List of Images for the Scope Type assocated with Commands.
        /// </summary>
        IReadOnlyDictionary<CommandType, Image> Images { get; }

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
            public Icon WindowIcon { get; init; } = Resources.Icon_UnknownMember;
            static readonly Icon defaultIcon = Resources.Icon_UnknownMember;

            /// <inheritdoc/>
            public IReadOnlyDictionary<CommandType, Image> Images
            { get { return images; } }
            Dictionary<CommandType, Image> images { get; init; } = new Dictionary<CommandType, Image>();
            static readonly Image defaultImage = Resources.UnknownMember;

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

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="windowIcon"></param>
            Enumeration(ScopeType scope, Icon windowIcon) : this(scope)
            { this.WindowIcon = windowIcon; }

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="defaultImage"></param>
            Enumeration(ScopeType scope, Image defaultImage) : this(scope)
            { this.images.Add(CommandType.Default, defaultImage); }

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="windowIcon"></param>
            /// <param name="defaultImage"></param>
            Enumeration(ScopeType scope, Icon windowIcon, Image defaultImage) : this(scope, windowIcon)
            { this.images.Add(CommandType.Default, defaultImage); }

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="windowIcon"></param>
            /// <param name="images"></param>
            Enumeration(ScopeType scope, Icon windowIcon, params (CommandType scope, Image image)[] images) : this(scope, windowIcon)
            {
                foreach ((CommandType scope, Image image) item in images)
                { this.images.Add(item.scope, item.image); }
            }
        }
    }
}
