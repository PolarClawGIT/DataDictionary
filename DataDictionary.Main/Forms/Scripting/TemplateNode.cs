using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.Model.ComboBoxList;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Enumerations.CommandType.Delete,
                Enumerations.CommandType.OpenDatabase,
                Enumerations.CommandType.SaveDatabase,
                Enumerations.CommandType.DeleteDatabase,
                Enumerations.CommandType.HistoryDatabase);
            newAttributeCommand.Image = ScopeType.ScriptingTemplateAttribute.GetImage(Enumerations.CommandType.Add);
            newElementCommand.Image = ScopeType.ScriptingTemplateElement.GetImage(Enumerations.CommandType.Add);
            AddCommands(nodeCommands, ToolStripItemDisplayStyle.Image);
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
                formBinding.SetPosition(nodeIndex);
                templateData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                nodeNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(BindingValue.NodeName)));

                RenderValueAsList.Load(renderValueAsData);
                renderValueAsData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingTemplateNode, nameof(BindingValue.RenderValueAs)));

                renderOrderData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(BindingValue.RenderOrder)));
                fixedValueData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(BindingValue.FixedValue)));

                ScopeNameList.Load(objectScopeData);
                objectScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingTemplateNode, nameof(BindingValue.ObjectScope)));
                objectPropertyData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplateNode, nameof(BindingValue.ObjectProperty)));

                PropertyNameList.Load(modelPropertyData, "(n/a)");
                modelPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedItem), bindingTemplateNode, nameof(BindingValue.ModelPropertyId)));
            }
        }

        private void NewAttributeCommand_Click(object sender, EventArgs e)
        {

        }

        private void NewElementCommand_Click(object sender, EventArgs e)
        {

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
