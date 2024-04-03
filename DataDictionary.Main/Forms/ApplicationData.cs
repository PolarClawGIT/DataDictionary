using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Help;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        /// Sets Title, Icon and RowState based on the BindingSource data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="scope"></param>
        protected void Setup(BindingSource data, ScopeType scope = ScopeType.Null)
        {
            if (data.Current is IScopeKey scopeKey)
            { this.Icon = new ScopeKey(scopeKey).Scope.ToIcon(); }
            else { this.Icon = scope.ToIcon(); }

            if (data.Current is IBindingRowState binding)
            {
                RowState = binding.RowState();
                binding.RowStateChanged += RowStateChanged;
            }
        }

        /// <summary>
        /// Exposes the Enabled/Disabled state of the OpenFromDatabase button.
        /// </summary>
        /// <remarks>Set verifies that the application is in an OnLine State and handles change to OnLine State</remarks>
        protected Boolean IsOpenDatabase
        {
            get { return openFromDatabaseCommand.Enabled; }
            set
            {
                openFromDatabaseCommand.Enabled = value && Settings.Default.IsOnLineMode;
                isOpenDatabase = value;
            }
        }
        Boolean isOpenDatabase = false; // Intended State

        /// <summary>
        /// Exposes the Enabled/Disabled state of the SaveToDatabase button
        /// </summary>
        /// <remarks>Set verifies that the application is in an OnLine State and handles change to OnLine State</remarks>
        protected Boolean IsSaveDatabase
        {
            get { return saveToDatabaseCommand.Enabled; }
            set
            {
                saveToDatabaseCommand.Enabled = value && Settings.Default.IsOnLineMode;
                isSaveDatabase = value;
            }
        }
        Boolean isSaveDatabase = false; // Intended State

        /// <summary>
        /// Exposes the Enabled/Disabled state of the DeleteFromDatabase button
        /// </summary>
        /// <remarks>Set verifies that the application is in an OnLine State and handles change to OnLine State</remarks>
        protected Boolean IsDeleteDatabase
        {
            get { return deleteFromDatabaseCommand.Enabled; }
            set { deleteFromDatabaseCommand.Enabled = value && Settings.Default.IsOnLineMode; isDeleteDatabase = value;  }
        }
        Boolean isDeleteDatabase = false; // Intended State

        protected virtual void helpToolStripButton_Click(object sender, EventArgs e)
        { Activate(() => new ApplicationWide.HelpSubject(this)); }

        protected virtual void OpenFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void SaveToDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected virtual void DeleteFromDatabaseCommand_Click(object? sender, EventArgs e)
        { }

        protected override void HandleMessage(OnlineStatusChanged message)
        {
            base.HandleMessage(message);
            IsOpenDatabase = isOpenDatabase;
            IsSaveDatabase = isSaveDatabase;
            IsDeleteDatabase = isDeleteDatabase;
        }
    }
}
