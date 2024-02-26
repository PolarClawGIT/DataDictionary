using DataDictionary.DataLayer;
using DataDictionary.DataLayer.ApplicationData.Property;
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

namespace DataDictionary.Main.Forms.ApplicationWide
{
    partial class Property : ApplicationBase
    {
        public PropertyKey DataKey { get; private set; }

        public Property() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Property;
            DataKey = new PropertyKey(new PropertyItem());

            newItemCommand.Enabled = true;
            newItemCommand.Click += NewItemCommand_Click;
            newItemCommand.Image = Resources.NewProperty;

            copyToolStripButton.Enabled = true;
            copyToolStripButton.Click += CopyToolStripButton_Click;
            pasteToolStripButton.Enabled = true;
            pasteToolStripButton.Click += PasteToolStripButton_Click;
        }

        private void ApplicationProperty_Load(object sender, EventArgs e)
        {
            BindData();
            this.ValidateChildren();
        }


        private void ApplicationProperty_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void NewItemCommand_Click(object? sender, EventArgs e)
        { NewCommand(); }

        private void PasteToolStripButton_Click(object? sender, EventArgs e)
        { PasteCommand(); }

        private void CopyToolStripButton_Click(object? sender, EventArgs e)
        { CopyCommand(); }

        private void CutToolStripButton_Click(object? sender, EventArgs e)
        { CutCommand(); }

        void NewCommand()
        {
            PropertyItem? newItem = bindingSource.AddNew() as PropertyItem;
            if (newItem is not null)
            {
                DataKey = new PropertyKey(newItem);
                bindingSource.Position = BusinessData.ApplicationData.Properties.IndexOf(newItem);
            }

            propertyTitleData.Focus();
        }

        void CopyCommand()
        {
            DataObject data = new DataObject();

            // Overlays the existing Data, do first.
            if (propertyNavigation.SelectedCells.Count > 0)
            { data = propertyNavigation.GetClipboardContent(); }

            // Add the specific data object
            if (bindingSource.Current is PropertyItem item)
            { data.SetData(nameof(PropertyItem), item); }

            Clipboard.SetDataObject(data, true);
        }

        void CutCommand() { } // Have not deiced what to do here.

        private PropertyItem? pasteItem;
        void PasteCommand()
        {
            if (Clipboard.GetDataObject() is DataObject source
                && source.GetData(nameof(PropertyItem)) is PropertyItem item
                && ActiveControl == propertyNavigation)
            {
                pasteItem = item;
                bindingSource.AddNew();
            }
            else { pasteItem = null; }
        }

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            PropertyItem newItem = new PropertyItem();

            if (pasteItem is not null)
            {
                newItem.PropertyTitle = pasteItem.PropertyTitle;
                newItem.PropertyDescription = pasteItem.PropertyDescription;
                newItem.ExtendedProperty = pasteItem.ExtendedProperty;
                newItem.IsExtendedProperty = pasteItem.IsExtendedProperty;
                newItem.IsFrameworkSummary = pasteItem.IsFrameworkSummary;
                newItem.IsDefinition = pasteItem.IsDefinition;
                newItem.IsChoice = pasteItem.IsChoice;
                newItem.Choices.AddRange(pasteItem.Choices);
            }
            else if (bindingSource.Current is null || propertyNavigation.CurrentRow is null)
            { // First Row scenario
                newItem.PropertyTitle = propertyTitleData.Text;
                newItem.PropertyDescription = propertyDescriptionData.Text;
                newItem.ExtendedProperty = extendedPropertyData.Text;
                newItem.IsExtendedProperty = isExtendedPropertyData.Checked;
                newItem.IsFrameworkSummary = isFrameworkSummaryData.Checked;
                newItem.IsDefinition = isDefinitionData.Checked;
                newItem.IsChoice = isChoiceData.Checked;
                newItem.Choices.AddRange(choiceData.Rows.Cast<DataGridViewRow>().Select(s => (PropertyItem.ChoiceItem)s.DataBoundItem));
            }
            else
            {
                newItem.PropertyTitle = String.Empty;
                newItem.PropertyDescription = String.Empty;
                newItem.ExtendedProperty = String.Empty;
                newItem.IsExtendedProperty = false;
                newItem.IsFrameworkSummary = false;
                newItem.IsDefinition = false;
                newItem.IsChoice = false;
                newItem.Choices.Clear();
            }

