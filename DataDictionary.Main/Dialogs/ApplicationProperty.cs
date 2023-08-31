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

namespace DataDictionary.Main.Dialogs
{
    partial class ApplicationProperty : ApplicationBase
    {
        PropertyKey propertyKey;
        PropertyItem property;

        public ApplicationProperty() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainProperty;

            if (Program.Data.Properties.FirstOrDefault() is PropertyItem item)
            { property = item; }
            else
            {
                property = new PropertyItem() { PropertyTitle = "new Property", Obsolete = false };
                Program.Data.Properties.Add(property);
            }
            propertyKey = new PropertyKey(property);

            newToolStripButton.Enabled = true;
            newToolStripButton.Click += NewToolStripButton_Click;
        }

        private void NewToolStripButton_Click(object? sender, EventArgs e)
        {
            property = new PropertyItem() { PropertyTitle = "new Property", Obsolete = false };
            Program.Data.Properties.Add(property);
            propertyKey = new PropertyKey(property);

            if (applicationPropertyNavigation.FirstOrDefault<PropertyItem>(propertyKey.Equals).Row is DataGridViewRow row)
            { row.Selected = true; }
        }

        private void ApplicationProperty_Load(object sender, EventArgs e)
        { BindNavigation(); SetNavigation(); }

        private void applicationPropertyNavigation_SelectionChanged(object sender, EventArgs e)
        {
            if (isBinding) { return; }

            if (applicationPropertyNavigation.SelectedRows.Count > 0 && applicationPropertyNavigation.SelectedRows[0].DataBoundItem is PropertyItem item)
            {
                UnBindData();
                property = item;
                propertyKey = new PropertyKey(property);
                BindData();
            }
        }

        Boolean isBinding = false; // Used to prevent the entering the Binding Logic when already binding
        void BindData()
        {
            if (isBinding) { return; }
            isBinding = true;

            propertyTitleData.DataBindings.Add(new Binding(nameof(propertyTitleData.Text), property, nameof(property.PropertyTitle)));
            propertyDescriptionData.DataBindings.Add(new Binding(nameof(propertyDescriptionData.Text), property, nameof(property.PropertyDescription)));
            propertyNameData.DataBindings.Add(new Binding(nameof(propertyNameData.Text), property, nameof(property.PropertyName)));
            obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), property, nameof(property.Obsolete), true, DataSourceUpdateMode.OnValidation, false));

            isBinding = false;
        }

        void BindNavigation()
        {
            if (isBinding) { return; }
            isBinding = true;

            applicationPropertyNavigation.AutoGenerateColumns = false;
            applicationPropertyNavigation.DataSource = Program.Data.Properties; // May cause the Selection Changed event to execute, but not always.

            isBinding = false;
        }

        void UnBindData()
        {
            if (isBinding) { return; }

            propertyTitleData.DataBindings.Clear();
            propertyDescriptionData.DataBindings.Clear();
            propertyNameData.DataBindings.Clear();
            obsoleteData.DataBindings.Clear();
        }

        void UnBindNavigation()
        {
            if (isBinding) { return; }
            applicationPropertyNavigation.DataSource = null;
        }

        void SetNavigation()
        {
            if (applicationPropertyNavigation.FirstOrDefault<PropertyItem>(propertyKey.Equals) is (DataGridViewRow, PropertyItem) value)
            { // Found the current Key, use that row.
                UnBindData();
                value.Row.Selected = true; // May cause the Selection Changed event to execute, but not always.
                property = value.Data;
                propertyKey = new PropertyKey(property);
                BindData();
            }
            else if (applicationPropertyNavigation.FirstOrDefault<PropertyItem>() is (DataGridViewRow, PropertyItem) firstValue)
            { // Did not find the current key, use the first row.
                UnBindData();
                firstValue.Row.Selected = true; // May cause the Selection Changed event to execute, but not always.
                property = firstValue.Data;
                propertyKey = new PropertyKey(property);
                BindData();
            }
        }

        #region IColleague

        protected override void HandleMessage(DbApplicationBatchStarting message)
        { UnBindNavigation(); }

        protected override void HandleMessage(DbApplicationBatchCompleted message)
        { BindNavigation(); SetNavigation(); }

        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindNavigation(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindNavigation(); SetNavigation(); }
        #endregion
    }
}
