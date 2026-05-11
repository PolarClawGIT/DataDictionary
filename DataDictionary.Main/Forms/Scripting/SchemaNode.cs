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

            formBinding = new FormBinding(
                templateBinding: bindingTemplate,
                schemaBinding: bindingSchema,
                nodeBinding: bindingNode,
                ownerBinding: bindingNodeOwner);

            SetRowState(
                bindingNode,
                bindingNodeOwner);
            SetTitle(bindingNode);
            SetIcon(bindingNode, ScopeType.ScriptingNode);

            SetCommand(ScopeType.ScriptingNode,
                Enumerations.ButtonType.Add,
                Enumerations.ButtonType.Delete);
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

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, nameof(ITemplateValue.TemplateTitle));
                formBinding.SchemaData.AddBinding(schemaTitleData, nameof(ISchemaDefinitionValue.SchemaTitle));
                formBinding.NodeData.AddBinding(nodeNameData, nameof(ISchemaNodeValue.NodeName));

                RenderValueAsList.Load(nodeRenderAsData);
                formBinding.NodeData.AddBinding(nodeRenderAsData, nameof(ISchemaNodeValue.RenderValueAs), RenderValueAsList.NullValue);
                formBinding.NodeData.AddBinding(nodeRenderOrderData, nameof(ISchemaNodeValue.NodeOrder));

                XScopeList.Load(nodeObjectScopeData, nodeObjectPropertyData, formBinding.Builders, "(n/a)");
                formBinding.NodeData.AddBinding(nodeObjectScopeData, nameof(ISchemaNodeValue.ObjectScope), ScopeNameList.NullValue);
                formBinding.NodeData.AddBinding(nodeObjectPropertyData, nameof(ISchemaNodeValue.ObjectProperty));

                PropertyNameList.Load(nodeModelPropertyData, "(n/a)");
                formBinding.NodeData.AddBinding(nodeModelPropertyData, nameof(ISchemaNodeValue.ModelPropertyId), PropertyNameList.NullValue);

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
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
