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

namespace DataDictionary.Main.Forms
{

    partial class ApplicationProperty : ApplicationFormBase
    {
        PropertyKey? propertyKey;
        PropertyItem? property;

        public ApplicationProperty() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainProperty;
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
                propertyKey = new PropertyKey(item);

                UnBindData();
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

        }

        void UnBindData()
        {
            propertyTitleData.DataBindings.Clear();
            propertyDescriptionData.DataBindings.Clear();
            propertyNameData.DataBindings.Clear();
        }
    }
}
