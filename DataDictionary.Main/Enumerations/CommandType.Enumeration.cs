using DataDictionary.Main.Properties;

namespace DataDictionary.Main.Enumerations
{
    interface ICommandTypeImages
    {
        /// <summary>
        /// List of Images for the Scope Type assocated with Commands.
        /// </summary>
        IReadOnlyDictionary<CommandType, Func<Image>> CommandImages { get; }
    }

    static partial class NavigationExtention
    {
        partial class Enumeration: ICommandTypeImages
        {
            // List of default overlay Images
            static readonly Dictionary<CommandType, Image> commandOverlay = new Dictionary<CommandType, Image>()
            {
                {CommandType.Browse,  Resources.ItemBrowse},
                {CommandType.Add,     Resources.ItemNew},
                {CommandType.Open,    Resources.ItemOpen},
                {CommandType.Save,    Resources.ItemSave},
                {CommandType.Delete,  Resources.ItemDelete},
                {CommandType.Export,  Resources.ItemExport},
                {CommandType.Import,  Resources.ItemImport},
                {CommandType.Refresh, Resources.ItemRefresh},
                {CommandType.Select,  Resources.ItemSelect},
                {CommandType.Sync,    Resources.ItemSync},
            };

            /// <inheritdoc/>
            public IReadOnlyDictionary<CommandType, Func<Image>> CommandImages
            { get { return commandImages; } }
            Dictionary<CommandType, Func<Image>> commandImages { get; init; } = new Dictionary<CommandType, Func<Image>>();

        }
    }
}
