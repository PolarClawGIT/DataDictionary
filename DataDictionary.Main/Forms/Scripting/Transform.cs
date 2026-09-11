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

            SetRowState(bindingTransform);
            SetTitle(bindingTransform);
            SetIcon(bindingTransform);

            SetCommand(ButtonType.Delete);

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
            { formBinding.LoadValue(transformIndex); }
            else
            {
                if (templateIndex.HasValue)
                {
                    TransformValue value = new TransformValue(templateIndex);
                    formBinding.TransformData.Add(value);
                    transformIndex = new TransformIndex(value);
                    formBinding.LoadValue(transformIndex);
                    SendMessage(new RefreshNavigation());
                }
                else
                {   // This should never occur.
                    Exception ex = new InvalidOperationException("Template not found");
                    ex.Data.Add(nameof(templateIndex), templateIndex);
                    throw ex;
                }
            }

            if (formBinding.TransformData.TryGetCurrent(out TransformValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.TransformData.AddBinding(transformTitleData, e => e.TransformTitle);

                DirectoryTypeList.Load(rootFolderData);
                formBinding.TransformData.AddBinding(rootFolderData, e => e.RootFolder, DirectoryTypeList.NullValue);

                formBinding.TransformData.AddBinding(relativePathData, e => e.RelativePath);
                formBinding.TransformData.AddBinding(filePrefixData, e => e.FilePrefix);
                formBinding.TransformData.AddBinding(fileSuffixData, e => e.FileSuffix);
                formBinding.TransformData.AddBinding(fileExtensionData, e => e.FileExtension);

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
            }
        }

        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
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

        protected override void HandleMessage(RefreshRow message)
        {
            base.HandleMessage(message);

            if (message is RefreshRow<TemplateIndex> rowMessage
                && rowMessage.Key.Equals(templateIndex))
            {
                formBinding.LoadValue(transformIndex);
                SendMessage(new RefreshRow<TransformIndex>(transformIndex));
            }
        }
    }
}
