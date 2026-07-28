using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
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
                nodeBinding: bindingNode);

            SetRowState(
                bindingSchema,
                bindingNode);
            SetTitle(bindingNode);
            SetIcon(ScopeType.ScriptingNode);

            SetCommand(
                ButtonType.Add,
                ButtonType.Delete);

            CommandButtons[ButtonType.Delete].Enabled = false;
            nodesTree.CommandButtons[ButtonType.Browse].Visible = false;
            nodesTree.DoWork = DoWork;
        }


        public SchemaNode(ISchemaDefinitionIndex schema) : this()
        {
            schemaIndex = new SchemaDefinitionIndex(schema);
        }


        public SchemaNode(
            ITemplateIndex template,
            ISchemaDefinitionIndex schema,
            Func<ITemplateData> getData) : this(schema)
        { formBinding.GetData = getData; }

        private void SchemaNode_Load(object sender, EventArgs e)
        {
            if (schemaIndex.HasValue)
            { formBinding.LoadValue(schemaIndex); }
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
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.SchemaData.AddBinding(schemaTitleData, e => e.SchemaTitle);
                formBinding.NodeData.AddBinding(nodeNameData, e => e.NodeName);

                ScopeNameList.Load(objectScopeData, ScopeType.Null,
                    ScopeType.ModelAttribute, ScopeType.ModelAttributeProperty,
                    ScopeType.ModelEntity, ScopeType.ModelEntityProperty);
                formBinding.NodeData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);

                formBinding.NodeData.AddBinding(objectPropertyData, e => e.ObjectProperty);

                ObjectValueTypeList.Load(objectTypeData);
                formBinding.NodeData.AddBinding(objectTypeData, e => e.ObjectType, ObjectValueTypeList.NullValue);

                RenderValueAsList.Load(renderValueAsData);
                formBinding.NodeData.AddBinding(renderValueAsData, e => e.RenderValueAs, RenderValueAsList.NullValue);

                XmlTypeCodeList.Load(renderTypeData);
                formBinding.NodeData.AddBinding(renderTypeData, e => e.RenderTypeAs, XmlTypeCodeList.NullValue);

                formBinding.NodeData.AddBinding(renderOrderData, e => e.RenderOrder);

                nodesTree.LoadTree(formBinding.GetBuilders());

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
            }

        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            formBinding.AddNew(templateIndex, schemaIndex);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            formBinding.RemoveCurrent();
        }


        protected override void HandleMessage(RefreshRow message)
        {
            base.HandleMessage(message);

            if (message is RefreshRow<SchemaDefinitionIndex> rowMessage
                && rowMessage.Key.Equals(schemaIndex))
            { formBinding.LoadValue(schemaIndex); }
        }

        private void NodesTree_OnNodeSelected(object sender, XmlBuilderIndex e)
        {
            formBinding.TrySetNode(e);
        }

        private void NodesTree_OnButtonClick(object sender, ButtonType e)
        {
            switch (e)
            {
                case ButtonType.Default:
                    break;
                case ButtonType.Browse:
                    break;
                case ButtonType.Select:
                    break;
                case ButtonType.Add:
                    break;
                case ButtonType.Delete:
                    break;
                case ButtonType.Save:
                    break;
                case ButtonType.Open:
                    break;
                case ButtonType.Refresh:
                    break;
                case ButtonType.Sync:
                    break;
                case ButtonType.Import:
                    break;
                case ButtonType.Export:
                    break;
                case ButtonType.OpenDatabase:
                    break;
                case ButtonType.SaveDatabase:
                    break;
                case ButtonType.DeleteDatabase:
                    break;
                case ButtonType.HistoryDatabase:
                    break;
                case ButtonType.SecurityDatabase:
                    break;
                default:
                    break;
            }
        }
    }
}