            DataKey = new PropertyKey(newItem);
            e.NewObject = newItem;
        }

        private void PropertyTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(propertyTitleData.Text))
            { errorProvider.SetError(propertyTitleData.ErrorControl, "PropertyTitle required"); }
            else { errorProvider.SetError(propertyTitleData.ErrorControl, String.Empty); }
        }

        private void PropertyTitleData_Validated(object sender, EventArgs e)
        {
            if (bindingSource.Current is null || propertyNavigation.CurrentRow is null)
            { bindingSource.AddNew(); } // First Row scenario, force a first row to be created.

            propertyNavigation.ValidateRows<PropertyItem>();
            propertyNavigation.Refresh();
        }

        private void PropertyNavigation_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            propertyNavigation.ValidateRows<PropertyItem>();
            propertyNavigation.Refresh();
        }

        private void PropertyNavigation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is not null && e.RowIndex < propertyNavigation.Rows.Count)
            { propertyNavigation.Rows[e.RowIndex].ErrorText = e.Exception.Message; }
            else { propertyNavigation.Rows[e.RowIndex].ErrorText = String.Empty; }
        }

        private void PropertyNavigation_Leave(object sender, EventArgs e)
        { // Want to keep the "new row" so that it can be edited in more detail.
            if (propertyNavigation.CurrentRow is not null && propertyNavigation.CurrentRow.IsNewRow)
            {
                propertyNavigation.NotifyCurrentCellDirty(true); // Marks the row as dirty so when end-edit the row is retained.
                propertyNavigation.EndEdit(); // Completes the edit so a binding error does not occur when focus changes.
            }
        }

        private void BindingSource_CurrentChanged(object? sender, EventArgs e)
        { // Catches when the PropertyNavigation changes rows. This works better then the DataGridView events.
            if (bindingSource.Current is PropertyItem item)
            {
                DataKey = new PropertyKey(item);
                choiceData.AutoGenerateColumns = false;
                choiceData.DataSource = item.Choices;
            }

            if (propertyNavigation.Rows.Cast<DataGridViewRow>().FirstOrDefault(w => w.GetDataBoundItem() is PropertyItem item && DataKey.Equals(item)) is DataGridViewRow row)
            { if (!row.Selected) { propertyNavigation.ClearSelection(); row.Selected = true; } }
        }

        private void BindingComplete(object sender, BindingCompleteEventArgs e)
        { if (sender is BindingSource binding) { binding.BindComplete(sender, e); } }

        private void IsExtendedPropertyData_CheckedChanged(object sender, EventArgs e)
        { extendedPropertyData.Enabled = isExtendedPropertyData.Checked; }

        private void IsChoiceData_CheckedChanged(object sender, EventArgs e)
        {
            choiceData.Enabled = isChoiceData.Checked;
            choiceData.ReadOnly = isChoiceData.Checked;
            choiceData.AllowUserToAddRows = isChoiceData.Checked;
            choicesHeader.Enabled = isChoiceData.Checked;
        }

        private void ChoiceData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is DataGridView control && e.CellStyle is not null)
            {
                if (control.Enabled)
                { e.CellStyle = new DataGridViewCellStyle(control.DefaultCellStyle); }
                else
                { e.CellStyle = new DataGridViewCellStyle(control.DefaultCellStyle) { BackColor = SystemColors.ControlLight, ForeColor = SystemColors.GrayText }; }
            }
        }

        void BindData()
        {
            bindingSource.CurrentChanged -= BindingSource_CurrentChanged;
            if (bindingSource.DataSource is null)
            { bindingSource.DataSource = BusinessData.ApplicationData.Properties; }

            bindingSource.ResetBindings(false);
            if (BusinessData.ApplicationData.Properties.FirstOrDefault(w => DataKey.Equals(w)) is PropertyItem current)
            { bindingSource.Position = bindingSource.IndexOf(current); }
            bindingSource.CurrentChanged += BindingSource_CurrentChanged;

            propertyNavigation.AutoGenerateColumns = false;
            propertyNavigation.DataSource = bindingSource;

            PropertyItem propertyNameOf = new PropertyItem(); // Used for nameof function
            propertyTitleData.DataBindings.Add(new Binding(nameof(propertyTitleData.Text), bindingSource, nameof(propertyNameOf.PropertyTitle), true));
            propertyDescriptionData.DataBindings.Add(new Binding(nameof(propertyDescriptionData.Text), bindingSource, nameof(propertyNameOf.PropertyDescription), true));
            extendedPropertyData.DataBindings.Add(new Binding(nameof(extendedPropertyData.Text), bindingSource, nameof(propertyNameOf.ExtendedProperty), true));

            isExtendedPropertyData.DataBindings.Add(new Binding(nameof(isExtendedPropertyData.Checked), bindingSource, nameof(propertyNameOf.IsExtendedProperty), true));
            isFrameworkSummaryData.DataBindings.Add(new Binding(nameof(isExtendedPropertyData.Checked), bindingSource, nameof(propertyNameOf.IsFrameworkSummary), true));
            isDefinitionData.DataBindings.Add(new Binding(nameof(isDefinitionData.Checked), bindingSource, nameof(propertyNameOf.IsDefinition), true));
            isChoiceData.DataBindings.Add(new Binding(nameof(isChoiceData.Checked), bindingSource, nameof(propertyNameOf.IsChoice), true));

            extendedPropertyData.Enabled = isExtendedPropertyData.Checked;
            choiceData.Enabled = isChoiceData.Checked;
            choiceData.AllowUserToAddRows = isChoiceData.Checked;
            choicesHeader.Enabled = isChoiceData.Checked;
        }

        void UnBindData()
        {
            if (bindingSource.Current is PropertyItem item)
            { DataKey = new PropertyKey(item); }

            propertyNavigation.DataSource = null;
            propertyTitleData.DataBindings.Clear();
            propertyDescriptionData.DataBindings.Clear();
            extendedPropertyData.DataBindings.Clear();
            isExtendedPropertyData.DataBindings.Clear();
            isFrameworkSummaryData.DataBindings.Clear();
            isDefinitionData.DataBindings.Clear();
            isChoiceData.DataBindings.Clear();
            choiceData.DataSource = null;
            bindingSource.DataSource = null;
        }

        #region IColleague
        protected override void HandleMessage(DbApplicationBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
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
