﻿using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationData : ApplicationBase
    {
        Dictionary<DataRowState, (Image icon, String tooltip)> rowStateSettings = new Dictionary<DataRowState, (Image icon, string tooltip)>()
        {
            { DataRowState.Added, new (Resources.RowAdded, "Added")},
            { DataRowState.Deleted, new (Resources.RowDeleted, "Deleted")},
            { DataRowState.Detached, new (Resources.RowDetached, "Detached")},
            { DataRowState.Modified, new (Resources.RowModified, "Modified")},
            { DataRowState.Unchanged, new (Resources.Row, "Unchanged")},
        };

        protected class CommandState
        {
            readonly ToolStripItem Control;

            /// <summary>
            /// Is the Command Button Enabled
            /// </summary>
            public Boolean IsEnabled
            {
                get { return Control.Enabled; }
                set
                {
                    Control.Enabled = value && AllowEnabled();
                    isEnabled = value;
                }
            }
            Boolean isEnabled = false; // Intended State
            public Func<Boolean> AllowEnabled { get; init; } = () => { return true; };

            public void Refresh()
            { Control.Enabled = isEnabled && AllowEnabled(); }

            /// <summary>
            /// Is the Command Button Visible
            /// </summary>
            public Boolean IsVisible
            {
                get { return Control.Visible; }
                set { Control.Visible = value; }
            }

            /// <summary>
            /// Image for the Command Button
            /// </summary>
            public Image? Image
            {
                get { return Control.Image; }
                set { Control.Image = value; }
            }

            public CommandState(ToolStripItem control)
            { Control = control; }

        }

        /// <summary>
        /// The set of Command Buttons
        /// </summary>
        protected IReadOnlyDictionary<CommandImageType, CommandState> CommandButtons { get { return commandButtons; } }
        Dictionary<CommandImageType, CommandState> commandButtons = new Dictionary<CommandImageType, CommandState>();

        private DataRowState? rowState;
        public DataRowState? RowState
        {
            get { return rowState; }
            set
            {
                if (value is DataRowState state)
                {
                    rowStateCommand.Enabled = true;
                    rowStateCommand.Visible = true;
                    rowStateCommand.Image = rowStateSettings[state].icon;
                    rowStateCommand.ToolTipText = rowStateSettings[state].tooltip;
                }
                else
                {
                    rowStateCommand.Enabled = false;
                    rowStateCommand.Visible = false;
                }

                rowState = value;
            }
        }

        /// <summary>
        /// Constructor called when in Form Design mode
        /// </summary>
        /// <remarks>
        /// WARNING: When a child form is designed, this method executes.
        /// The "DesignMode" property IS NOT SET.
        /// Any reference to instances of Objects defined outside of this code
        /// will throw errors when a child form is designed.
        /// This causes lots of issue with Visual Studio.
        /// </remarks>
        public ApplicationData() : base()
        {
            InitializeComponent();

            commandButtons.Add(CommandImageType.Browse, new CommandState(browseCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.New, new CommandState(newCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Delete, new CommandState(deleteCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Save, new CommandState(saveCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Open, new CommandState(openCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Import, new CommandState(importCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.Export, new CommandState(exportCommand) { IsVisible = false });
            commandButtons.Add(CommandImageType.OpenDatabase, new CommandState(openFromDatabaseCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(CommandImageType.SaveDatabase, new CommandState(saveToDatabaseCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
            commandButtons.Add(CommandImageType.DeleteDatabase, new CommandState(deleteFromDatabaseCommand) { AllowEnabled = () => Settings.Default.IsOnLineMode });
        }

        /// <summary>
        /// Delegate for the Event to handle the RowState of the data.
        /// </summary>
        /// <param name="sender">IBindingRowState</param>
        /// <param name="e"></param>
        /// <remarks>This will lock the form is the data is Detached or Deleted.</remarks>
        protected virtual void RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingRowState data)
            {
                RowState = data.RowState();
                if (IsHandleCreated)
                { this.Invoke(() => { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }); }
                else { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }
            }
        }

        private void toolStrip_VisibleChanged(object? sender, EventArgs e)
        {
            // Visibility can be set in code.
            // More often it changes based on other controls on the Form and any over lapping controls or form not the top most form.
            if (toolStrip.Visible)
            {
                // Assumes that a TableLayout Control or similar is the only other control on the page.
                // Condition and order is an attempt to prevent issues when multiple controls on the same form.
                if (this.Controls.Cast<Control>().
                    OrderBy(o => o.Top).
                    FirstOrDefault(w => w.HasChildren &&
                        w != toolStrip && // Not the ToolStrip
                        w.Top < toolStrip.Height // Top control does not overlap with ToolStrip
                        ) is Control topControl)
                {
                    topControl.Padding = new Padding(
                       topControl.Padding.Left,
                       toolStrip.Height + topControl.Padding.Top,
                       topControl.Padding.Right,
                       topControl.Padding.Bottom);
                }

                // Don't respond to further changes to Visibility (change only on first time visible only).
                toolStrip.VisibleChanged -= toolStrip_VisibleChanged;
            }
        }

        private void ApplicationData_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            { // Avoids issues where the Load event fires in Design Mode
                LoadToolTips(this);
            }
        }

        /// <summary>
        /// Common Setup method.
        /// Sets RowState based on the BindingSource data.
        /// </summary>
        /// <param name="data"></param>
        protected void Setup(BindingSource data)
        {
            if (data.Current is IBindingRowState binding)
            {
                RowState = binding.RowState();
                binding.RowStateChanged += RowStateChanged;
            }
        }

        /// <summary>
        /// Common Setup method.
        /// Sets the Icon and Command Button Images
        /// </summary>
        /// <param name="scope"></param>
        protected void Setup(ScopeType scope)
        {
            Icon = ImageEnumeration.GetIcon(scope);

            foreach (KeyValuePair<CommandImageType, CommandState> item in commandButtons)
            {
                if (ImageEnumeration.Values.ContainsKey(scope) && ImageEnumeration.Values[scope].Images.ContainsKey(item.Key))
                { item.Value.Image = ImageEnumeration.GetImage(scope, item.Key); }
                // Else leave the image as is
            }
        }

        protected virtual void helpToolStripButton_Click(object sender, EventArgs e)
        { Activate(() => new ApplicationWide.HelpSubject(this)); }

        protected virtual void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void BrowseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void NewCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void DeleteCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void ImportCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void ExportCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void OpenCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SaveCommand_Click(object? sender, EventArgs e)
        { }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);
            commandButtons[CommandImageType.OpenDatabase].Refresh();
            commandButtons[CommandImageType.SaveDatabase].Refresh();
            commandButtons[CommandImageType.DeleteDatabase].Refresh();
        }


    }
}
