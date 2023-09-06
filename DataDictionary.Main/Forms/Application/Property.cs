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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Application
{
    partial class Property : ApplicationBase
    {
        PropertyKey propertyKey;

        public Property() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainProperty;

            bindingSource.DataSource = Program.Data.Properties;
            if (bindingSource.Current is PropertyItem item)
            { propertyKey = new PropertyKey(item); }
            else { propertyKey = new PropertyKey(new PropertyItem()); }

            newToolStripButton.Enabled = true;
            newToolStripButton.Click += NewToolStripButton_Click;
            //cutToolStripButton.Enabled = true;
            //cutToolStripButton.Click += CutToolStripButton_Click;
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
            PropertyItem? newItem = bindingSource.AddNew() as PropertyItem;
            if (newItem is not null)
            {
                propertyKey = new PropertyKey(newItem);
                bindingSource.Position = Program.Data.Properties.IndexOf(newItem);
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
                newItem.PropertyName = pasteItem.PropertyName;
            }

            propertyKey = new PropertyKey(newItem);
            e.NewObject = newItem;

            //PropertyTitleData.Focus();
        }

        private void PropertyTitleData_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(propertyTitleData.Text))
            { errorProvider.SetError(propertyTitleData.ErrorControl, "PropertyTitle required"); }
            else { errorProvider.SetError(propertyTitleData.ErrorControl, String.Empty); }
        }

        private void PropertyTitleData_Validated(object sender, EventArgs e)
        { ValidateRows(); }

        private void PropertyNavigation_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        { ValidateRows(); }

        void ValidateRows()
        {
            foreach (DataGridViewRow row in propertyNavigation.Rows)
            {
                if (row.GetData() is PropertyItem item)
                { row.ErrorText = item.Validate(); }
            }

            propertyNavigation.Refresh();
        }

        private void PropertyNavigation_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is not null && e.RowIndex < propertyNavigation.Rows.Count)
            { propertyNavigation.Rows[e.RowIndex].ErrorText = e.Exception.Message; }
            else { propertyNavigation.Rows[e.RowIndex].ErrorText = String.Empty; }
        }

        void BindData()
        {
            bindingSource.ResetBindings(false);

            if (Program.Data.Properties.FirstOrDefault(w => propertyKey is not null && propertyKey.Equals(w)) is PropertyItem priorItem)
            { bindingSource.Position = Program.Data.Properties.IndexOf(priorItem); }

            propertyNavigation.AutoGenerateColumns = false;
            propertyNavigation.DataSource = bindingSource;

            if (bindingSource.Current is PropertyItem item)
            {
                propertyTitleData.DataBindings.Add(new Binding(nameof(propertyTitleData.Text), bindingSource, nameof(item.PropertyTitle)));
                propertyDescriptionData.DataBindings.Add(new Binding(nameof(propertyDescriptionData.Text), bindingSource, nameof(item.PropertyDescription)));
                propertyNameData.DataBindings.Add(new Binding(nameof(propertyNameData.Text), bindingSource, nameof(item.PropertyName)));
                obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), bindingSource, nameof(item.Obsolete), false, DataSourceUpdateMode.OnValidation, false));
            }
        }

        void UnBindData()
        {
            if (bindingSource.Current is PropertyItem item)
            { propertyKey = new PropertyKey(item); }

            propertyNavigation.DataSource = null;
            propertyTitleData.DataBindings.Clear();
            propertyDescriptionData.DataBindings.Clear();
            propertyNameData.DataBindings.Clear();
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
