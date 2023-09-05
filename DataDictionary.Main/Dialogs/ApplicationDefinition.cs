using DataDictionary.BusinessLayer.Validation;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Dialogs
{
    partial class ApplicationDefinition : ApplicationBase
    {
        DefinitionKey definitionKey;

        public ApplicationDefinition() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainDefinition;

            bindingSource.DataSource = Program.Data.Definitions;
            if (bindingSource.Current is DefinitionItem item)
            { definitionKey = new DefinitionKey(item); }
            else { definitionKey = new DefinitionKey(new DefinitionItem()); }

            newToolStripButton.Enabled = true;
            newToolStripButton.Click += NewToolStripButton_Click;
            //cutToolStripButton.Enabled = true;
            //cutToolStripButton.Click += CutToolStripButton_Click;
            copyToolStripButton.Enabled = true;
            copyToolStripButton.Click += CopyToolStripButton_Click;
            pasteToolStripButton.Enabled = true;
            pasteToolStripButton.Click += PasteToolStripButton_Click;
        }

        private void ApplicationDefinition_Load(object sender, EventArgs e)
        {
            BindData();

            this.ValidateChildren();
        }


        private void ApplicationDefinition_FormClosed(object sender, FormClosedEventArgs e)
        {
            newToolStripButton.Click -= NewToolStripButton_Click;
            cutToolStripButton.Click -= CutToolStripButton_Click;
            copyToolStripButton.Click -= CopyToolStripButton_Click;
            pasteToolStripButton.Click -= PasteToolStripButton_Click;

        }

        private void NewToolStripButton_Click(object? sender, EventArgs e)
        { NewCommand(); }

        private void PasteToolStripButton_Click(object? sender, EventArgs e)
        { PasteCommand(); }

        private void CopyToolStripButton_Click(object? sender, EventArgs e)
        { CopyCommand(); }

        private void CutToolStripButton_Click(object? sender, EventArgs e)
        { CutCommand(); }

        void NewCommand()
        {
            DefinitionItem? newItem = bindingSource.AddNew() as DefinitionItem;
            if (newItem is not null)
            {
                definitionKey = new DefinitionKey(newItem);
                bindingSource.Position = Program.Data.Definitions.IndexOf(newItem);
            }

            definitionTitleData.Focus();
        }

        void CopyCommand()
        {
            DataObject data = new DataObject();

            // Overlays the existing Data, do first.
            if (definitionNavigation.SelectedCells.Count > 0)
            { data = definitionNavigation.GetClipboardContent(); }

            // Add the specific data object
            if (bindingSource.Current is DefinitionItem item)
            { data.SetData(nameof(DefinitionItem), item); }

            Clipboard.SetDataObject(data, true);
        }

        void CutCommand() { } // Have not deiced what to do here.

        private DefinitionItem? pasteItem;
        void PasteCommand()
        {
            if (Clipboard.GetDataObject() is DataObject source
                && source.GetData(nameof(DefinitionItem)) is DefinitionItem item
                && ActiveControl == definitionNavigation)
            {
                pasteItem = item;
                bindingSource.AddNew();
            }
            else { pasteItem = null; }
        }


        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            DefinitionItem newItem = new DefinitionItem();

            if (pasteItem is not null)
            {
                newItem.DefinitionTitle = pasteItem.DefinitionTitle;
                newItem.DefinitionDescription = pasteItem.DefinitionDescription;
            }

            definitionKey = new DefinitionKey(newItem);
            e.NewObject = newItem;

            //definitionTitleData.Focus();
        }

        private void definitionTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(definitionTitleData.Text))
            { errorProvider.SetError(definitionTitleData.ErrorControl, "DefinitionTitle required"); }
            else { errorProvider.SetError(definitionTitleData.ErrorControl, String.Empty); }
        }

        private void definitionTitleData_Validated(object sender, EventArgs e)
        { ValidateRows(); }

        private void definitionNavigation_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        { ValidateRows(); }

        private void definitionNavigation_RowValidated(object sender, DataGridViewCellEventArgs e)
        { }

        void ValidateRows()
        { //TODO: This does not always cause the error icon to show.
          // The icon does show if the user navigates to a different row.
            foreach (DataGridViewRow row in definitionNavigation.Rows)
            {
                if (row.GetData() is DefinitionItem definitionItem)
                { row.ErrorText = definitionItem.Validate(); }
            }

            definitionNavigation.Refresh();
        }

        private void definitionNavigation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is not null && e.RowIndex < definitionNavigation.Rows.Count)
            { definitionNavigation.Rows[e.RowIndex].ErrorText = e.Exception.Message; }
            else { definitionNavigation.Rows[e.RowIndex].ErrorText = String.Empty; }
        }

        void BindData()
        {
            bindingSource.ResetBindings(false);

            if (Program.Data.Definitions.FirstOrDefault(w => definitionKey is not null && definitionKey.Equals(w)) is DefinitionItem priorItem)
            { bindingSource.Position = Program.Data.Definitions.IndexOf(priorItem); }

            definitionNavigation.AutoGenerateColumns = false;
            definitionNavigation.DataSource = bindingSource;

            if (bindingSource.Current is DefinitionItem item)
            {
                definitionTitleData.DataBindings.Add(new Binding(nameof(definitionTitleData.Text), bindingSource, nameof(item.DefinitionTitle)));
                definitionDescriptionData.DataBindings.Add(new Binding(nameof(definitionDescriptionData.Text), bindingSource, nameof(item.DefinitionDescription)));
                obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), bindingSource, nameof(item.Obsolete), false, DataSourceUpdateMode.OnValidation, false));
            }
        }

        void UnBindData()
        {
            if (bindingSource.Current is DefinitionItem item)
            { definitionKey = new DefinitionKey(item); }

            definitionNavigation.DataSource = null;
            definitionTitleData.DataBindings.Clear();
            definitionDescriptionData.DataBindings.Clear();
            obsoleteData.DataBindings.Clear();
        }

        #region IColleague
        protected override void HandleMessage(DbApplicationBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { BindData(); }

        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }

        protected override void HandleMessage(WindowsCutCommand message)
        {
            base.HandleMessage(message);
            if (!message.IsHandled) { CutCommand(); message.IsHandled = true; }
        }

        protected override void HandleMessage(WindowsCopyCommand message)
        {
            base.HandleMessage(message);
            if (!message.IsHandled) { CopyCommand(); message.IsHandled = true; }
        }

        protected override void HandleMessage(WindowsPasteCommand message)
        {
            base.HandleMessage(message);
            if (!message.IsHandled) { PasteCommand(); message.IsHandled = true; }
        }


        #endregion

    }
}
