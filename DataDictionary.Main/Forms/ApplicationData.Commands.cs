using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Data;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        /// <summary>
        /// The set of Command Buttons
        /// </summary>
        protected IReadOnlyDictionary<ButtonType, ToolStripCommand> CommandButtons { get { return commandItems; } }
        ToolStripCommandCollection commandItems = new ToolStripCommandCollection();

        /// <summary>
        /// Init the Command Buttons, linking the dictionary to the UI buttons.
        /// </summary>
        void InitCommands()
        {   //Just makes it easer to read.
            commandItems.AddRange(
                new ToolStripCommand(ButtonType.Browse, browseCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Browse) },
                new ToolStripCommand(ButtonType.Select, selectCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Select) },
                new ToolStripCommand(ButtonType.Add, newCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Add) },
                new ToolStripCommand(ButtonType.Delete, deleteCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Delete) },
                new ToolStripCommand(ButtonType.Save, saveCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Save) },
                new ToolStripCommand(ButtonType.Open, openCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Open) },
                new ToolStripCommand(ButtonType.Import, importCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Import) },
                new ToolStripCommand(ButtonType.Export, exportCommand)
                { Visible = false, AllowEnabled = () => GetAuthorization(ButtonType.Export) },

                new ToolStripCommand(ScopeType.Database, ButtonType.OpenDatabase, openFromDatabaseCommand)
                {
                    Visible = true,
                    Enabled = false,
                    AllowEnabled = () =>
                        GetAuthorization(ButtonType.OpenDatabase)
                        && Settings.Default.IsOnLineMode
                        && RowState is not (DataRowState.Added or DataRowState.Detached)
                },
                new ToolStripCommand(ScopeType.Database, ButtonType.SaveDatabase, saveToDatabaseCommand)
                {
                    Visible = true,
                    Enabled = false,
                    AllowEnabled = () =>
                        GetAuthorization(ButtonType.SaveDatabase)
                        && Settings.Default.IsOnLineMode
                        && RowState is not (DataRowState.Detached)
                },
                new ToolStripCommand(ScopeType.Database, ButtonType.DeleteDatabase, deleteFromDatabaseCommand)
                {
                    Visible = true,
                    Enabled = false,
                    AllowEnabled = () =>
                        GetAuthorization(ButtonType.DeleteDatabase)
                        && Settings.Default.IsOnLineMode
                        && RowState is not (DataRowState.Added or DataRowState.Detached)
                },
                new ToolStripCommand(ScopeType.Security, ButtonType.SecurityDatabase, securityCommand)
                {
                    Visible = false,
                    Enabled = false,
                    AllowEnabled = () =>
                        GetAuthorization(ButtonType.SecurityDatabase)
                        && Settings.Default.IsOnLineMode
                        && RowState is not (DataRowState.Added or DataRowState.Detached)
                },
                new ToolStripCommand(ScopeType.ApplicationTimeLine, ButtonType.HistoryDatabase, historyCommand)
                {
                    Visible = false,
                    Enabled = false,
                    AllowEnabled = () =>
                        GetAuthorization(ButtonType.HistoryDatabase)
                        && Settings.Default.IsOnLineMode
                        && RowState is not (DataRowState.Added or DataRowState.Detached)
                }

            );
        }

    }
}
