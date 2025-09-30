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
        IReadOnlyDictionary<CommandType, Func<Image>> Images { get; }

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

            // List of default overlay Images
            static readonly Dictionary<CommandType, Image> overlayImages = new Dictionary<CommandType, Image>()
            {
                {CommandType.Browse, Resources.ItemBrowse},
                {CommandType.Add,    Resources.ItemNew},
                {CommandType.Open,   Resources.ItemOpen},
                {CommandType.Save,   Resources.ItemSave},
                {CommandType.Delete, Resources.ItemDelete},
                {CommandType.Export, Resources.ItemExport},
                {CommandType.Import, Resources.ItemImport},
                {CommandType.Select, Resources.ItemSelect},
                {CommandType.Sync,   Resources.ItemSync},
            };

            /// <inheritdoc/>
            public IReadOnlyDictionary<CommandType, Func<Image>> Images
            { get { return images; } }
            Dictionary<CommandType, Func<Image>> images { get; init; } = new Dictionary<CommandType, Func<Image>>();

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

                images.Add(CommandType.Default, windowIcon.GetSmallImage);

                foreach (var item in overlayImages)
                { images.Add(item.Key, () => windowIcon.MergeImage(item.Value)); }
            }

            /// <summary>
            /// Constructor for the Window Form Scope Enumeration.
            /// </summary>
            /// <param name="scope"></param>
            /// <param name="defaultImage"></param>
            Enumeration(ScopeType scope, Image defaultImage) : this(scope)
            {
                images.Add(CommandType.Default, () => defaultImage);

                foreach (var item in overlayImages)
                { images.Add(item.Key, () => defaultImage.MergeImage(item.Value)); }
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
                    if (images.ContainsKey(item.scope))
                    { images[item.scope] = () => item.image; } // Update the image
                    else
                    { images.Add(item.scope, () => item.image); } // Add the image
                }
            }
        }
    }
}
