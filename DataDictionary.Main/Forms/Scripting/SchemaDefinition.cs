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

            formBinding = new FormBinding()
            {
                TemplateBinding = bindingTemplate,
                SchemaBinding = bindingSchema,
                ObjectBinding = bindingObject,
                DoWork = base.DoWork,
            };

            SetRowState(
                bindingSchema
                //bindingTemplate,
                //bindingObject
                );
            SetTitle(bindingSchema);
            SetIcon(ScopeType.ScriptingSchema);

            SetCommand(ScopeType.ScriptingSchema,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);

            openNodeCommand.Image = ScopeType.ScriptingNode.GetImage(ButtonType.Open);
            documentNewCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Add);
            documentOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
            openObjectCommand.Image = ScopeType.ScriptingObject.GetImage(ButtonType.Open);
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
        { formBinding.GetData = getData; }

        private void SchemaDefinition_Load(object sender, EventArgs e)
        {
            if (schemaIndex.HasValue)
            { formBinding.Load(schemaIndex); }
            else
            {
                if (templateIndex.HasValue
                    && formBinding.TryAddValue(templateIndex, out SchemaDefinitionValue? value))
                {
                    schemaIndex = new SchemaDefinitionIndex(value);
                    formBinding.Load(schemaIndex);
                    SendMessage(new RefreshNavigation());
                }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(templateIndex), templateIndex);
                    throw ex;
                }
            }

            if (formBinding.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                templateTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingTemplate, nameof(ITemplateValue.TemplateTitle)));
                schemaTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.SchemaTitle)));

                DirectoryTypeList.Load(rootFolderData);
                rootFolderData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingSchema,
                    nameof(ISchemaDefinitionValue.RootFolder),
                    true, DataSourceUpdateMode.OnValidation));

                relativePathData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.RelativePath)));
                filePrefixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.FilePrefix)));
                fileSuffixData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.FileSuffix)));
                fileExtensionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.FileExtension)));

                ScopeNameList.Load(objectScopeColumn);
                objectData.AutoGenerateColumns = false;
                objectData.DataSource = bindingObject;

                ScopeNameList.Load(forEachScopeData, ScopeType.Model, ScopeType.ModelAttribute, ScopeType.ModelEntity, ScopeType.ModelProcess);
                forEachScopeData.DataBindings.Add(new Binding(nameof(ComboBox.SelectedValue), bindingSchema, nameof(ISchemaDefinitionValue.ForEachScope)) { DataSourceNullValue = ScopeNameList.NullValue });
                rootNodeData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingSchema, nameof(ISchemaDefinitionValue.RootNodeName)));

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
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
            if (formBinding.TryGetValue(out SchemaDefinitionValue? value))
            {
                value.RelativePath = String.Empty;
                localPathData.Text = value.SchemaDirectory.InitialDirectory;
            }
            else { localPathData.Text = String.Empty; }
        }

        private void RelativePathData_Validated(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out SchemaDefinitionValue? value))
            { localPathData.Text = value.SchemaDirectory.InitialDirectory; }
            else { localPathData.Text = String.Empty; }
        }

        private void RelativePathData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TryGetValue(out SchemaDefinitionValue? current))
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

        private void OpenObjectCommand_Click(object sender, EventArgs e)
        {
            Activate(() => new Forms.Scripting.TemplateObject(
                template: templateIndex,
                getData: formBinding.GetData));
        }
    }
}
