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
                nodeBinding: bindingNode,
                ownerBinding: bindingNodeOwner);

            SetRowState(
                bindingNode,
                bindingNodeOwner);
            SetTitle(bindingNode);
            SetIcon(bindingNode);

            SetCommand(
                ButtonType.Add,
                ButtonType.Delete);

            nodeTreeView.ImageList = new ImageList();
            nodeTreeView.ImageList.AddImages(Enum.GetValues<NodeRenderAsType>().ToList());

            CommandButtons[ButtonType.Delete].Enabled = false;
            IsEnabled(false);
        }


        public SchemaNode(ITemplateIndex template, ISchemaDefinitionIndex schema) : this()
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

                formBinding.NodeData.AddBinding(isNameOverrideData, e => e.IsNameOverride);

                RenderValueAsList.Load(nodeRenderAsData);
                formBinding.NodeData.AddBinding(nodeRenderAsData, e => e.RenderValueAs);
                formBinding.NodeData.AddBinding(nodeRenderOrderData, e => e.NodeOrder);

                formBinding.NodeData.AddBinding(isObjectValueData, e => e.IsObjectValue);
                formBinding.NodeData.AddBinding(isPropertyValueData, e => e.IsPropertyValue);
                formBinding.NodeData.AddBinding(isFixedValueData, e => e.IsFixedValue);

                //XScopeList.Load(nodeObjectScopeData, nodeObjectPropertyData, formBinding.Builders, "(n/a)");
                formBinding.NodeData.AddBinding(nodeObjectScopeData, e => e.ObjectNodeValue.ObjectScope);
                formBinding.NodeData.AddBinding(nodeObjectPropertyData, e => e.ObjectNodeValue.ObjectProperty);

                //XScopeList.Load(nodePropertyScopeData, formBinding.Builders, "(n/a)");
                //formBinding.NodeData.AddBinding(nodePropertyScopeData, e => e.PropertyNodeValue.ObjectScope, XScopeList.NullValue);

                PropertyNameList.Load(nodePropertyData, "(n/a)");
                formBinding.NodeData.AddBinding(nodePropertyData, e => e.PropertyNodeValue.PropertyId, PropertyNameList.NullValue);

                formBinding.NodeData.AddBinding(nodeFixedValueData, e => e.FixedNodeValue.FixedValue);

                formBinding.NodeData.LoadCombBox(nodeOwnerColumn, e => e.NodeId, e => e.NodeName);
                formBinding.OwnerData.AddBinding(nodeOwnerColumn, e => e.NodeOwnerId);
                nodeOwnershipData.AutoGenerateColumns = false;
                nodeOwnershipData.DataSource = formBinding.OwnerData;

                BuildTree();

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
            if (formBinding.NodeData.TryGetValue(out SchemaNodeValue? value))
            {
                CommandButtons[ButtonType.Delete].Enabled = true;
                IsEnabled(true);
            }
            else
            {
                CommandButtons[ButtonType.Delete].Enabled = false;
                IsEnabled(false);
            }
        }

        void IsEnabled(Boolean newState)
        {
            foreach (Control item in nodeOverviewLayout.Controls)
            { item.Enabled = newState; }

            foreach (Control item in valueSourceLayout.Controls)
            { item.Enabled = newState; }

            nodeTabs.Enabled = newState;
        }

        void BuildTree()
        {
            // TODO: Currently simple, just list the nodes

            nodeTreeView.BeginUpdate();
            nodeTreeView.Nodes.Clear();

            foreach (SchemaNodeValue item in
                formBinding.NodeData.
                OrderBy(o => o.NodeOrder).
                ThenBy(o => o.NodeName ?? String.Empty))
            {
                TreeNode newNode = new TreeNode(item.NodeName ?? "(no node name)")
                {
                    ImageKey = Enum.GetName<NodeRenderAsType>(item.RenderValueAs),
                    SelectedImageKey = Enum.GetName<NodeRenderAsType>(item.RenderValueAs)
                };

                nodeTreeView.Nodes.Add(newNode);
            }

            nodeTreeView.EndUpdate();
        }

        private void BindingNode_ListChanged(object sender, ListChangedEventArgs e)
        {
            if(formBinding.TryGetValue(out _))
            { BuildTree(); }
        }
    }
}
