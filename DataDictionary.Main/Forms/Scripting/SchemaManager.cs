using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Messages;
using Microsoft.VisualBasic;
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

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaManager : ApplicationData
    {
        public SchemaManager() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(schemaToolStrip, 0);
        }

        public SchemaManager(ISchemaItem? schemaItem) : this()
        {
            if (schemaItem is null)
            {
                schemaItem = new SchemaItem();
                BusinessData.ScriptingEngine.Schemta.Add(schemaItem);
                BusinessData.NameScope.Add(new NamedScopeItem(BusinessData.Model, schemaItem));
                SendMessage(new RefreshNavigation());
            }

            DataLayer.ScriptingData.Schema.SchemaKey key = new DataLayer.ScriptingData.Schema.SchemaKey(schemaItem);

            bindingSchema.DataSource = new BindingView<SchemaItem>(BusinessData.ScriptingEngine.Schemta, w => key.Equals(w));
            bindingSchema.Position = 0;

            bindingElement.DataSource = new BindingView<ElementItem>(BusinessData.ScriptingEngine.Elements, w => key.Equals(w));
        }


        private void SchemaManager_Load(object sender, EventArgs e)
        {
            ISchemaItem nameOf;
            schemaTitleData.DataBindings.Add(new Binding(nameof(schemaTitleData.Text), bindingSchema, nameof(nameOf.SchemaTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            schemaDescriptionData.DataBindings.Add(new Binding(nameof(schemaDescriptionData.Text), bindingSchema, nameof(nameOf.SchemaDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            elementSelection.Groups.Clear();
            elementSelection.Items.Clear();

            foreach (IGrouping<ScopeType, ColumnItem> groups in BusinessData.ScriptingEngine.Columns.GroupBy(g => new ScopeKey(g).Scope))
            {
                ListViewGroup group = new ListViewGroup(groups.Key.ToScopeName());
                elementSelection.Groups.Add(group);

                foreach (ColumnItem column in groups)
                {

                }
            }

        }

        private void addSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void removeSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void openSchemaElements_Click(object sender, EventArgs e)
        {

        }

    }
}
