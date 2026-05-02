using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    partial class CommandImage
    {
        static readonly Dictionary<ButtonType, Image> commandOverlay
            = new Dictionary<ButtonType, Image>()
            {
                {ButtonType.Browse,           Resources.ItemBrowse},
                {ButtonType.Add,              Resources.ItemNew},
                {ButtonType.Open,             Resources.ItemOpen},
                {ButtonType.Save,             Resources.ItemSave},
                {ButtonType.Delete,           Resources.ItemDelete},
                {ButtonType.Export,           Resources.ItemExport},
                {ButtonType.Import,           Resources.ItemImport},
                {ButtonType.Refresh,          Resources.ItemRefresh},
                {ButtonType.Select,           Resources.ItemSelect},
                {ButtonType.Sync,             Resources.ItemSync},
                {ButtonType.OpenDatabase,     Resources.ItemOpen},
                {ButtonType.SaveDatabase,     Resources.ItemSave},
                {ButtonType.DeleteDatabase,   Resources.ItemDelete},
                {ButtonType.HistoryDatabase,  Resources.ItemHistory},
                {ButtonType.SecurityDatabase, Resources.ItemSecurity},


            };

        static readonly Dictionary<ScopeType, Icon> commmandOverride
            = new Dictionary<ScopeType, Icon>() 
            {
                {ScopeType.ApplicationHelp, Resources.Icon_HelpOffset },
                {ScopeType.Database,        Resources.Icon_Table },
            };
    }
}
