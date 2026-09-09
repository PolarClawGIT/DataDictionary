using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDefinition : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex();
        //TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ISchemaDefinitionIndex key && schemaIndex.Equals(key); }

        public SchemaDefinition() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingSchema, bindingNode, bindingDocument);
            nodesTree = new TreeBinding(schemaNodeTree);

            SetRowState(bindingSchema);
            SetTitle(bindingSchema);
            SetIcon(bindingSchema);

            SetCommand(ButtonType.Delete);

            documentBuildCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Export);
            documentNewCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Add);
            documentOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
            documentDeleteCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Delete);

            nodeNewCommand.Image = ScopeType.ScriptingNode.GetImage(ButtonType.Add);
            nodeDeleteCommand.Image = ScopeType.ScriptingNode.GetImage(ButtonType.Delete);
        }

        public SchemaDefinition(ITemplateIndex template, ISchemaDefinitionIndex? schema) : this()
        {
            templateIndex = new TemplateIndex(template);

            if (schema is ISchemaDefinitionIndex key)
            { schemaIndex = new SchemaDefinitionIndex(key); }
        }

        public SchemaDefinition(ISchemaComposite schema) : this(schema, schema)
        { }

        public SchemaDefinition(
            ITemplateIndex template,
            ISchemaDefinitionIndex? schema,
            Func<ITemplateData> getData) : this(template, schema)
        {
            formBinding.TemplateData.GetData = () => getData();
            formBinding.SchemaData.GetData = () => getData().Schemata;
        }

        private void SchemaDefinition_Load(object sender, EventArgs e)
        {
            if (schemaIndex.HasValue)
            { formBinding.LoadValue(schemaIndex); }
            else if (templateIndex.HasValue)
            {
                formBinding.LoadValue(templateIndex, out schemaIndex);
                SendMessage(new RefreshNavigation());
            }
            else
            {   // This should never occur.
                Exception ex = new InvalidOperationException("Template not found");
                ex.Data.Add(nameof(templateIndex), templateIndex);
                throw ex;
            }

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                // Main Tab
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.SchemaData.AddBinding(schemaTitleData, e => e.SchemaTitle);

                DirectoryTypeList.Load(rootFolderData);
                formBinding.SchemaData.AddBinding(rootFolderData, e => e.RootFolder, DirectoryTypeList.NullValue);

                formBinding.SchemaData.AddBinding(relativePathData, e => e.RelativePath);
                formBinding.SchemaData.AddBinding(filePrefixData, e => e.FilePrefix);
                formBinding.SchemaData.AddBinding(fileSuffixData, e => e.FileSuffix);
                formBinding.SchemaData.AddBinding(fileExtensionData, e => e.FileExtension);

                // Node Tab
                nodesTree.LoadTree(formBinding.BuilderData);

                formBinding.BuilderData.AddBinding(nodeNameData, e => e.NodeName);
                formBinding.BuilderData.AddBinding(isOverrideData, e => e.IsOverride);

                ScopeNameList.Load(objectScopeData, ScopeType.Null,
                    ScopeType.ModelAttribute, ScopeType.ModelAttributeProperty,
                    ScopeType.ModelEntity, ScopeType.ModelEntityProperty);
                formBinding.BuilderData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);

                formBinding.BuilderData.AddBinding(objectPropertyData, e => e.ObjectProperty);

                ObjectValueTypeList.Load(objectTypeData);
                formBinding.BuilderData.AddBinding(objectTypeData, e => e.ObjectType, ObjectValueTypeList.NullValue);

                XmlNodeTypeList.Load(renderNodeTypeData);
                formBinding.BuilderData.AddBinding(renderNodeTypeData, e => e.RenderNodeType, XmlNodeTypeList.NullValue);

                XmlTypeCodeList.Load(renderTypeCodeData);
                formBinding.BuilderData.AddBinding(renderTypeCodeData, e => e.RenderTypeCode, XmlTypeCodeList.NullValue);

                formBinding.BuilderData.AddBinding(renderOrderData, e => e.RenderOrder);

                // Document Tab   
                documentOpenCommand.Enabled = false;
                documentDeleteCommand.Enabled = false;
                formBinding.DocumentData.AddBinding(documentData);

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
            throw new NotImplementedException();
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

        private void DocumentBuildCommand_Click(object sender, EventArgs e)
        {
            using (SelectionDialog dialog = new SelectionDialog(this))
            {
                dialog.MultiSelect = true;
                dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());
                dialog.BuildData(formBinding.DocumentData.Select(s => new PathIndex(s.ObjectPath)));

                if (dialog.ShowDialog(this) is DialogResult.OK)
                {
                    // Add missing
                    foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                    {
                        if(formBinding.SchemaData.TryGetSingle(out SchemaDefinitionValue? schemaValue)
                            && !formBinding.DocumentData.Any(w => item.Path.Equals(new PathIndex(w.ObjectPath)) && item.Scope == w.ObjectScope))
                        {
                            var newDocument = new SchemaDocumentValue(templateIndex, schemaIndex);
                            newDocument.ObjectScope = item.Scope;
                            newDocument.ObjectPath = item.Path.MemberFullPath;
                            newDocument.FileName = String.Concat(schemaValue.FilePrefix, item.Path.Member, schemaValue.FileSuffix, ".", schemaValue.FileExtension);

                            formBinding.DocumentData.Add(newDocument);
                        }
                    }

                    // Build XML
                    formBinding.BuildDocuments();
                }
            }
        }

        private void DocumentNewCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.SchemaDocument(schemaIndex, formBinding.GetData)); }

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? value))
            {
                DocumentIndex key = new DocumentIndex(value);
                Activate(() => new Forms.Scripting.SchemaDocument(key, formBinding.GetData), o => o.IsOpenItem(key));
            }
        }

        private void DocumentDeleteCommand_Click(object sender, EventArgs e)
        {
            formBinding.DocumentData.Remove();
        }

        private void BindingDocument_CurrentChanged(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? value))
            {
                documentOpenCommand.Enabled = true;
                documentDeleteCommand.Enabled = true;
            }
            else
            {
                documentOpenCommand.Enabled = false;
                documentDeleteCommand.Enabled = false;
            }
        }

        private void RootFolderData_Validated(object sender, EventArgs e)
        {
            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? value))
            {
                value.RelativePath = String.Empty;
                localPathData.Text = value.SchemaDirectory.InitialDirectory;
            }
            else { localPathData.Text = String.Empty; }
        }

        private void RelativePathData_Validated(object sender, EventArgs e)
        {
            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? value))
            { localPathData.Text = value.SchemaDirectory.InitialDirectory; }
            else { localPathData.Text = String.Empty; }
        }

        private void RelativePathData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? current))
            {
                folderBrowserDialog.Reset();
                folderBrowserDialog.RootFolder = current.SchemaDirectory.RootFolder;
                folderBrowserDialog.InitialDirectory = current.SchemaDirectory.InitialDirectory;

                if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
                {
                    current.SchemaDirectory.InitialDirectory = folderBrowserDialog.SelectedPath;
                    localPathData.Text = current.SchemaDirectory.InitialDirectory;
                }
            }
        }

        protected override void HandleMessage(RefreshRow message)
        {
            base.HandleMessage(message);

            if (message is RefreshRow<TemplateIndex> rowMessage
                && rowMessage.Key.Equals(templateIndex))
            {
                formBinding.LoadValue(schemaIndex);
                SendMessage(new RefreshRow<SchemaDefinitionIndex>(schemaIndex));
            }
        }

        private void NodeNewCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.BuilderData.TryGetValue(out XmlBuilderNode? value)
                && value.SchemaNode is null)
            {
                SchemaNodeValue node = new SchemaNodeValue(templateIndex, schemaIndex);
                value.SchemaNode = node;
                OnNodeChanged();
            }
        }

        private void NodeDeleteCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.BuilderData.TryGetValue(out XmlBuilderNode? value)
                && value.SchemaNode is not null)
            {
                value.SchemaNode = null;
                OnNodeChanged();
            }
        }

        private void BindingNode_CurrentChanged(object sender, EventArgs e)
        { OnNodeChanged(); }

        void OnNodeChanged()
        {
            if (formBinding.BuilderData.TryGetValue(out XmlBuilderNode? value))
            {
                nodeNameData.Enabled = value.SchemaNode is not null;
                nodeRenderGroup.Enabled = value.SchemaNode is not null;
                nodeNewCommand.Enabled = value.SchemaNode is null;
                nodeDeleteCommand.Enabled = value.SchemaNode is not null;
            }
            else
            {   // Should not occur. No selected Node.
                nodeNameData.Enabled = false;
                nodeRenderGroup.Enabled = false;
                nodeNewCommand.Enabled = false;
                nodeDeleteCommand.Enabled = false;
            }
        }


    }
}
