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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataDictionary.Main.Forms
{
    partial class ApplicationDefinition : ApplicationBase
    {
        DefinitionKey? definitionKey;
        DefinitionItem? definition;

        public ApplicationDefinition() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainDefinition;
        }

        private void ApplicationDefinition_Load(object sender, EventArgs e)
        {
            definitionNavigation.AutoGenerateColumns = false;
            definitionNavigation.DataSource = Program.Data.Definitions;
        }

        private void definitionNavigation_SelectionChanged(object sender, EventArgs e)
        {

            if (definitionNavigation.SelectedRows.Count > 0 && definitionNavigation.SelectedRows[0].DataBoundItem is DefinitionItem item)
            {
                definitionKey = new DefinitionKey(item);

                UnBindData();
                BindData();
            }
        }

        void BindData()
        {
            if (Program.Data.Definitions.FirstOrDefault(w => definitionKey is DefinitionKey && new DefinitionKey(w) == definitionKey) is DefinitionItem item)
            { definition = item; }

            definitionTitleData.DataBindings.Add(new Binding(nameof(definitionTitleData.Text), definition, nameof(definition.DefinitionTitle)));
            definitionDescriptionData.DataBindings.Add(new Binding(nameof(definitionDescriptionData.Text), definition, nameof(definition.DefinitionDescription)));
            obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), definition, nameof(definition.Obsolete), true, DataSourceUpdateMode.OnValidation, false));
        }

        void UnBindData()
        {
            definitionTitleData.DataBindings.Clear();
            definitionDescriptionData.DataBindings.Clear();
            obsoleteData.DataBindings.Clear();
        }
    }
}
