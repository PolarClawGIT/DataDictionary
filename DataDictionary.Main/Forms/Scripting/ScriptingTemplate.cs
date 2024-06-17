using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.ScriptingData.Template;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Main.Forms.Scripting.ComboBoxList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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

            transformFilePath.Width = transformToolStrip.Width -
                transformToolStrip.Items.
                Cast<ToolStripItem>().
                Where(w => w != transformFilePath).
                Sum(s => s.Width) - 3;
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
                bindingPath.DataSource = new BindingView<TemplatePathValue>(BusinessData.ScriptingEngine.Templates.Paths, w => key.Equals(w));
                bindingNode.DataSource = new BindingView<TemplateNodeValue>(BusinessData.ScriptingEngine.Templates.Nodes, w => key.Equals(w));
                bindingAttribute.DataSource = new BindingView<TemplateAttributeValue>(BusinessData.ScriptingEngine.Templates.Attributes, w => key.Equals(w));
            }
        }

        private void ScriptingTemplate_Load(object sender, EventArgs e)
        {
            ITemplateValue nameOfValues;
            ITemplateNodeValue nameOfNode;

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingTemplate, nameof(nameOfValues.TemplateTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            templateTitleData.DataBindings.Add(new Binding(nameof(templateTitleData.Text), bindingTemplate, nameof(nameOfValues.TemplateTitle), false, DataSourceUpdateMode.OnPropertyChanged));
            templateDescriptionData.DataBindings.Add(new Binding(nameof(templateDescriptionData.Text), bindingTemplate, nameof(nameOfValues.TemplateDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            DirectoryNameList.Load(rootDirectoryData);
            rootDirectoryData.DataBindings.Add(new Binding(nameof(rootDirectoryData.SelectedValue), bindingTemplate, nameof(nameOfValues.RootDirectory), false, DataSourceUpdateMode.OnPropertyChanged, DirectoryNameList.NullValue));

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

            ScopeNameList.Load(propertyScopeData);
            propertyScopeData.DataBindings.Add(new Binding(nameof(propertyScopeData.SelectedValue), bindingNode, nameof(nameOfNode.PropertyScope), false, DataSourceUpdateMode.OnPropertyChanged, ScopeNameList.NullValue));
            propertyNameData.DataBindings.Add(new Binding(nameof(propertyNameData.Text), bindingNode, nameof(nameOfNode.PropertyName), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            nodeNameData.DataBindings.Add(new Binding(nameof(nodeNameData.Text), bindingNode, nameof(nameOfNode.NodeName), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            NodeValueAsList.Load(nodeValueAsData);
            nodeValueAsData.DataBindings.Add(new Binding(nameof(nodeValueAsData.SelectedValue), bindingNode, nameof(nameOfNode.NodeValueAs), false, DataSourceUpdateMode.OnPropertyChanged, NodeValueAsList.NullValue));

            templatePathData.AutoGenerateColumns = false;
            templatePathData.DataSource = bindingPath;

            PropertyNameList.Load(attributePropertyColumn);

            ElementSelection_Load();

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingTemplate.Current is not ITemplateValue);
        }

        private void RootDirectoryData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rootDirectoryData.SelectedItem is DirectoryNameList value
                && bindingTemplate.Current is TemplateValue current)
            {
                if (value.Directory is null)
                {
                    rootDirectoryExpanded.Text = String.Empty;
                    current.DocumentDirectory = null;
                    current.ScriptDirectory = null;
                }
                else
                {
                    rootDirectoryExpanded.Text = value.Directory.FullName;
                    current.DocumentDirectory = null;
                    current.ScriptDirectory = null;
                }
            }
            else { rootDirectoryExpanded.Text = String.Empty; }
        }

        private void DeleteTemplateCommand_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            { BusinessData.ScriptingEngine.Templates.Delete(current); }
        }

        private void DocumentDirectoryPicker_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current
                && new DirectoryTypeKey(current.RootDirectory).ToDirectoryInfo() is DirectoryInfo directory)
            {
                folderBrowserDialog.InitialDirectory = Path.Combine(directory.FullName, current.DocumentDirectory ?? String.Empty);
                if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
                {
                    if (folderBrowserDialog.SelectedPath.Length > directory.FullName.Length
                        && String.Equals(folderBrowserDialog.SelectedPath.Substring(0, directory.FullName.Length), directory.FullName, StringComparison.CurrentCultureIgnoreCase))
                    { current.DocumentDirectory = folderBrowserDialog.SelectedPath.Substring(directory.FullName.Length + 1); }
                }
            }
        }

        private void ScriptingDirectoryPicker_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current
                && new DirectoryTypeKey(current.RootDirectory).ToDirectoryInfo() is DirectoryInfo directory)
            {
                folderBrowserDialog.InitialDirectory = Path.Combine(directory.FullName, current.ScriptDirectory ?? String.Empty);
                if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
                {
                    if (folderBrowserDialog.SelectedPath.Length > directory.FullName.Length
                        && String.Equals(folderBrowserDialog.SelectedPath.Substring(0, directory.FullName.Length), directory.FullName, StringComparison.CurrentCultureIgnoreCase))
                    { current.ScriptDirectory = folderBrowserDialog.SelectedPath.Substring(directory.FullName.Length + 1); }
                }
            }
        }

        private void ScriptAsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            { current.ScriptExtension = new ScriptAsTypeKey(current).ToExtension(); }
        }

        private void TransformParseCommand_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            {
                if (current.TransformException is null && current.TransformXml is not null)
                {
                    XDocument value = new XDocument(current.TransformXml);
                    if (value.Declaration is not null)
                    { value.Declaration.Encoding = String.Empty; }

                    current.TransformScript = current.TransformXml.ToString();
                }
            }
        }

        private void TransformImportCommand_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            {
                if (new DirectoryTypeKey(current.RootDirectory).ToDirectoryInfo() is DirectoryInfo directory)
                { openFileDialog.InitialDirectory = directory.FullName; }

                openFileDialog.DefaultExt = "xslt";
                openFileDialog.Filter = "XML Transformation (*.xslt)|*.xslt|XML (*.xml)|*.xml|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() is DialogResult.OK)
                {
                    transformFilePath.Text = openFileDialog.FileName;
                    XDocument file = XDocument.Load(openFileDialog.OpenFile());
                    if (file.Root is XElement)
                    { current.TransformScript = file.Root.ToString(); }
                }
            }
        }

        private void TransformExportCommand_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            {
                if (new DirectoryTypeKey(current.RootDirectory).ToDirectoryInfo() is DirectoryInfo directory)
                { saveFileDialog.InitialDirectory = directory.FullName; }

                if (String.IsNullOrWhiteSpace(transformFilePath.Text))
                { saveFileDialog.FileName = current.TemplateTitle; }
                else { saveFileDialog.FileName = transformFilePath.Text; }

                saveFileDialog.DefaultExt = "xslt";
                saveFileDialog.Filter = "XML Transformation (*.xslt)|*.xslt|XML (*.xml)|*.xml|All files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() is DialogResult.OK && current.TransformXml is not null)
                {
                    XDocument file = new XDocument(current.TransformXml);
                    file.Save(saveFileDialog.FileName);
                }
            }
        }

        private void TransformToolStrip_Resize(object sender, EventArgs e)
        {
            transformFilePath.Width = transformToolStrip.Width -
                transformToolStrip.Items.
                Cast<ToolStripItem>().
                Where(w => w != transformFilePath).
                Sum(s => s.Width) - 3;
        }

        private void NamedScopeData_OnApply(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current) { }

            if (bindingPath.DataSource is IList<ITemplatePathValue> path
                && path.FirstOrDefault(
                    w => w.PathScope == templatePathSelect.Scope
                    && new NamedScopePath(NamedScopePath.Parse(w.PathName).ToArray()) == templatePathSelect.ScopePath)
                is ITemplatePathValue value)
            { bindingPath.Position = path.IndexOf(value); }
            else { bindingPath.AddNew(); }
        }

        private void BindingPath_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            {
                TemplatePathValue newItem = new TemplatePathValue(current);
                newItem.PathName = templatePathSelect.ScopePath.MemberFullPath;
                newItem.PathScope = templatePathSelect.Scope;
                e.NewObject = newItem;
            }
        }

        private void BindingPath_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingPath.Current is TemplatePathValue current)
            {
                NamedScopePath path = new NamedScopePath(NamedScopePath.Parse(current.PathName).ToArray());
                templatePathSelect.ScopePath = path;
                templatePathSelect.Scope = current.PathScope;
            }
        }


        Dictionary<ListViewItem, NodePropertyValue> nodeProperties = new Dictionary<ListViewItem, NodePropertyValue>();
        private void ElementSelection_Load()
        {
            elementSelection.Groups.Clear();
            elementSelection.Items.Clear();
            nodeProperties.Clear();
            schemaNodeLayout.Enabled = false;

            foreach (var groups in BusinessData.ScriptingEngine.Columns.GroupBy(g => g.PropertyScope))
            {
                ListViewGroup newGroup = new ListViewGroup(groups.Key.ToName());
                elementSelection.Groups.Add(newGroup);

                foreach (NodePropertyValue item in groups)
                {
                    ListViewItem newItem = new ListViewItem(item.PropertyName, newGroup);


                    if (bindingNode.DataSource is IList<TemplateNodeValue> nodes)
                    {
                        NodePropertyIndex nodeKey = new NodePropertyIndex(item);
                        TemplateNodeValue? node = nodes.FirstOrDefault(w => nodeKey.Equals(new NodePropertyIndex(w)));

                        if (node is TemplateNodeValue)
                        { newItem.Checked = true; }
                        else { newItem.Checked = false; }
                    }

                    elementSelection.Items.Add(newItem);
                    nodeProperties.Add(newItem, item);
                }
            }
        }


        private void ElementSelection_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //TODO: Item Checked is firing as soon as the control becomes visible.
            // This is incorrectly removing all Nodes.


        }


        private void elementSelection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void ElementSelection_ItemCheck(object sender, ItemCheckEventArgs e)
        {   //Item_Checked and Item_Check occurs when user checks an item
            //The ItemChecked occurs when the control becomes visible for each item that is checked. 
            //Item_Checked DOES NOT fire when the control becomes visible.

            ListViewItem item = elementSelection.Items[e.Index];

            if (nodeProperties.ContainsKey(item))
            {
                NodePropertyValue element = nodeProperties[item];
                if (e.NewValue == CheckState.Checked)
                {
                    if (bindingNode.AddNew() is TemplateNodeValue newNode)
                    {
                        bindingNode.ResumeBinding();

                        newNode.PropertyScope = element.PropertyScope;
                        newNode.PropertyName = element.PropertyName;
                        newNode.NodeName = element.PropertyName;

                        TemplateNodeIndex key = new TemplateNodeIndex(newNode);

                        if (bindingNode.DataSource is IList<TemplateNodeValue> nodes
                        && nodes.FirstOrDefault(w => key.Equals(w)) is TemplateNodeValue node)
                        { bindingNode.Position = nodes.IndexOf(node); }

                        schemaNodeLayout.Enabled = true;

                    }
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    if (bindingNode.DataSource is IList<TemplateNodeValue> nodes
                        && nodes.FirstOrDefault(w => new NodePropertyIndex(nodeProperties[item]).Equals(new NodePropertyIndex(w))) is TemplateNodeValue node)
                    {
                        TemplateNodeIndex key = new TemplateNodeIndex(node);
                        bindingNode.RemoveAt(nodes.IndexOf(node));

                        schemaNodeLayout.Enabled = false;
                        bindingNode.SuspendBinding();

                        bindingAttribute.DataSource = null;

                        while (BusinessData.ScriptingEngine.Templates.Attributes.FirstOrDefault(w => key.Equals(w)) is TemplateAttributeValue attribute)
                        { BusinessData.ScriptingEngine.Templates.Attributes.Remove(attribute); }
                    }
                }
            }
        }

        private void ElementSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (elementSelection.SelectedItems.Count > 0 && nodeProperties.ContainsKey(elementSelection.SelectedItems[0]))
            {
                NodePropertyIndex columnKey = new NodePropertyIndex(nodeProperties[elementSelection.SelectedItems[0]]);

                if (bindingNode.DataSource is IList<TemplateNodeValue> nodes && nodes.FirstOrDefault(w => columnKey.Equals(w)) is TemplateNodeValue node)
                {
                    schemaNodeLayout.Enabled = true;
                    bindingNode.ResumeBinding();
                    bindingNode.Position = nodes.IndexOf(node);
                }
                else
                {
                    schemaNodeLayout.Enabled = false;
                    bindingNode.SuspendBinding();
                    //bindingNode.Position = -1;
                }

            }





            /*
            if (bindingNode.DataSource is IList<ITemplateNodeValue> nodes
                && bindingTemplate.Current is ITemplateValue template)
            {
                if (elementSelection.SelectedItems.Count > 0 && nodeProperties.ContainsKey(elementSelection.SelectedItems[0]))
                {
                    NodePropertyIndex columnKey = new NodePropertyIndex(nodeProperties[elementSelection.SelectedItems[0]]);

                    attributeData.AutoGenerateColumns = false;
                    attributeData.DataSource = null;
                    bindingAttribute.DataSource = null;

                    TemplateIndex templateKey = new TemplateIndex(template);

                    if (nodes.FirstOrDefault(w => columnKey.Equals(new NodePropertyIndex(w))) is ITemplateNodeValue node)
                    {
                        TemplateNodeIndex nodeKey = new TemplateNodeIndex(node);
                        if (nodes.IndexOf(node) >= 0)
                        {
                            bindingNode.Position = nodes.IndexOf(node);
                            bindingNode.ResumeBinding();
                        }
                        else { bindingNode.SuspendBinding(); }

                        bindingAttribute.DataSource = new BindingView<TemplateAttributeValue>(BusinessData.ScriptingEngine.Templates.Attributes, w => nodeKey.Equals(w));
                    }
                    else
                    {
                        bindingAttribute.DataSource = new BindingView<TemplateAttributeValue>(BusinessData.ScriptingEngine.Templates.Attributes, w => 1 == 2);
                        bindingNode.SuspendBinding();
                    }

                    attributeData.DataSource = bindingAttribute;
                }
            }*/
        }

        private void BindingNode_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingTemplate.Current is ITemplateValue template)
            { e.NewObject = new TemplateNodeValue(template); }
        }

        private void BindingAttribute_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingNode.Current is ITemplateNodeValue node)
            { e.NewObject = new TemplateAttributeValue(node); }
        }

    }
}
