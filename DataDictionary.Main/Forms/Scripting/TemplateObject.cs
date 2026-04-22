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
    partial class TemplateObject : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public TemplateObject()
        {
            InitializeComponent();

            formBinding = new FormBinding()
            {
                DoWork = base.DoWork,
                TemplateBinding = bindingTemplate,
                ObjectBinding = bindingObject
            };

            SetRowState(
                bindingTemplate,
                bindingObject);
            //SetIcon(ScopeType.ScriptingObject);

            SetCommand(ScopeType.ScriptingObject,
                Enumerations.ButtonType.Add,
                Enumerations.ButtonType.Select,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);
        }

        public TemplateObject(ITemplateIndex template) : this()
        { templateIndex = new TemplateIndex(template); }

        public TemplateObject(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        { temporalIndex = new TemporalIndex(); }

        public TemplateObject(ITemplateIndex template,
            Func<TemplateIndex, BindingView<TemplateValue>> getTemplates,
            Func<TemplateIndex, BindingView<TemplateObjectValue>> getObjects)
            : this(template)
        {
            formBinding.GetTemplates = getTemplates;
            formBinding.GetObjects = getObjects;
        }


        private void TemplateObject_Load(object sender, EventArgs e)
        {
            formBinding.Load(templateIndex);

            DoBinding();

            void DoBinding()
            {
                templateTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));

                ScopeNameList.Load(objectScopeColumn);
                objectData.AutoGenerateColumns = false;
                objectData.DataSource = bindingObject;

                ScopeNameList.Load(objectScopeData);
                objectScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingObject, nameof(ITemplateObjectValue.ObjectScope), false) { DataSourceNullValue = ScopeNameList.NullValue });
                objectNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingObject, nameof(ITemplateObjectValue.ObjectName)));
                objectIsExcluded.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingObject, nameof(ITemplateObjectValue.IsExcluded)));
                objectKeepOrphaned.DataBindings.Add(new Binding(nameof(CheckBox.Checked), bindingObject, nameof(ITemplateObjectValue.KeepOrphaned)));

                if (bindingObject.Position >= 0)
                { BindingObject_CurrentChanged(bindingObject, new EventArgs()); }
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            formBinding.TryAddValue(out TemplateObjectValue? _);
        }

        protected override void SelectCommand_Click(Object sender, EventArgs e)
        {
            base.SelectCommand_Click(sender, e);
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

        private void BindingObject_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingObject.Position >= 0)
            {
                objectScopeData.ReadOnly = false;
                objectNameData.ReadOnly = false;
                objectIsExcluded.Enabled = true;
                objectKeepOrphaned.Enabled = true;
            }
            else
            {
                objectScopeData.ReadOnly = true;
                objectNameData.ReadOnly = true;
                objectIsExcluded.Enabled = false;
                objectKeepOrphaned.Enabled = false;
            }
        }
    }
}
