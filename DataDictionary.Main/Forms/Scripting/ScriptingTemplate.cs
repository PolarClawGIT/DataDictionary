using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Forms.Domain.ComboBoxList;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class ScriptingTemplate : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTemplate.Current is ITemplateValue current && ReferenceEquals(current, item); }


        protected ScriptingTemplate() : base()
        {
            InitializeComponent();

            transformFilePath.Text = String.Empty;
            documentStatus.Text = String.Empty;
            pathAddCommand.Image = ImageEnumeration.GetImage(ScopeType.ScriptingTemplatePath, CommandImageType.Add);
            pathSelectCommand.Image = ImageEnumeration.GetImage(ScopeType.ScriptingTemplatePath, CommandImageType.Select);
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


            if (bindingTemplate.Current is ITemplateValue current)
            {
                Setup(bindingTemplate, CommandImageType.Delete);

                bindingPath.DataSource = new BindingView<TemplatePathValue>(BusinessData.ScriptingEngine.TemplatePaths, w => key.Equals(w));
                bindingNode.DataSource = new BindingView<TemplateNodeValue>(BusinessData.ScriptingEngine.TemplateNodes, w => key.Equals(w));
                bindingDocument.DataSource = new BindingView<TemplateDocumentValue>(BusinessData.ScriptingEngine.TemplateDocuments, w => key.Equals(w));

                bindingAttribute.DataSource = null;
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            if (bindingTemplate.Current is TemplateValue current)
            { DoWork(BusinessData.ScriptingEngine.Delete(current)); }
        }

        private void ScriptingTemplate_Load(object sender, EventArgs e)
        {
            ITemplateValue nameOfValues;
            ITemplateNodeValue nameOfNode;
            ITemplateDocumentValue nameOfDocument;

            this.DataBindings.Add(new Binding(nameof(this.Text), bindingTemplate, nameof(nameOfValues.TemplateTitle)));
            templateTitleData.DataBindings.Add(new Binding(nameof(templateTitleData.Text), bindingTemplate, nameof(nameOfValues.TemplateTitle)));
            templateDescriptionData.DataBindings.Add(new Binding(nameof(templateDescriptionData.Text), bindingTemplate, nameof(nameOfValues.TemplateDescription), false, DataSourceUpdateMode.OnPropertyChanged));

            rootDirectoryData.ValueMember = nameof(TemplateDirectoryEnumeration.Value);
            rootDirectoryData.DisplayMember = nameof(TemplateDirectoryEnumeration.DisplayName);
            rootDirectoryData.DataSource = TemplateDirectoryEnumeration.Members.Values.ToList();
            rootDirectoryData.DataBindings.Add(new Binding(
                nameof(rootDirectoryData.SelectedValue),
                bindingTemplate, nameof(nameOfValues.RootDirectory),
                false, DataSourceUpdateMode.OnPropertyChanged)
            { DataSourceNullValue = TemplateDirectoryType.Null });

            ScopeNameList.Load(breakOnScopeData);
            breakOnScopeData.DataBindings.Add(new Binding(nameof(breakOnScopeData.SelectedValue), bindingTemplate, nameof(nameOfValues.BreakOnScope), false, DataSourceUpdateMode.OnPropertyChanged, ScopeNameList.NullValue));

            documentDirectoryData.DataBindings.Add(new Binding(nameof(documentDirectoryData.Text), bindingTemplate, nameof(nameOfValues.DocumentDirectory), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentPrefixData.DataBindings.Add(new Binding(nameof(documentPrefixData.Text), bindingTemplate, nameof(nameOfValues.DocumentPrefix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentSuffixData.DataBindings.Add(new Binding(nameof(documentSuffixData.Text), bindingTemplate, nameof(nameOfValues.DocumentSuffix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentExtensionData.DataBindings.Add(new Binding(nameof(documentExtensionData.Text), bindingTemplate, nameof(nameOfValues.DocumentExtension), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            scriptAsData.ValueMember = nameof(TemplateScriptAsEnumeration.Value);
            scriptAsData.DisplayMember = nameof(TemplateScriptAsEnumeration.DisplayName);
            scriptAsData.DataSource = TemplateScriptAsEnumeration.Members.Values.ToList();
            scriptAsData.DataBindings.Add(new Binding(
                nameof(scriptAsData.SelectedValue),
                bindingTemplate, nameof(nameOfValues.ScriptAs),
                false, DataSourceUpdateMode.OnPropertyChanged)
            { DataSourceNullValue = TemplateScriptAsType.none });

            scriptingDirectoryData.DataBindings.Add(new Binding(nameof(scriptingDirectoryData.Text), bindingTemplate, nameof(nameOfValues.ScriptDirectory), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            scriptingPrefixData.DataBindings.Add(new Binding(nameof(scriptingPrefixData.Text), bindingTemplate, nameof(nameOfValues.ScriptPrefix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            scriptingSuffixData.DataBindings.Add(new Binding(nameof(scriptingSuffixData.Text), bindingTemplate, nameof(nameOfValues.ScriptSuffix), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            scriptingExtensionData.DataBindings.Add(new Binding(nameof(scriptingExtensionData.Text), bindingTemplate, nameof(nameOfValues.ScriptExtension), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            transformScriptData.DataBindings.Add(new Binding(nameof(transformScriptData.Text), bindingTemplate, nameof(nameOfValues.TransformScript), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            transformExceptionData.DataBindings.Add(new Binding(nameof(transformExceptionData.Text), bindingTemplate, nameof(nameOfValues.TransformException), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            ScopeNameList.Load(propertyScopeData);
            propertyScopeData.DataBindings.Add(new Binding(nameof(propertyScopeData.SelectedValue), bindingNode, nameof(nameOfNode.PropertyScope), false, DataSourceUpdateMode.OnPropertyChanged, ScopeNameList.NullValue));
            propertyNameData.DataBindings.Add(new Binding(nameof(propertyNameData.Text), bindingNode, nameof(nameOfNode.PropertyName), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            nodeNameData.DataBindings.Add(new Binding(nameof(nodeNameData.Text), bindingNode, nameof(nameOfNode.NodeName), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            nodeValueAsData.ValueMember = nameof(TemplateNodeValueAsEnumeration.Value);
            nodeValueAsData.DisplayMember = nameof(TemplateNodeValueAsEnumeration.DisplayName);
            nodeValueAsData.DataSource = TemplateNodeValueAsEnumeration.Members.Values.ToList();
            nodeValueAsData.DataBindings.Add(new Binding(
                nameof(nodeValueAsData.SelectedValue),
                bindingNode, nameof(nameOfNode.NodeValueAs),
                false, DataSourceUpdateMode.OnPropertyChanged)
            { DataSourceNullValue = TemplateNodeValueAsType.none });


            PropertyNameList.Load(attributePropertyColumn);
            attributeData.AutoGenerateColumns = false;
            attributeData.DataSource = bindingAttribute;

            documentData.AutoGenerateColumns = false;
            documentData.DataSource = bindingDocument;

            documentXMLData.DataBindings.Add(new Binding(nameof(documentXMLData.Text), bindingDocument, nameof(nameOfDocument.SourceAsText), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentScriptData.DataBindings.Add(new Binding(nameof(documentScriptData.Text), bindingDocument, nameof(nameOfDocument.ResultsAsText), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));
            documentException.DataBindings.Add(new Binding(nameof(documentException.Text), bindingDocument, nameof(nameOfDocument.ExceptionAsText), false, DataSourceUpdateMode.OnPropertyChanged, String.Empty));

            // Path Handling
            ScopeNameList.Load(pathScopeColumn);
            ScopeNameList.Load(pathScopeData);

            pathsData.AutoGenerateColumns = false;
            pathsData.DataSource = bindingPath;

            pathScopeData.DataBindings.Add(new Binding(nameof(pathScopeData.SelectedValue), bindingPath, nameof(TemplatePathValue.PathScope), false, DataSourceUpdateMode.OnPropertyChanged) { DataSourceNullValue = ScopeNameList.NullValue });
            pathNameData.DataBindings.Add(new Binding(nameof(pathNameData.Text), bindingPath, nameof(TemplatePathValue.PathName), false, DataSourceUpdateMode.OnPropertyChanged));

            ElementSelection_Load();

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingTemplate.Current is not ITemplateValue);
        }

        private void RootDirectoryData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rootDirectoryData.SelectedValue is TemplateDirectoryType value
                && TemplateDirectoryEnumeration.Cast(value).Directory is DirectoryInfo directory)
            { rootDirectoryExpanded.Text = directory.FullName; }
            else { rootDirectoryExpanded.Text = String.Empty; }
        }

        private void RootDirectoryData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (rootDirectoryData.SelectedValue is TemplateDirectoryType value
                && bindingTemplate.Current is TemplateValue current)
            {
                current.RootDirectory = value; // TODO: Some reason Binding is not setting the value.
                current.DocumentDirectory = null;
                current.ScriptDirectory = null;
            }
            else { rootDirectoryExpanded.Text = String.Empty; }
        }


        private void DocumentDirectoryPicker_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current
                && TemplateDirectoryEnumeration.Cast(current.RootDirectory).Directory is DirectoryInfo directory)
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
                && TemplateDirectoryEnumeration.Cast(current.RootDirectory).Directory is DirectoryInfo directory)
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
        { }

        private void ScriptAsData_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current
                && scriptAsData.SelectedValue is TemplateScriptAsType value)
            {
                current.ScriptAs = value;// TODO: Some reason Binding is not setting the value.
                current.ScriptExtension = TemplateScriptAsEnumeration.Members[value].Extension;
            }
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
                if (TemplateDirectoryEnumeration.Cast(current.RootDirectory).Directory is DirectoryInfo directory)
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
                if (TemplateDirectoryEnumeration.Cast(current.RootDirectory).Directory is DirectoryInfo directory)
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


        private void BindingPath_AddingNew(object sender, AddingNewEventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            {
                TemplatePathValue newItem = new TemplatePathValue(current);
                e.NewObject = newItem;
            }
        }

        private void BindingPath_CurrentChanged(object sender, EventArgs e)
        {
            if (bindingPath.Current is TemplatePathValue current)
            {
                Boolean inModel = BusinessData.NamedScope.PathKeys(current.Path).Count > 0;
                isPathInModelData.Checked = inModel;
                pathNameData.ReadOnly = inModel;
                pathScopeData.ReadOnly = inModel;
            }
        }


        Dictionary<ListViewItem, NodePropertyValue> nodeProperties = new Dictionary<ListViewItem, NodePropertyValue>();
        private void ElementSelection_Load()
        {
            elementSelection.Groups.Clear();
            elementSelection.Items.Clear();
            nodeProperties.Clear();
            schemaNodeLayout.Enabled = false;

            foreach (var groups in BusinessData.ScriptingEngine.Properties.GroupBy(g => g.PropertyScope))
            {
                ListViewGroup newGroup = new ListViewGroup(ImageEnumeration.Cast(groups.Key).Name);
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

        Boolean nodesSelectReady = false; // Used to prevent ItemCheck from triggering an add or remove when not issued by the user.
        private void ElementSelection_VisibleChanged(object sender, EventArgs e)
        {   //VisibleChange does not fire when the control becomes hidden.
            // The event will fire again when the control next becomes visible.
            // The event fires AFTER the first calls to ItemChecked & ItemCheck.
            nodesSelectReady = elementSelection.Visible;
        }

        private void ElementSelection_ItemChecked(object sender, ItemCheckedEventArgs e)
        { } // Not used but here for debugging event order. 

        private void ElementSelection_ItemCheck(object sender, ItemCheckEventArgs e)
        {   // The order of events is not what is expected.
            // No interaction by the user has occurred except to make the control visible.
            //
            // When the form is loaded and the control is not visible, nothing happens.
            // When the control becomes visible:
            //   ItemChecked fires for every item in the list (all items start as unchecked).
            //     This will set the Check-box to the setting specified. In this form, during ElementSelection_Load.
            //   ItemCheck fires for every item that the check state changes (this case to Checked).
            //     During this process, the control is "Visible" but the VisibleChanged event has not occurred.
            //   VisibleChange event fires.
            // The order changes once the control is visible and ready for use.
            //   ItemCheck fires when the check-box is clicked (selection does not fire)
            //   ItemChecked fires (selection does not fire)

            ListViewItem item = elementSelection.Items[e.Index];

            if (nodesSelectReady
                && nodeProperties.ContainsKey(item)
                && bindingTemplate.Current is TemplateValue template)
            {
                NodePropertyValue element = nodeProperties[item];
                if (e.NewValue == CheckState.Checked)
                {
                    // Duplicate check. Here just in case something unexpected happens.
                    TemplateNodeIndexName dupKey = new TemplateNodeIndexName(element);
                    TemplateIndex dupTemplate = new TemplateIndex(template);
                    if (bindingNode.DataSource is IList<TemplateNodeValue> nodesDupCheck
                        && nodesDupCheck.FirstOrDefault(w => dupTemplate.Equals(w) && dupKey.Equals(w)) is TemplateNodeValue nodeDupCheck)
                    {
                        Exception ex = new InvalidOperationException("Duplicate");
                        ex.Data.Add(nameof(template.TemplateTitle), template.TemplateTitle);
                        ex.Data.Add(nameof(element.PropertyName), element.PropertyName);
                        ex.Data.Add(nameof(element.PropertyScope), ImageEnumeration.Cast(element.PropertyScope).Name);
                        throw ex;
                    }

                    if (bindingNode.AddNew() is TemplateNodeValue newNode)
                    {
                        TemplateNodeIndex key = new TemplateNodeIndex(newNode);

                        newNode.PropertyScope = element.PropertyScope;
                        newNode.PropertyName = element.PropertyName;
                        newNode.NodeName = element.PropertyName;
                        newNode.NodeValueAs = TemplateNodeValueAsType.ElementText;
                        bindingNode.ResumeBinding();

                        if (bindingNode.DataSource is IList<TemplateNodeValue> nodes
                        && nodes.FirstOrDefault(w => key.Equals(w)) is TemplateNodeValue node)
                        { bindingNode.Position = nodes.IndexOf(node); }

                        attributeData.DataSource = null;
                        bindingAttribute.DataSource = null;
                        bindingAttribute.DataSource = new BindingView<TemplateAttributeValue>(BusinessData.ScriptingEngine.TemplateAttributes, w => key.Equals(w));
                        attributeData.DataSource = bindingAttribute;

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

                        bindingNode.SuspendBinding();

                        attributeData.DataSource = null;
                        bindingAttribute.DataSource = null;

                        while (BusinessData.ScriptingEngine.TemplateAttributes.FirstOrDefault(w => key.Equals(w)) is TemplateAttributeValue attribute)
                        { BusinessData.ScriptingEngine.TemplateAttributes.Remove(attribute); }

                        schemaNodeLayout.Enabled = false;
                    }
                }
            }
        }

        private void ElementSelection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        { } // Not used but here to debug event order. This occurs before SelectedIndexChanged.

        private void ElementSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (elementSelection.SelectedItems.Count > 0 && nodeProperties.ContainsKey(elementSelection.SelectedItems[0]))
            {
                NodePropertyIndex columnKey = new NodePropertyIndex(nodeProperties[elementSelection.SelectedItems[0]]);

                if (bindingNode.DataSource is IList<TemplateNodeValue> nodes && nodes.FirstOrDefault(w => columnKey.Equals(w)) is TemplateNodeValue node)
                {
                    TemplateNodeIndex key = new TemplateNodeIndex(node);
                    schemaNodeLayout.Enabled = true;
                    bindingNode.ResumeBinding();
                    bindingNode.Position = nodes.IndexOf(node);

                    attributeData.DataSource = null;
                    bindingAttribute.DataSource = null;
                    bindingAttribute.DataSource = new BindingView<TemplateAttributeValue>(BusinessData.ScriptingEngine.TemplateAttributes, w => key.Equals(w));
                    attributeData.DataSource = bindingAttribute;
                }
                else
                {
                    schemaNodeLayout.Enabled = false;
                    attributeData.DataSource = null;
                    bindingAttribute.DataSource = null;
                    bindingNode.SuspendBinding();
                }
            }
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

        private void DocumentBuildComand_Click(object sender, EventArgs e)
        {
            if (bindingTemplate.Current is TemplateValue current)
            {
                TemplateIndex key = new TemplateIndex(current);
                bindingDocument.SuspendBinding();
                documentData.DataSource = null;
                //bindingDocument.DataSource = null;
                BusinessData.ScriptingEngine.TemplateDocuments.Remove(current);
                documentStatus.Text = "working";

                DoWork(BusinessData.BuildDocuments(current), onComplete);

                void onComplete(RunWorkerCompletedEventArgs args)
                {
                    if (args.Error is null)
                    {
                        bindingDocument.DataSource = new BindingView<TemplateDocumentValue>(BusinessData.ScriptingEngine.TemplateDocuments, w => key.Equals(w));
                        documentData.DataSource = bindingDocument;
                        bindingDocument.ResumeBinding();
                    }

                    if (args.Error is null) { documentStatus.Text = "Build Complete"; }
                    else { documentStatus.Text = args.Error.Message; }
                }
            }
        }

        private void DocumentSaveXMLCommand_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            documentStatus.Text = "working";

            foreach (DataGridViewRow item in documentData.SelectedRows)
            {
                if (item.DataBoundItem is TemplateDocumentValue doc)
                { work.AddRange(doc.SaveSource()); }

            }
            this.DoWork(work, OnSaveComplete);
        }

        private void DocumentSaveScriptCommand_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            documentStatus.Text = "working";

            foreach (DataGridViewRow item in documentData.SelectedRows)
            {
                if (item.DataBoundItem is TemplateDocumentValue doc)
                { work.AddRange(doc.SaveResult()); }

            }
            this.DoWork(work, OnSaveComplete);
        }

        private void DocumentSaveAllCommand_Click(object sender, EventArgs e)
        {
            List<WorkItem> work = new List<WorkItem>();
            if (bindingDocument.DataSource is IEnumerable<TemplateDocumentValue> items)
            {
                documentStatus.Text = "working";

                foreach (var doc in items)
                {
                    work.AddRange(doc.SaveSource());
                    work.AddRange(doc.SaveResult());
                }
            }
            this.DoWork(work, OnSaveComplete);
        }

        private void OnSaveComplete(RunWorkerCompletedEventArgs args)
        {
            if (args.Error is null) { documentStatus.Text = "Save Complete"; }
            else { documentStatus.Text = args.Error.Message; }
        }

        private void DocumentData_SelectionChanged(object sender, EventArgs e)
        {
            documentStatus.Text = String.Format("{0} selected", documentData.SelectedRows.Count);
        }

        private void PathSelectCommand_Click(object sender, EventArgs e)
        {
            if (bindingPath.DataSource is IList<TemplatePathValue> alias)
            {
                using (var dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.Add(ScopeType.ModelAttribute);
                    // TODO: Add more when they are supported
                    // dialog.FilterScopes.Add(ScopeType.ModelEntity);

                    dialog.BuildData(alias.SelectMany(s => BusinessData.NamedScope.PathKeys(s.Path)));


                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        IEnumerable<INamedScopeValue> selected = dialog.SelectedByNamedScope();
                        IEnumerable<TemplatePathValue> inModel = alias.Where(w => BusinessData.NamedScope.PathKeys(w.Path).Count() > 0);

                        foreach (TemplatePathValue removeItem in alias.Where(w => !selected.Select(s => s.Path).Contains(w.Path)).ToList())
                        {
                            if (inModel.Contains(removeItem)) // Only remove items that are in this model
                            { alias.Remove(removeItem); }
                        }

                        foreach (INamedScopeValue addItem in selected.Where(w => !alias.Select(s => s.Path).Contains(w.Path)).ToList())
                        { // Add
                            if (bindingPath.AddNew() is TemplatePathValue newValue)
                            {
                                newValue.Path = addItem.Path;
                                newValue.PathScope = addItem.Scope;
                            }
                        }
                    }
                }
            }
        }

        private void PathAddCommand_Click(object sender, EventArgs e)
        {
            if (bindingPath.AddNew() is TemplatePathValue newValue)
            { }
        }

        private void pathNameData_Validating(object sender, CancelEventArgs e)
        {
            PathIndex path = new PathIndex(PathIndex.Parse(pathNameData.Text).ToArray());
            pathNameData.Text = path.MemberFullPath;
        }
    }
}
