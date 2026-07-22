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
                bindingNode);
            SetTitle(bindingNode);
            SetIcon(bindingNode);

            SetCommand(
                ButtonType.Add,
                ButtonType.Delete);

            CommandButtons[ButtonType.Delete].Enabled = false;
            nodesTree.DoWork = DoWork;
            IsEnabled(false);
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

                RenderValueAsList.Load(renderValueAsData);
                formBinding.NodeData.AddBinding(renderValueAsData, e => e.RenderValueAs, RenderValueAsList.NullValue);

                //renderTypeData
                formBinding.NodeData.AddBinding(renderOrderData, e => e.RenderOrder);
                

                //formBinding.NodeData.AddBinding(isNameOverrideData, e => e.IsNameOverride);

                //RenderValueAsList.Load(nodeRenderAsData);
                //formBinding.NodeData.AddBinding(nodeRenderAsData, e => e.RenderValueAs);
                //formBinding.NodeData.AddBinding(nodeRenderOrderData, e => e.RenderOrder);

                //formBinding.NodeData.AddBinding(isObjectValueData, e => e.IsObjectValue);
                //formBinding.NodeData.AddBinding(isPropertyValueData, e => e.IsPropertyValue);
                //formBinding.NodeData.AddBinding(isFixedValueData, e => e.IsFixedValue);

                //XScopeList.Load(nodeObjectScopeData, nodeObjectPropertyData, formBinding.Builders, "(n/a)");
                //formBinding.NodeData.AddBinding(nodeObjectScopeData, e => e.ObjectNodeValue.ObjectScope);
                //formBinding.NodeData.AddBinding(nodeObjectPropertyData, e => e.ObjectNodeValue.ObjectProperty);

                //XScopeList.Load(nodePropertyScopeData, formBinding.Builders, "(n/a)");
                //formBinding.NodeData.AddBinding(nodePropertyScopeData, e => e.PropertyNodeValue.ObjectScope, XScopeList.NullValue);

                //PropertyNameList.Load(nodePropertyData, "(n/a)");
                //formBinding.NodeData.AddBinding(nodePropertyData, e => e.PropertyNodeValue.PropertyId, PropertyNameList.NullValue);

                //formBinding.NodeData.AddBinding(nodeFixedValueData, e => e.FixedNodeValue.FixedValue);

                //formBinding.NodeData.LoadCombBox(nodeOwnerColumn, e => e.NodeId, e => e.NodeName);
                //formBinding.OwnerData.AddBinding(nodeOwnerColumn, e => e.NodeOwnerId);
                //nodeOwnershipData.AutoGenerateColumns = false;
                //nodeOwnershipData.DataSource = formBinding.OwnerData;

                nodesTree.HeaderText = String.Empty;
                nodesTree.LoadTree(formBinding.GetBuilders());
                //nodeTreeView.LoadTree(formBinding.)


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

        private void BindingNode_CurrentChanged(object sender, EventArgs e)
        {

        }

        void IsEnabled(Boolean newState)
        {
            //foreach (Control item in nodeOverviewLayout.Controls)
            //{ item.Enabled = newState; }

            //foreach (Control item in valueSourceLayout.Controls)
            //{ item.Enabled = newState; }

            //nodeTabs.Enabled = newState;
        }

        //namedScopeData.HeaderText
        private void BindingNode_ListChanged(object sender, ListChangedEventArgs e)
        {

        }

        private void NodesTree_OnNodeSelected(object sender, XmlBuilderIndex e)
        {
            formBinding.TrySetNode(e);
        }
    }
}
