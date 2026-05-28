using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;

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

            formBinding = new FormBinding(bindingTemplate, bindingSchema);

            SetRowState(
                bindingSchema
                //bindingTemplate,
                //bindingObject
                );
            SetTitle(bindingSchema);
            SetIcon(ScopeType.ScriptingSchema);

            SetCommand(ButtonType.Delete);
            AddCommands(nodeMenu);

            openNodeCommand.Image = ScopeType.ScriptingNode.GetImage(ButtonType.Open);
            openNodeCommand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            documentNewCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Add);
            documentOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
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
            else
            {
                if (templateIndex.HasValue)
                {
                    SchemaDefinitionValue value = new SchemaDefinitionValue(templateIndex);
                    formBinding.SchemaData.Add(value);
                    schemaIndex = new SchemaDefinitionIndex(value);
                    formBinding.LoadValue(schemaIndex);
                    SendMessage(new RefreshNavigation());
                }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(templateIndex), templateIndex);
                    throw ex;
                }
            }

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, nameof(ITemplateValue.TemplateTitle));
                formBinding.SchemaData.AddBinding(schemaTitleData, nameof(ISchemaDefinitionValue.SchemaTitle));

                DirectoryTypeList.Load(rootFolderData);
                formBinding.SchemaData.AddBinding(rootFolderData, nameof(ISchemaDefinitionValue.RootFolder), DirectoryTypeList.NullValue);

                formBinding.SchemaData.AddBinding(relativePathData, nameof(ISchemaDefinitionValue.RelativePath));
                formBinding.SchemaData.AddBinding(filePrefixData, nameof(ISchemaDefinitionValue.FilePrefix));
                formBinding.SchemaData.AddBinding(fileSuffixData, nameof(ISchemaDefinitionValue.FileSuffix));
                formBinding.SchemaData.AddBinding(fileExtensionData, nameof(ISchemaDefinitionValue.FileExtension));



                ScopeNameList.Load(forEachScopeData, ScopeType.Model, ScopeType.ModelAttribute, ScopeType.ModelEntity, ScopeType.ModelProcess);
                formBinding.SchemaData.AddBinding(forEachScopeData, nameof(ISchemaDefinitionValue.ForEachScope), ScopeNameList.NullValue);
                formBinding.SchemaData.AddBinding(rootNodeData, nameof(ISchemaDefinitionValue.RootNodeName));

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

        private void DocumentNewCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.SchemaDocument());
        }

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.SchemaDocument());
        }

        private void OpenNodeCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.SchemaNode(
                template: templateIndex, schema: schemaIndex,
                getData: formBinding.GetData));
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
    }
}
