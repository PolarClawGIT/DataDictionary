using DataDictionary.DataLayer.ApplicationData;
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
        PropertyKey? propertyKey;
        PropertyItem? property;

        public ApplicationProperty() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainProperty;
            newToolStripButton.Enabled = true;
            newToolStripButton.Click += NewToolStripButton_Click;
        }

        private void NewToolStripButton_Click(object? sender, EventArgs e)
        {
            PropertyItem newproperty = new PropertyItem() { PropertyTitle = "new Property", Obsolete = false };
            PropertyKey newKey = new PropertyKey(newproperty);
            Program.Data.Properties.Add(newproperty);

            if (applicationPropertyNavigation.FindRow<PropertyItem, PropertyKey>(newKey, (item) => new PropertyKey(item)).row is DataGridViewRow row)
            { row.Selected = true; }
        }

        private void ApplicationProperty_Load(object sender, EventArgs e)
        {
            applicationPropertyNavigation.AutoGenerateColumns = false;
            applicationPropertyNavigation.DataSource = Program.Data.Properties;
        }

        private void applicationPropertyNavigation_SelectionChanged(object sender, EventArgs e)
        {
            if (applicationPropertyNavigation.SelectedRows.Count > 0 && applicationPropertyNavigation.SelectedRows[0].DataBoundItem is PropertyItem item)
            {
                UnBindData();
                propertyKey = new PropertyKey(item);
                BindData();
            }
        }

        void BindData()
        {
            if (Program.Data.Properties.FirstOrDefault(w => propertyKey is PropertyKey && new PropertyKey(w) == propertyKey) is PropertyItem item)
            { property = item; }

            propertyTitleData.DataBindings.Add(new Binding(nameof(propertyTitleData.Text), property, nameof(property.PropertyTitle)));
            propertyDescriptionData.DataBindings.Add(new Binding(nameof(propertyDescriptionData.Text), property, nameof(property.PropertyDescription)));
            propertyNameData.DataBindings.Add(new Binding(nameof(propertyNameData.Text), property, nameof(property.PropertyName)));
            obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), property, nameof(property.Obsolete), true, DataSourceUpdateMode.OnValidation, false));
        }

        void UnBindData()
        {
            propertyTitleData.DataBindings.Clear();
            propertyDescriptionData.DataBindings.Clear();
            propertyNameData.DataBindings.Clear();
            obsoleteData.DataBindings.Clear();
        }
    }
}
