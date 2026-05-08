using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Transform : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        TransformIndex transformIndex = new TransformIndex();
        //TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public Transform() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingTransform);

            SetRowState(
                bindingTransform);
            SetTitle(bindingTransform);
            SetIcon(ScopeType.ScriptingTransform);

            SetCommand(ScopeType.ScriptingTransform,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);

            scriptOpenCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
            scriptSaveCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Save);
            documentNewCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Add);
            documentOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
        }

        public Transform(ITemplateIndex template, ITransformIndex? transform) : this()
        {
            templateIndex = new TemplateIndex(template);

            if (transform is ITransformIndex key)
            { transformIndex = new TransformIndex(key); }
        }

        public Transform(
            ITemplateIndex template,
            ITransformIndex? transform,
            Func<ITemplateData> getData) : this(template, transform)
        { formBinding.GetData = getData; }

        public Transform(ITransformComposite transform) : this(transform, transform)
        { }

        private void Transform_Load(object sender, EventArgs e)
        {
            if (transformIndex.HasValue)
            { formBinding.Load(transformIndex); }
            else
            {
                if (templateIndex.HasValue)
                {
                    TransformValue value = new TransformValue(templateIndex);
                    formBinding.TransformData.AddValue(value);
                    transformIndex = new TransformIndex(value);
                    formBinding.Load(transformIndex);
                    SendMessage(new RefreshNavigation());
                }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(templateIndex), templateIndex);
                    throw ex;
                }
            }

            if (formBinding.TransformData.TryGetValue(out TransformValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, nameof(ITemplateValue.TemplateTitle));
                formBinding.TransformData.AddBinding(transformTitleData, nameof(ITransformValue.TransformTitle));

                DirectoryTypeList.Load(rootFolderData);
                formBinding.TransformData.AddBinding(rootFolderData, nameof(ISchemaDefinitionValue.RootFolder));

                formBinding.TransformData.AddBinding(relativePathData, nameof(ISchemaDefinitionValue.RelativePath));
                formBinding.TransformData.AddBinding(filePrefixData, nameof(ISchemaDefinitionValue.FilePrefix));
                formBinding.TransformData.AddBinding(fileSuffixData, nameof(ISchemaDefinitionValue.FileSuffix));
                formBinding.TransformData.AddBinding(fileExtensionData, nameof(ISchemaDefinitionValue.FileExtension));

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
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

        private void DocumentNewCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.TransformDocument());
        }

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.TransformDocument());
        }

        private void ScriptOpenCommand_Click(object sender, EventArgs e)
        {

        }

        private void ScriptSaveCommand_Click(object sender, EventArgs e)
        {

        }


    }
}
