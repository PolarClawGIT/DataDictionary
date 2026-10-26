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

            formBinding = new FormBinding(bindingTemplate, bindingTransform, bindingDocument);

            SetRowState(bindingTransform);
            SetTitle(bindingTransform);
            SetIcon(bindingTransform);

            SetCommand(ButtonType.Open, ButtonType.Save);

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

        public Transform(ITransformComposite transform) : this(transform, transform) { }

        public Transform(ITransformComposite transform, Func<ITemplateData> getData) : this(transform, transform, getData) { }

        private void Transform_Load(object sender, EventArgs e)
        {
            if (transformIndex.HasValue)
            {
                formBinding.LoadValue(transformIndex);
                DoBinding();
            }
            else if (formBinding.TryCreate(templateIndex, ref transformIndex, out Exception? exception))
            {
                formBinding.LoadValue(transformIndex);
                SendMessage(new RefreshNavigation());
                DoBinding();
            }
            else
            { IsLocked(true); throw exception; }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.TransformData.AddBinding(transformTitleData, e => e.TransformTitle);

                DirectoryTypeList.Load(rootFolderData);
                formBinding.TransformData.AddBinding(rootFolderData, e => e.RootFolder, DirectoryTypeList.NullValue);
                formBinding.TransformData.AddBinding(relativePathData, e => e.RelativePath);
                formBinding.TransformData.AddBinding(filePrefixData, e => e.FilePrefix);
                formBinding.TransformData.AddBinding(fileSuffixData, e => e.FileSuffix);

                FileFormatList.Load(fileExtensionData, TransformDocumentValue.FileFormats, formBinding.TransformData.Select(s => s.FileExtension));
                formBinding.TransformData.AddBinding(fileExtensionData, e => e.FileExtension, FileFormatList.NullValue);

                formBinding.TemplateData.AddBinding(transformLocalPath, e => e.InitialDirectory);
                formBinding.TransformData.AddBinding(documentLocalPathData, e => e.InitialDirectory);

                formBinding.TransformData.AddBinding(scriptFileNameData, e => e.TransformFileName);
                formBinding.TransformData.AddBinding(scriptData, e => e.FileContent);

                SchemaDefinitionList.Load(schemaIdData, formBinding.SchemaData, "<Select Schema>");
                formBinding.TransformData.AddBinding(schemaIdData, e => e.SchemaId, SchemaDefinitionList.NullValue);

                formBinding.DocumentData.AddBinding(documentData);

                // Security
                ValidateChildren();
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.Authorize);
            }
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            { DoWork(openFileDialog.OpenDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                ValidateChildren();

                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        protected override void SaveCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveCommand_Click(sender, e);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            { DoWork(saveFileDialog.SaveDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        private void DocumentNewCommand_Click(object sender, EventArgs e)
        { Activate(() => new Forms.Scripting.TransformDocument(transformIndex, formBinding.GetData)); }

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetCurrent(out TransformDocumentValue? value))
            {
                DocumentIndex key = new DocumentIndex(value);
                Activate(() => new Forms.Scripting.TransformDocument(key, formBinding.GetData), o => o.IsOpenItem(key));
            }
        }

        private void RelativePathData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TransformData.TryGetCurrent(out TransformValue? current))
            {
                folderBrowserDialog.Reset();
                folderBrowserDialog.RootFolder = current.RootFolder.GetSystemFolder();
                folderBrowserDialog.InitialDirectory = current.InitialDirectory;

                if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
                { current.InitialDirectory = folderBrowserDialog.SelectedPath; }
            }
        }

        private void ScriptFileNameData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            { openFileDialog.SelectDialog(directory, file); }
        }

        private void ScriptFileNameData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(scriptFileNameData.ErrorControl, String.Empty);

            if (formBinding.TransformData.TryGetCurrent(out TransformValue? transform)
                && !transform.IsFileValid(out Exception? exception))
            { errorProvider.SetError(scriptFileNameData.ErrorControl, exception); }
        }

        private void TransformLocalPath_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(transformLocalPath.ErrorControl, String.Empty);
            Boolean directoryValid = true;
            CommandButtons[ButtonType.Save].Enabled = false;
            CommandButtons[ButtonType.Open].Enabled = false;

            if (formBinding.TemplateData.TryGetCurrent(out TemplateValue? template)
                && !template.IsDirectoryValid(out Exception? exception))
            { errorProvider.SetError(transformLocalPath.ErrorControl, exception); directoryValid = false; }

            if (formBinding.TransformData.TryGetCurrent(out TransformValue? transform))
            {
                CommandButtons[ButtonType.Save].Enabled = directoryValid
                    && !String.IsNullOrWhiteSpace(transform.FileContent)
                    && !String.IsNullOrWhiteSpace(transform.TransformFileName);

                CommandButtons[ButtonType.Open].Enabled = directoryValid;
            }
        }

        private void RootFolderData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(rootFolderData.ErrorControl, String.Empty);

            if (formBinding.TransformData.TryGetCurrent(out TransformValue? transform)
                && transform.RootFolder is DirectoryType.Null)
            { errorProvider.SetError(rootFolderData.ErrorControl, "Not Defined"); }
        }

        private void RelativePathData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(relativePathData.ErrorControl, String.Empty);

            if (formBinding.TransformData.TryGetCurrent(out TransformValue? transform)
                && !transform.IsFileValid(out Exception? exception))
            { errorProvider.SetError(relativePathData.ErrorControl, exception); }
        }

        private void SchemaIdData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(schemaIdData.ErrorControl, String.Empty);

            if (formBinding.TransformData.TryGetCurrent(out TransformValue? transform))
            {
                if (!new SchemaDefinitionIndex(transform).HasValue)
                { errorProvider.SetError(schemaIdData.ErrorControl, "Missing Value"); }
            }
        }

        private void BindingTemplate_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TemplateData.TryGetCurrent(out TemplateValue? template)
                && formBinding.TransformData.TryGetCurrent(out TransformValue? transform))
            {
                if (template.RootFolder is not DirectoryType.Null
                    && transform.RootFolder is DirectoryType.Null)
                {
                    transform.RootFolder = template.RootFolder;
                    rootFolderData.ValidateChildren();
                }

                if(!String.IsNullOrWhiteSpace(template.RelativePath)
                    && String.IsNullOrWhiteSpace(transform.RelativePath))
                {
                    transform.RelativePath = template.RelativePath;
                    relativePathData.ValidateChildren();
                }

                if (template.IsDirectoryValid(out _)
                    && !String.IsNullOrWhiteSpace(template.RelativePath)
                    && !String.IsNullOrWhiteSpace(transform.TransformFileName)
                    && (Path.GetDirectoryName(transform.TransformFileName) ?? String.Empty).
                        Equals(template.RelativePath ?? String.Empty)
                    )
                {   // Not enough information to determine what the current full path is to what the new one would be.
                    // Just reset the file name as a safe alternative.
                    transform.TransformFileName = Path.GetFileName(transform.TransformFileName);
                    scriptFileNameData.ValidateChildren();
                }
            }

            transformLocalPath.ValidateChildren();
        }

    }
}
