using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingTemplate : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTemplate.Current is ITemplateValue current && ReferenceEquals(current, item); }


        public ScriptingTemplate() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(templateToolStrip, 0);
        }

        public ScriptingTemplate(ITemplateValue? templateItem) : this()
        {
            if (templateItem is null)
            {
                templateItem = new TemplateValue();
                BusinessData.ScriptingEngine.Templates.Add(templateItem);
            }

            TemplateIndex key = new TemplateIndex(templateItem);
            bindingTemplate.DataSource = new BindingView<TemplateValue>(BusinessData.ScriptingEngine.Templates, w => key.Equals(w));
            bindingTemplate.Position = 0;

            Setup(bindingTemplate);

            if (bindingTemplate.Current is ITemplateValue current)
            {
                //bindingProperty.DataSource = new BindingView<AttributePropertyValue>(BusinessData.DomainModel.Attributes.Properties, w => key.Equals(w));
            }
        }

        private void ScriptingTemplate_Load(object sender, EventArgs e)
        {
            ITemplateValue nameOfValues;

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingTemplate, nameof(nameOfValues.TemplateTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            templateTitleData.DataBindings.Add(new Binding(nameof(templateTitleData.Text), bindingTemplate, nameof(nameOfValues.TemplateTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            templateDescriptionData.DataBindings.Add(new Binding(nameof(templateDescriptionData.Text), bindingTemplate, nameof(nameOfValues.TemplateDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            SupportedFolderList.Load(rootDirectoryData);
            rootDirectoryData.DataBindings.Add(new Binding(nameof(rootDirectoryData.SelectedValue), bindingTemplate, nameof(nameOfValues.RootDirectory), true, DataSourceUpdateMode.OnPropertyChanged, SupportedFolderList.NullValue));

            ScopeNameList.Load(breakOnScopeData);
            breakOnScopeData.DataBindings.Add(new Binding(nameof(breakOnScopeData.SelectedValue), bindingTemplate, nameof(nameOfValues.BreakOnScope), false, DataSourceUpdateMode.OnPropertyChanged, ScopeNameList.NullValue));

            documentDirectoryData.DataBindings.Add(new Binding(nameof(documentDirectoryData.Text), bindingTemplate, nameof(nameOfValues.DocumentDirectory), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentPrefixData.DataBindings.Add(new Binding(nameof(documentPrefixData.Text), bindingTemplate, nameof(nameOfValues.DocumentPrefix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentSuffixData.DataBindings.Add(new Binding(nameof(documentSuffixData.Text), bindingTemplate, nameof(nameOfValues.DocumentSuffix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentExtensionData.DataBindings.Add(new Binding(nameof(documentExtensionData.Text), bindingTemplate, nameof(nameOfValues.DocumentExtension), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            ScriptAsList.Load(scriptAsData);
            scriptAsData.DataBindings.Add(new Binding(nameof(scriptAsData.SelectedValue), bindingTemplate, nameof(nameOfValues.ScriptAs), false, DataSourceUpdateMode.OnPropertyChanged, ScriptAsList.NullValue));
            scriptingDirectoryData.DataBindings.Add(new Binding(nameof(documentDirectoryData.Text), bindingTemplate, nameof(nameOfValues.ScriptDirectory), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            scriptingPrefixData.DataBindings.Add(new Binding(nameof(scriptingPrefixData.Text), bindingTemplate, nameof(nameOfValues.ScriptPrefix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            scriptingSuffixData.DataBindings.Add(new Binding(nameof(scriptingSuffixData.Text), bindingTemplate, nameof(nameOfValues.ScriptSuffix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            scriptingExtensionData.DataBindings.Add(new Binding(nameof(scriptingExtensionData.Text), bindingTemplate, nameof(nameOfValues.ScriptExtension), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            transformScriptData.DataBindings.Add(new Binding(nameof(transformScriptData.Text), bindingTemplate, nameof(nameOfValues.TransformScript), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            transformExceptionData.DataBindings.Add(new Binding(nameof(transformExceptionData.Text), bindingTemplate, nameof(nameOfValues.TransformException), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
        }

        private void rootDirectoryData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rootDirectoryData.SelectedItem is SupportedFolderList value)
            {
                if (value.Directory is null)
                { rootDirectoryExpanded.Text = String.Empty; }
                else
                { rootDirectoryExpanded.Text = value.Directory.FullName; }
            }
            else { rootDirectoryExpanded.Text = String.Empty; }
        }
    }
}
