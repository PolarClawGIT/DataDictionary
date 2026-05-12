using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
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
        //TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public TemplateObject()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingObject);

            SetRowState(
                bindingTemplate,
                bindingObject);
            SetIcon(ScopeType.ScriptingObject);

            SetCommand(ScopeType.ScriptingObject,
                Enumerations.ButtonType.Add,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.Select);
        }

        public TemplateObject(ITemplateIndex template) : this()
        { templateIndex = new TemplateIndex(template); }

        // Temporal is handled by the Template screen and gets past to this screen.
        //public TemplateObject(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        //{ temporalIndex = new TemporalIndex(); }

        public TemplateObject(ITemplateIndex template,
            Func<ITemplateData> getData) : this(template)
        { formBinding.GetData = getData; }


        private void TemplateObject_Load(object sender, EventArgs e)
        {
            formBinding.LoadValue(templateIndex);

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

                BindingObject_CurrentChanged(bindingObject, new EventArgs());
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            formBinding.ObjectData.AddValue(new TemplateObjectValue(templateIndex));
        }

        protected override void SelectCommand_Click(Object sender, EventArgs e)
        {
            base.SelectCommand_Click(sender, e);

            using (SelectionDialog dialog = new SelectionDialog(this))
            {
                dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                dialog.FilterScopes.Add(ScopeType.ModelEntity);
                dialog.FilterScopes.Add(ScopeType.ModelProcess);

                dialog.BuildData(formBinding.GetObjectPaths(), GetDescription);

                if (dialog.ShowDialog(this) is DialogResult.OK)
                {
                    foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                    { formBinding.ObjectData.AddValue(new TemplateObjectValue(templateIndex)); }

                    bindingObject.ResetCurrentItem();
                }
            }

            String GetDescription(INamedScopeSourceValue value)
            {   // Needed a physical method rather then a Lambda expression.
                // Properties don't get passed as expected.
                // I needed the property passed by Reference and that did not work.
                if (value is AttributeValue attribute)
                { return attribute.AttributeDescription ?? String.Empty; }

                else if (value is EntityValue entity)
                { return entity.EntityDescription ?? String.Empty; }

                else if (value is ProcessValue process)
                { return process.ProcessDescription ?? String.Empty; }

                else { return String.Empty; }
            }
        }


        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        private void BindingObject_CurrentChanged(object sender, EventArgs e)
        {
            if (formBinding.ObjectData.TryGetValue(out TemplateObjectValue? objectValue))
            {
                objectScopeData.ReadOnly = false;
                objectNameData.ReadOnly = false;
                objectIsExcluded.Enabled = true;
                objectKeepOrphaned.Enabled = true;

                Boolean inModel = BusinessData.NamedScope.PathKeys(objectValue.ObjectPath).Count > 0;
                isInModelData.Checked = inModel;
            }
            else
            {
                objectScopeData.ReadOnly = true;
                objectNameData.ReadOnly = true;
                objectIsExcluded.Enabled = false;
                objectKeepOrphaned.Enabled = false;
                isInModelData.Checked = false;
            }
        }

        private void ObjectNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(objectNameData.Text).ToArray());
            objectNameData.Text = path.MemberFullPath;
        }
    }
}
