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
    partial class ApplicationDefinition : ApplicationBase
    {
        DefinitionKey definitionKey;
        DefinitionItem definition;

        public ApplicationDefinition() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DomainDefinition;

            if (Program.Data.Definitions.FirstOrDefault() is DefinitionItem item)
            { definition = item; }
            else
            {
                definition = new DefinitionItem() { DefinitionTitle = "new Definition", Obsolete = false };
                Program.Data.Definitions.Add(definition);
            }
            definitionKey = new DefinitionKey(definition);

            newToolStripButton.Enabled = true;
            newToolStripButton.Click += NewToolStripButton_Click;
        }

        private void NewToolStripButton_Click(object? sender, EventArgs e)
        {
            definition = new DefinitionItem() { DefinitionTitle = "new Definition", Obsolete = false };
            Program.Data.Definitions.Add(definition);
            definitionKey = new DefinitionKey(definition);

            if (definitionNavigation.FirstOrDefault<DefinitionItem>(definitionKey.Equals).Row is DataGridViewRow row)
            { row.Selected = true; }
        }

        private void ApplicationDefinition_Load(object sender, EventArgs e)
        { BindNavigation(); SetNavigation(); }

        private void definitionNavigation_SelectionChanged(object sender, EventArgs e)
        {
            if (isBinding) { return; }

            if (definitionNavigation.SelectedRows.Count > 0 && definitionNavigation.SelectedRows[0].DataBoundItem is DefinitionItem item)
            {
                UnBindData();
                definition = item;
                definitionKey = new DefinitionKey(definition);
                BindData();
            }
        }

        Boolean isBinding = false; // Used to prevent the entering the Binding Logic when already binding
        void BindData()
        {
            if (isBinding) { return; }
            isBinding = true;

            definitionTitleData.DataBindings.Add(new Binding(nameof(definitionTitleData.Text), definition, nameof(definition.DefinitionTitle)));
            definitionDescriptionData.DataBindings.Add(new Binding(nameof(definitionDescriptionData.Text), definition, nameof(definition.DefinitionDescription)));
            obsoleteData.DataBindings.Add(new Binding(nameof(obsoleteData.Checked), definition, nameof(definition.Obsolete), true, DataSourceUpdateMode.OnValidation, false));

            isBinding = false;
        }

        void BindNavigation()
        {
            if (isBinding) { return; }
            isBinding = true;

            definitionNavigation.AutoGenerateColumns = false;
            definitionNavigation.DataSource = Program.Data.Definitions; // May cause the Selection Changed event to execute, but not always.

            isBinding = false;
        }

        void UnBindData()
        {
            if (isBinding) { return; }

            definitionTitleData.DataBindings.Clear();
            definitionDescriptionData.DataBindings.Clear();
            obsoleteData.DataBindings.Clear();
        }

        void UnBindNavigation()
        {
            if (isBinding) { return; }
            definitionNavigation.DataSource = null;
        }

        void SetNavigation()
        {
            if (definitionNavigation.FirstOrDefault<DefinitionItem>(definitionKey.Equals) is (DataGridViewRow, DefinitionItem) value)
            { // Found the current Key, use that row.
                UnBindData();
                value.Row.Selected = true; // May cause the Selection Changed event to execute, but not always.
                definition = value.Data;
                definitionKey = new DefinitionKey(definition);
                BindData();
            }
            else if (definitionNavigation.FirstOrDefault<DefinitionItem>() is (DataGridViewRow, DefinitionItem) firstValue)
            { // Did not find the current key, use the first row.
                UnBindData();
                firstValue.Row.Selected = true; // May cause the Selection Changed event to execute, but not always.
                definition = firstValue.Data;
                definitionKey = new DefinitionKey(definition);
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
