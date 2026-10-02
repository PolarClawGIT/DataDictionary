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

            formBinding = new FormBinding(bindingTemplate, bindingTransform);

            SetRowState(bindingTransform);
            SetTitle(bindingTransform);
            SetIcon(bindingTransform);

            SetCommand(ButtonType.Open, ButtonType.Save, ButtonType.Delete);

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

                FileFormatList.Load(fileExtensionData, TransformDocumentValue.FileFormats, formBinding.TransformData.Select(s => s.FileExtension));
                formBinding.TransformData.AddBinding(fileExtensionData, e => e.FileExtension, FileFormatList.NullValue);

                formBinding.TemplateData.AddBinding(transformLocalPath, e => e.InitialDirectory);
                formBinding.TransformData.AddBinding(documentLocalPathData, e => e.InitialDirectory);
                
                formBinding.TransformData.AddBinding(scriptFileNameData, e => e.TransformFileName);
                formBinding.TransformData.AddBinding(scriptData, e => e.FileContent);

                ValidateFile();

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

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            { DoWork(openFileDialog.OpenDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                ValidateFile();

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

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.TransformDocument());
        }

        private void RootFolderData_Validated(object sender, EventArgs e)
        {
            ValidateFile();
        }

        private void RelativePathData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TransformData.TryGetCurrent(out TransformValue? current))
            {
                folderBrowserDialog.Reset();
                folderBrowserDialog.RootFolder = current.RootFolder.GetSystemFolder();
                folderBrowserDialog.InitialDirectory = current.InitialDirectory;

                if (folderBrowserDialog.ShowDialog() is DialogResult.OK)
                {
                    current.InitialDirectory = folderBrowserDialog.SelectedPath;
                    ValidateFile();
                }
            }
        }

        private void ScriptFileNameData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            {
                openFileDialog.SelectDialog(directory, file);
                ValidateFile();
            }
        }

        private Boolean ValidateFile()
        {
            CommandButtons[ButtonType.Open].Enabled = true;
            CommandButtons[ButtonType.Save].Enabled = true;
            Boolean result = true;

            errorProvider.SetError(rootFolderData.ErrorControl, String.Empty);
            errorProvider.SetError(scriptFileNameData.ErrorControl, String.Empty);
            errorProvider.SetError(scriptData.ErrorControl, String.Empty);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            {
                if (directory.RootFolder is DirectoryType.Null)
                {
                    errorProvider.SetError(rootFolderData.ErrorControl, "Not Defined");
                    CommandButtons[ButtonType.Open].Enabled = false;
                    CommandButtons[ButtonType.Save].Enabled = false;
                    result = false;
                }

                if (!file.IsValid(out Exception? fileException))
                {
                    errorProvider.SetError(scriptFileNameData.ErrorControl, fileException);
                    CommandButtons[ButtonType.Save].Enabled = false;
                    result = false;
                }

                if (formBinding.TransformData.TryGetCurrent(out TransformValue? transform))
                {
                    if (transform.ContentException is not null)
                    {
                        errorProvider.SetError(scriptData.ErrorControl, transform.ContentException);
                        CommandButtons[ButtonType.Save].Enabled = false;
                        result = false;
                    }
                }
            }
            else { throw new InvalidOperationException(); } // Should not get here.

            return result;
        }


    }
}
