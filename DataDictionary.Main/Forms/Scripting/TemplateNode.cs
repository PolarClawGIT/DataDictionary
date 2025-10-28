using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateNode : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex template && templateIndex.Equals(template); }

        FormBinding formBinding;
        TemplateIndex templateIndex = new TemplateIndex();

        private TemplateNode()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                NodeBinding = bindingNode,
                NodeOwnerBinding = bindingNodeOwner,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingTemplateNode); // Set the Default,
            SetTitle(bindingNode);

            SetRowState(bindingNode, bindingNodeOwner);

            SetCommand(ScopeType.ScriptingTemplateNode,
                CommandType.Add,
                CommandType.Delete);
        }

        public TemplateNode(ITemplate data, ITemplateIndex template) : this()
        {
            formBinding.Data = data;
            templateIndex = new TemplateIndex(template);
        }

        public void SetTemplateNode(ITemplateNodeIndex node)
        { formBinding.TrySetPosition(node); }

        private void TemplateNode_Load(object sender, EventArgs e)
        {
            formBinding.Load(templateIndex);
            DoBinding();

            void DoBinding()
            {
                templateData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                nodeNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.NodeName)));

                RenderValueAsList.Load(renderValueAsData);
                renderValueAsData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingNode, nameof(ITemplateNodeValue.RenderValueAs)));

                renderOrderData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.NodeOrder)));
                fixedValueData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.FixedValue)));

                ScopeNameList.Load(objectScopeData);
                objectScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingNode, nameof(ITemplateNodeValue.ObjectScope)));
                objectPropertyData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.ObjectProperty)));

                PropertyNameList.Load(modelPropertyData, "(n/a)");
                modelPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingNode, nameof(ITemplateNodeValue.ModelPropertyId)));

                ownershipData.AutoGenerateColumns = false;
                ownershipData.DataSource = bindingNodeOwner;
                formBinding.BindComboBox(nodeParentColumn);

                // Security
                if (formBinding.TryGetValue(out TemplateNodeValue? value))
                { IsLocked(formBinding.GetLocked()); }
                else
                { nodeDetailLayout.Enabled = false; }

                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            formBinding.NewValue(templateIndex);
            nodeDetailLayout.Enabled = true;
            IsLocked(formBinding.GetLocked());
            SetAuthorization(formBinding.GetAuthorization);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            formBinding.RemoveValue();
            nodeDetailLayout.Enabled = false;
            SetAuthorization(formBinding.GetAuthorization);
        }

        private void BindingNode_ListChanged(object sender, ListChangedEventArgs e)
        { formBinding.BuildTree(nodeTreeView); }

        private void BindingNodeOwner_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType is ListChangedType.ItemAdded or
                ListChangedType.ItemDeleted or
                ListChangedType.ItemChanged)
            { formBinding.BuildTree(nodeTreeView); }
        }

        private void NodeTreeView_NodeSelected(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Need to get the Hit Location itself because the flag may have been reset.
            if (e.Node is not null
                && e.Node.TreeView is not null
                && e.Node.TreeView.HitTest(e.Location).Location != TreeViewHitTestLocations.PlusMinus
                && e.Node.TryGetValue(out TemplateNodeValue? value))
            { formBinding.TrySetPosition(value); }
        }

        private void bindingNodeOwner_AddingNew(object sender, AddingNewEventArgs e)
        { e.NewObject = formBinding.NewOwner(); }

        private void ownershipData_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (sender is DataGridView gridView
            //    && gridView.Rows.Count > e.RowIndex
            //    && gridView.Rows[e.RowIndex] is DataGridViewRow row)
            //{
            //    if (row.DataBoundItem is TemplateNodeOwnerValue value)
            //    { row.ErrorText = formBinding.Validate(value); }
            //}
            //else
            //{ e.Cancel = true; }
        }
    }
}
