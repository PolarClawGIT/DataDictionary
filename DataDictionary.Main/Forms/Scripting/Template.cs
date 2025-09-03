using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
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
    partial class Template : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex template && templateIndex.Equals(template); }

        FormBinding formBinding;
        TemplateIndex templateIndex = new TemplateIndex();
        TemporalIndex? temporalIndex = null;

        public Template() : base()
        {
            InitializeComponent();
            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                DataSourceBinding = bindingTemplateData,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingTemplate);
            SetTitle(bindingTemplate);
            SetRowState(bindingTemplate);

            SetCommand(ScopeType.ScriptingTemplate,
                CommandImageType.Delete,
                CommandImageType.OpenDatabase,
                CommandImageType.SaveDatabase,
                CommandImageType.DeleteDatabase,
                CommandImageType.HistoryDatabase);
        }

        public Template(ITemplateIndex? template) : this()
        {
            if (template is ITemplateIndex)
            { templateIndex = new TemplateIndex(template); }
            else { templateIndex = new TemplateIndex(formBinding.NewValue()); }
        }

        public Template(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        { temporalIndex = new TemporalIndex(); }

        private void Template_Load(object sender, EventArgs e)
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
                templateTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                templateDescriptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateDescription)));

                DataSourceNameList.Load(dataSourceIdColumn);
                templateDataSource.AutoGenerateColumns = false;
                templateDataSource.DataSource = bindingTemplateData;

                transformScriptData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TransformScript)));
                transformExceptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TransformException), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

                rootDirectoryData.ValueMember = nameof(TemplateDirectoryEnumeration.Value);
                rootDirectoryData.DisplayMember = nameof(TemplateDirectoryEnumeration.DisplayName);
                rootDirectoryData.DataSource = TemplateDirectoryEnumeration.Members.Values.ToList();
                rootDirectoryData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingTemplate, nameof(ITemplateValue.TemplateDirectory),
                    false, DataSourceUpdateMode.OnPropertyChanged)
                { DataSourceNullValue = TemplateDirectoryType.Null });

                ScopeNameList.Load(breakOnScopeData);
                breakOnScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingTemplate, nameof(ITemplateValue.TemplateBreakOn), false, DataSourceUpdateMode.OnPropertyChanged, ScopeNameList.NullValue));

                documentDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentDirectory), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                documentPrefixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentPrefix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                documentSuffixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentSuffix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                documentExtensionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentExtension), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

                scriptingDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptDirectory), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                scriptingPrefixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptPrefix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                scriptingSuffixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptSuffix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
                scriptingExtensionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptExtension), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            }
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

        private void DocumentDirectoryData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out TemplateValue? current))
            {
                DirectoryInfo rootDirectory = new DirectoryInfo(rootPhysicalDirectory.Text);
                folderBrowserDialog.InitialDirectory = Path.Combine(rootDirectory.FullName, current.DocumentDirectory ?? String.Empty);

                if (folderBrowserDialog.ShowDialog() is DialogResult.OK
                    && folderBrowserDialog.SelectedPath.Length > rootDirectory.FullName.Length
                    && String.Equals(folderBrowserDialog.SelectedPath.Substring(0, rootDirectory.FullName.Length), rootDirectory.FullName, StringComparison.CurrentCultureIgnoreCase))
                {
                    current.DocumentDirectory = folderBrowserDialog.SelectedPath.Substring(rootDirectory.FullName.Length + 1);
                    documentPhysicalDirectory.Text = Path.Combine(rootDirectory.FullName, current.DocumentDirectory);
                }
            }
        }

        private void DocumentDirectoryData_Validated(object sender, EventArgs e)
        { documentPhysicalDirectory.Text = Path.Combine(rootPhysicalDirectory.Text, documentDirectoryData.Text); }

        private void ScriptingDirectoryData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out TemplateValue? current))
            {
                DirectoryInfo rootDirectory = new DirectoryInfo(rootPhysicalDirectory.Text);
                folderBrowserDialog.InitialDirectory = Path.Combine(rootDirectory.FullName, current.ScriptDirectory ?? String.Empty);

                if (folderBrowserDialog.ShowDialog() is DialogResult.OK
                    && folderBrowserDialog.SelectedPath.Length > rootDirectory.FullName.Length
                    && String.Equals(folderBrowserDialog.SelectedPath.Substring(0, rootDirectory.FullName.Length), rootDirectory.FullName, StringComparison.CurrentCultureIgnoreCase))
                {
                    current.ScriptDirectory = folderBrowserDialog.SelectedPath.Substring(rootDirectory.FullName.Length + 1);
                    scriptingPhysicalDirectory.Text = Path.Combine(rootDirectory.FullName, current.ScriptDirectory);
                }
            }
        }

        private void ScriptingDirectoryData_Validated(object sender, EventArgs e)
        { scriptingPhysicalDirectory.Text = Path.Combine(rootPhysicalDirectory.Text, scriptingDirectoryData.Text); }


        private void RootDirectoryData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rootDirectoryData.SelectedValue is TemplateDirectoryType value
                && TemplateDirectoryEnumeration.Cast(value).Directory is DirectoryInfo directory)
            { rootPhysicalDirectory.Text = directory.FullName; }
            else { rootPhysicalDirectory.Text = String.Empty; }
        }

        private void RootDirectoryData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (rootDirectoryData.SelectedValue is TemplateDirectoryType value
                && formBinding.TryGetValue(out TemplateValue? current))
            {
                //Note: For reason unknown, current.TemplateDirectory has not been updated
                //at this point. Setting the current.RootDirectory directly solves this.
                current.RootDirectory = TemplateDirectoryEnumeration.Cast(value).Name;
                current.DocumentDirectory = null;
                current.ScriptDirectory = null;
            }
        }

        private void BindingTemplateData_AddingNew(object sender, AddingNewEventArgs e)
        { e.NewObject = formBinding.NewDataSource(); }
    }
}
