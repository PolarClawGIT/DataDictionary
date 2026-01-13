using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Template : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return templateIndex.Equals(item); }

        FormBinding formBinding;
        TemplateIndex templateIndex = new TemplateIndex();
        TemporalIndex? temporalIndex = null;

        public Template() : base()
        {
            InitializeComponent();
            newDataSourceCommand.Image = ScopeType.ScriptingData.GetImage(CommandType.Default);

            documentCommand.Image = ScopeType.ScriptingDocument.GetImage(CommandType.Default);
            transformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Default);
            addNodeCommand.Image = ScopeType.ScriptingTemplateNode.GetImage(CommandType.Add);
            deleteNodeCommand.Image = ScopeType.ScriptingTemplateNode.GetImage(CommandType.Delete);
            addNodeParentCommand.Image = ScopeType.ScriptingTemplateNodeOwner.GetImage(CommandType.Add);
            nodeDetailLayout.Enabled = false;

            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                NodeBinding = bindingNode,
                NodeOwnerBinding = bindingNodeOwner,
                DataSourceBinding = bindingTemplateData,
                DoWork = base.DoWork
            };

            SetTitle(bindingTemplate);
            SetRowState(bindingTemplate);

            SetCommand(ScopeType.ScriptingTemplate,
                Enumerations.CommandType.Delete,
                Enumerations.CommandType.OpenDatabase,
                Enumerations.CommandType.SaveDatabase,
                Enumerations.CommandType.DeleteDatabase,
                Enumerations.CommandType.HistoryDatabase);
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

                DirectoryTypeList.Load(rootFolderData);
                rootFolderData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingTemplate,
                    nameof(ITemplateValue.RootFolder),
                    true, DataSourceUpdateMode.OnValidation));

                ScopeNameList.Load(breakOnScopeData, formBinding.XBuilder.Keys);
                breakOnScopeData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingTemplate,
                    nameof(ITemplateValue.TemplateBreakOn),
                    true, DataSourceUpdateMode.OnValidation));

                documentDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentDirectory), false, DataSourceUpdateMode.OnValidation, String.Empty));
                documentPrefixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentPrefix), false, DataSourceUpdateMode.OnValidation, String.Empty));
                documentSuffixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentSuffix), true, DataSourceUpdateMode.OnValidation, String.Empty));
                documentExtensionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.DocumentExtension), false, DataSourceUpdateMode.OnValidation, String.Empty));

                scriptingDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptDirectory), false, DataSourceUpdateMode.OnValidation, String.Empty));
                scriptingPrefixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptPrefix), false, DataSourceUpdateMode.OnValidation, String.Empty));
                scriptingSuffixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptSuffix), false, DataSourceUpdateMode.OnValidation, String.Empty));
                scriptingExtensionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.ScriptExtension), false, DataSourceUpdateMode.OnValidation, String.Empty));

                nodeNameData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.NodeName), false, DataSourceUpdateMode.OnValidation, String.Empty)); // TODO: Change to On Property Changed

                RenderValueAsList.Load(nodeRenderAsData);
                nodeRenderAsData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ITemplateNodeValue.RenderValueAs)));

                nodeRenderOrderData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.NodeOrder), true, DataSourceUpdateMode.OnValidation, 0));
                nodeFixedValueData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingNode, nameof(ITemplateNodeValue.FixedValue)));

                XScopeList.Load(nodeObjectScopeData, nodeObjectPropertyData, formBinding.XBuilder, "(n/a)");
                nodeObjectScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ITemplateNodeValue.ObjectScope), true, DataSourceUpdateMode.OnValidation, ScopeNameList.NullValue));
                nodeObjectPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ITemplateNodeValue.ObjectProperty)));

                PropertyNameList.Load(nodeModelPropertyData, "(n/a)");
                nodeModelPropertyData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingNode, nameof(ITemplateNodeValue.ModelPropertyId), true, DataSourceUpdateMode.OnValidation, Guid.Empty));

                TemplateNodeList.Load(nodeParentColumn, templateIndex);
                nodeOwnershipData.AutoGenerateColumns = false;
                nodeOwnershipData.DataSource = bindingNodeOwner;
                nodeParentColumn.DataPropertyName = nameof(ITemplateNodeOwnerValue.NodeOwnerId);

                TemplateNodeList.Load(nodeParentSelect, templateIndex, "(n/a)");

                formBinding.BuildTree(nodeTreeView);
            }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            formBinding.RemoveValue();
            formBinding.BuildTree(nodeTreeView);
            SetAuthorization(formBinding.GetAuthorization);
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(templateIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(templateIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.RemoveValue();
            formBinding.Save(templateIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal(templateIndex))
            {
                OpenForm = (temporal) =>
                {
                    if (temporal.TryGetValue(out TemplateValue? template))
                    { return new Template(template, new TemporalIndex(temporal)); }
                    else { throw new InvalidOperationException("Could not convert TemporalValue back to AttributeValue"); }
                }
            });
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


        private void RootFolderData_Validated(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out TemplateValue? current))
            {
                if (current.RootFolder.GetEnumeration().Directory is DirectoryInfo directory)
                { rootPhysicalDirectory.Text = directory.FullName; }
                else { rootPhysicalDirectory.Text = String.Empty; }

                current.DocumentDirectory = String.Empty;
                current.ScriptDirectory = String.Empty;
            }
        }

        private void BindingTemplateData_AddingNew(object sender, AddingNewEventArgs e)
        { e.NewObject = formBinding.NewDataSource(); }

        private void BindingNode_ListChanged(object sender, ListChangedEventArgs e)
        { formBinding.BuildTree(nodeTreeView); }

        private void BindingNodeOwner_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType is ListChangedType.ItemAdded or
                ListChangedType.ItemDeleted or
                ListChangedType.ItemChanged)
            { formBinding.BuildTree(nodeTreeView); }
        }

        private void NodeTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node is not null
            && e.Node.TreeView is not null
            && e.Node.TreeView.HitTest(e.Location).Location != TreeViewHitTestLocations.PlusMinus
            && e.Node.TryGetValue(out TemplateNodeValue? value))
            {
                if (formBinding.TrySetPosition(value))
                { nodeDetailLayout.Enabled = true; }
                else
                { nodeDetailLayout.Enabled = false; }
            }
        }

        private void AddNodeCommand_Click(object sender, EventArgs e)
        {
            ITemplateNodeIndex value = formBinding.NewNodeValue(templateIndex);
            formBinding.TrySetPosition(value);

            if (formBinding.TrySetPosition(value))
            { nodeDetailLayout.Enabled = true; }
            else
            { nodeDetailLayout.Enabled = false; }
        }

        private void DeleteNodeCommand_Click(object sender, EventArgs e)
        {
            formBinding.RemoveNodeValue();
            formBinding.BuildTree(nodeTreeView);
            nodeDetailLayout.Enabled = false;
        }

        private void AddNodeParentCommand_Click(object sender, EventArgs e)
        {
            if (nodeParentSelect.SelectedItem is TemplateNodeList selected)
            {
                TemplateNodeIndex key = new TemplateNodeIndex(selected);
                formBinding.NewNodeOwner(key);
            }
        }


    }
}
