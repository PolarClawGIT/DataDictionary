using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    partial class CommandImage
    {
        static readonly Dictionary<CommandType, Image> commandOverlay
            = new Dictionary<CommandType, Image>()
            {
                {CommandType.Browse,           Resources.ItemBrowse},
                {CommandType.Add,              Resources.ItemNew},
                {CommandType.Open,             Resources.ItemOpen},
                {CommandType.Save,             Resources.ItemSave},
                {CommandType.Delete,           Resources.ItemDelete},
                {CommandType.Export,           Resources.ItemExport},
                {CommandType.Import,           Resources.ItemImport},
                {CommandType.Refresh,          Resources.ItemRefresh},
                {CommandType.Select,           Resources.ItemSelect},
                {CommandType.Sync,             Resources.ItemSync},
                {CommandType.OpenDatabase,     Resources.ItemOpen},
                {CommandType.SaveDatabase,     Resources.ItemSave},
                {CommandType.DeleteDatabase,   Resources.ItemDelete},
                {CommandType.HistoryDatabase,  Resources.ItemHistory},
                {CommandType.SecurityDatabase, Resources.ItemSecurity},


            };

        static readonly Dictionary<ScopeType, Icon> commmandOverride
            = new Dictionary<ScopeType, Icon>() 
            {
                {ScopeType.ApplicationHelp, Resources.Icon_HelpOffset },
                {ScopeType.Database,        Resources.Icon_Table },
            };
    }
}
