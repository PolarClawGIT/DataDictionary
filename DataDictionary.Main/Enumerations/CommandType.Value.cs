using DataDictionary.Main.Properties;

namespace DataDictionary.Main.Enumerations
{
    partial class CommandImage
    {
        static readonly Dictionary<CommandType, Image> commandOverlay
            = new Dictionary<CommandType, Image>()
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
    }
}
