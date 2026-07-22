using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Properties;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData
    {
        protected IReadOnlyDictionary<ButtonType, ToolStripCommand> Commands { get { return commandItems; } }
        ToolStripCommandCollection commandItems = new ToolStripCommandCollection();

        void CreateCommands_New()
        {
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

        void CreateCommands()
        {
            new CommandState(browseCommand)
            {
                Command = ButtonType.Browse,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(selectCommand)
            {
                Command = ButtonType.Select,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(newCommand)
            {
                Command = ButtonType.Add,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(deleteCommand)
            {
                Command = ButtonType.Delete,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(saveCommand)
            {
                Command = ButtonType.Save,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(openCommand)
            {
                Command = ButtonType.Open,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(importCommand)
            {
                Command = ButtonType.Import,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(exportCommand)
            {
                Command = ButtonType.Export,
                Visible = false,
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            toolStripSeparator.Visible = false;

            new CommandState(openFromDatabaseCommand)
            {
                Scope = ScopeType.Database,
                Command = ButtonType.OpenDatabase,
                Visible = true,
                Enabled = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached),
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(saveToDatabaseCommand)
            {
                Scope = ScopeType.Database,
                Command = ButtonType.SaveDatabase,
                Visible = true,
                Enabled = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Detached),
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(deleteFromDatabaseCommand)
            {
                Scope = ScopeType.Database,
                Command = ButtonType.DeleteDatabase,
                Visible = true,
                Enabled = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached),
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(securityCommand)
            {
                Scope = ScopeType.Security,
                Command = ButtonType.SecurityDatabase,
                Visible = false,
                Enabled = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached),
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);

            new CommandState(historyCommand)
            {
                Scope = ScopeType.ApplicationTimeLine,
                Command = ButtonType.HistoryDatabase,
                Visible = false,
                Enabled = false,
                AllowEnabled = () => Settings.Default.IsOnLineMode && RowState is not (DataRowState.Added or DataRowState.Detached),
                IsAuthorized = GetAuthorization
            }.AddTo(commandButtons);
        }
    }
}
