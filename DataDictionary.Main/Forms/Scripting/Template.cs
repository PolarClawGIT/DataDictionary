using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
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

                transformScriptData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TransformScript)));
                transformExceptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TransformException), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

                rootDirectoryData.ValueMember = nameof(TemplateDirectoryEnumeration.Value);
                rootDirectoryData.DisplayMember = nameof(TemplateDirectoryEnumeration.DisplayName);
                rootDirectoryData.DataSource = TemplateDirectoryEnumeration.Members.Values.ToList();
                rootDirectoryData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingTemplate, nameof(ITemplateValue.RootDirectory),
                    false, DataSourceUpdateMode.OnPropertyChanged)
                { DataSourceNullValue = TemplateDirectoryType.Null });

                ScopeNameList.Load(breakOnScopeData);
                breakOnScopeData.DataBindings.Add(new Binding(nameof(breakOnScopeData.SelectedValue), bindingTemplate, nameof(ITemplateValue.BreakOnScope), false, DataSourceUpdateMode.OnPropertyChanged, ScopeNameList.NullValue));

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

    }
}
