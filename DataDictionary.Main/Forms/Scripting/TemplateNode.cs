using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.Model.ComboBoxList;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using DataDictionary.Main.Messages;
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
        TemplateNodeIndex nodeIndex = new TemplateNodeIndex();
        TemporalIndex? temporalIndex = null;

        private TemplateNode()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                TemplateNodeBinding = bindingTemplateNode,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingTemplateNode);
            SetTitle(bindingTemplate);
            SetRowState(bindingTemplate);

            SetCommand(ScopeType.ScriptingTemplateNode,
                CommandType.Add,
                CommandType.Delete,
                CommandType.OpenDatabase,
                CommandType.SaveDatabase,
                CommandType.DeleteDatabase,
                CommandType.HistoryDatabase);
        }

        public TemplateNode(ITemplateIndex template) : this()
        { templateIndex = new TemplateIndex(template); }

        public TemplateNode(ITemplateIndex template, ITemplateNodeIndex node) : this(template)
        { nodeIndex = new TemplateNodeIndex(node); }

        public TemplateNode(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        { temporalIndex = new TemporalIndex(); }

        public TemplateNode(ITemplateIndex template, ITemplateNodeIndex node, ITemporalIndex temporal) : this(template, node)
        { temporalIndex = new TemporalIndex(); }

        private void TemplateNode_Load(object sender, EventArgs e)
        {
            if (temporalIndex is null)
            {
                formBinding.Load(templateIndex);
                DoBinding();
            }
            else
            { formBinding.Load(templateIndex, temporalIndex, onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is null)
                {
                    DoBinding();
                    SendMessage(new RefreshNavigation());
                }
            }

            void DoBinding()
            {
                templateData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                nodeNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(ITemplateNodeValue.NodeName)));

                RenderValueAsList.Load(renderValueAsData);
                renderValueAsData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingTemplateNode, nameof(ITemplateNodeValue.RenderValueAs)));

                renderOrderData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(ITemplateNodeValue.NodeOrder)));
                fixedValueData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(ITemplateNodeValue.FixedValue)));

                ScopeNameList.Load(objectScopeData);
                objectScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingTemplateNode, nameof(ITemplateNodeValue.ObjectScope)));
                objectPropertyData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(ITemplateNodeValue.ObjectProperty)));

                PropertyNameList.Load(modelPropertyData, "(n/a)");
                modelPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingTemplateNode, nameof(ITemplateNodeValue.ModelPropertyId)));

                // Security
                if (formBinding.TryGetValue(out TemplateNodeValue? value))
                { IsLocked(formBinding.GetLocked()); }
                else
                { nodeLayout.Enabled = false; }

                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            formBinding.NewValue(templateIndex);
            IsLocked(formBinding.GetLocked());
            SetAuthorization(formBinding.GetAuthorization);
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            formBinding.RemoveValue();
            nodeLayout.Enabled = false; 
            SetAuthorization(formBinding.GetAuthorization);
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
