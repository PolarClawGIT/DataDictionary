using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Drawing.Drawing2D;

namespace DataDictionary.Main.Enumerations
{
    interface INavigationEnumeration : IScopeEnumeration
    {
        /// <summary>
        /// Icon used for the ScopeType
        /// </summary>
        Icon WindowIcon { get; }

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
            {
                this.WindowIcon = windowIcon;

                commandImages.Add(CommandType.Default, windowIcon.GetSmallImage);

                foreach (var item in commandOverlay)
                { commandImages.Add(item.Key, () => windowIcon.MergeImage(item.Value)); }

                foreach (var item in statusOverlay)
                { statusImages.Add(item.Key, () => windowIcon.MergeImage(item.Value, ContentAlignment.BottomRight, CompositingMode.SourceOver)); }
            }

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="windowIcon"></param>
            /// <param name="imageList"></param>
            Enumeration(ScopeType scope, Icon windowIcon, params (CommandType scope, Image image)[] imageList) : this(scope, windowIcon)
            {
                foreach ((CommandType scope, Image image) item in imageList)
                {
                    if (commandImages.ContainsKey(item.scope))
                    { commandImages[item.scope] = () => item.image; } // Update the image
                    else
                    { commandImages.Add(item.scope, () => item.image); } // Add the image
                }
            }
        }
    }
}
