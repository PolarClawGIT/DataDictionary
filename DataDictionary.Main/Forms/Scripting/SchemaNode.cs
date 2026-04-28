using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaNode : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex();
        //TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ISchemaDefinitionIndex key && schemaIndex.Equals(key); }

        public SchemaNode()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                SchemaBinding = bindingSchema,
                NodeBinding = bindingNode,
                NodeOwnerBinding = bindingNodeOwner,
                DoWork = base.DoWork,
            };

            SetRowState(
                bindingNode,
                bindingNodeOwner);
            SetTitle(bindingNode);
            SetIcon(bindingNode, ScopeType.ScriptingNode);

            SetCommand(ScopeType.ScriptingNode,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);
        }

        public SchemaNode(ISchemaDefinitionIndex schema) : this()
        { schemaIndex = new SchemaDefinitionIndex(schema); }

        public SchemaNode(
                ITemplateIndex template,
                ISchemaDefinitionIndex? schema,
                Func<TemplateIndex, BindingView<TemplateValue>> getTemplates,
                Func<SchemaDefinitionIndex, BindingView<SchemaDefinitionValue>> getSchemata,
                Func<SchemaDefinitionIndex, BindingView<SchemaNodeValue>> getNodes,
                Func<SchemaDefinitionIndex, BindingView<SchemaNodeOwnerValue>> getOwners)
        : this()
        {
            templateIndex = new TemplateIndex(template);
            if (schema is ISchemaDefinitionIndex key)
            { schemaIndex = new SchemaDefinitionIndex(key); }

            formBinding.GetTemplates = getTemplates;
            formBinding.GetSchemata = getSchemata;
            formBinding.GetNodes = getNodes;
            formBinding.GetOwners = getOwners;
        }

        private void SchemaNode_Load(object sender, EventArgs e)
        {
            if (schemaIndex.HasValue)
            { formBinding.Load(schemaIndex); }
            else
            {   // This should never occur.
                Exception ex = new InvalidOperationException("Template Schema not found");
                ex.Data.Add(nameof(templateIndex), templateIndex);
                ex.Data.Add(nameof(schemaIndex), schemaIndex);
                throw ex;
            }

            if (formBinding.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }

        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);
        }
    }
}
