using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
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


        public SchemaNode(ITemplateIndex template, ISchemaDefinitionIndex schema) : this ()
        {
            templateIndex = new TemplateIndex(template);
            schemaIndex = new SchemaDefinitionIndex(schema);
        }

        public SchemaNode(ISchemaComposite schema) : this(schema, schema) { }

        public SchemaNode(
            ITemplateIndex template,
            ISchemaDefinitionIndex schema,
            Func<ITemplateData> getData) : this(template, schema)
        { formBinding.GetData = getData; }

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
                templateTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                schemaTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.SchemaTitle)));

                nodeNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ISchemaNodeValue.NodeName)));

                RenderValueAsList.Load(nodeRenderAsData);
                nodeRenderAsData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ISchemaNodeValue.RenderValueAs)));
                nodeRenderOrderData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ISchemaNodeValue.NodeOrder)));


                XScopeList.Load(nodeObjectScopeData, nodeObjectPropertyData, formBinding.Builders, "(n/a)");
                nodeObjectScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ISchemaNodeValue.ObjectScope), true, DataSourceUpdateMode.OnValidation, ScopeNameList.NullValue));
                nodeObjectPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ISchemaNodeValue.ObjectProperty)));

                PropertyNameList.Load(nodeModelPropertyData, "(n/a)");
                nodeModelPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ISchemaNodeValue.ModelPropertyId), true, DataSourceUpdateMode.OnValidation, Guid.Empty));

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
