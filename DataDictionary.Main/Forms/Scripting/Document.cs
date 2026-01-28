using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document : ApplicationData, IApplicationDataForm
    {
        FormBinding formBinding;
        DocumentIndex documentIndex = new DocumentIndex();
        TemporalIndex? temporalIndex = null;

        public Boolean IsOpenItem(object? item)
        { return formBinding.Equals(item); }

        public Document()
        {
            InitializeComponent();

            inputOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(CommandType.Open);
            inputSaveCommand.Image = ScopeType.ScriptingDocument.GetImage(CommandType.Save);

            openTransformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Open);
            saveTransformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Save);
            getTransformCommand.Image = ScopeType.ScriptingTemplate.GetImage(CommandType.Import);

            saveResultCommand.Image = ScopeType.ApplicationDocument.GetImage(CommandType.Save);
            refreshResultCommand.Image = ScopeType.ApplicationDocument.GetImage(CommandType.Refresh);

            SetIcon(ScopeType.ScriptingDocument);

            formBinding = new FormBinding()
            {
                DocumentBinding = bindingDocument,
                DoWork = base.DoWork
            };

            SetIcon(ScopeType.ScriptingData);
            SetTitle(bindingDocument);
            SetRowState(bindingDocument);

            SetCommand(ScopeType.ScriptingDocument,
                CommandType.Delete,
                CommandType.Save,
                CommandType.Open,
                CommandType.OpenDatabase,
                CommandType.SaveDatabase,
                CommandType.DeleteDatabase,
                CommandType.HistoryDatabase);
        }

        public Document(IDocumentIndex? document) : this()
        {
            if (document is IDocumentIndex)
            { documentIndex = new DocumentIndex(document); }
            else { documentIndex = new DocumentIndex(formBinding.NewValue()); }
        }

        public Document(DocumentIndex document, ITemporalIndex temporal) : this(document)
        { temporalIndex = new TemporalIndex(); }

        private void Document_Load(object sender, EventArgs e)
        {
            if (temporalIndex is null)
            {
                formBinding.Load(documentIndex);
                DoBinding();
            }
            else
            { formBinding.Load(documentIndex, temporalIndex, onCompleting); }

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
                documentTitleData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.DocumentTitle)));

                DirectoryTypeList.Load(rootFolderData);
                rootFolderData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingDocument,
                    nameof(ITemplateValue.RootFolder),
                    true, DataSourceUpdateMode.OnValidation));

                rootPathData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.RootPath)));
                //exceptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.DocumentException)));

                inputData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.InputValue), nameof(IDocumentFile.Content))));
                inputDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.InputValue), nameof(IDocumentFile.Directory))));
                inputFileData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.InputValue), nameof(IDocumentFile.FileName))));

                TemplateNameList.Load(templateData, "(n/a)");
                templateData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingDocument,
                    nameof(ITemplateValue.TemplateId)));

                transformData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.TransformValue), nameof(IDocumentFile.Content))));
                transformDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.TransformValue), nameof(IDocumentFile.Directory))));
                transformFileData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.TransformValue), nameof(IDocumentFile.FileName))));

                outputData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.OutputValue), nameof(IDocumentFile.Content))));
                outputDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.OutputValue), nameof(IDocumentFile.Directory))));
                outputFileData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, NavigationPath(nameof(IDocumentValue.OutputValue), nameof(IDocumentFile.FileName))));

                // Security
                SetCommandEnabled();
                IsLocked(formBinding.GetLocked());
                SetAuthorization((f) => formBinding.GetAuthorization(documentIndex, f));
            }
        }

        private void InputOpenCommand_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML data|*.XML";
            Open(openFileDialog, formBinding.InputValue, onCompleted);

            void onCompleted(RunWorkerCompletedEventArgs args)
            {
                bindingDocument.ResetCurrentItem();

                if (args.Error is Exception exception)
                { errorProvider.SetError(inputFileData.ErrorControl, exception.Message); }
                else { errorProvider.SetError(inputFileData.ErrorControl, String.Empty); }

                if (formBinding.InputValue.TryParse(out XDocument? _, out Exception? xException))
                { errorProvider.SetError(inputData.ErrorControl, String.Empty); }
                else { errorProvider.SetError(inputData.ErrorControl, xException.Message); }
            }

        }

        private void InputSaveCommand_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XML data|*.XML";
            Save(saveFileDialog, formBinding.InputValue, onCompleted);

            void onCompleted(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is Exception exception)
                { errorProvider.SetError(inputFileData.ErrorControl, exception.Message); }
                else { errorProvider.SetError(inputFileData.ErrorControl, String.Empty); }
            }
        }

        private void OpenTransformCommand_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XSL Transform|*.XSLT;*.XSL;";
            Open(openFileDialog, formBinding.TransformValue, onCompleted);

            void onCompleted(RunWorkerCompletedEventArgs args)
            {
                bindingDocument.ResetCurrentItem();

                if (args.Error is Exception exception)
                { errorProvider.SetError(transformFileData.ErrorControl, exception.Message); }
                else { errorProvider.SetError(transformFileData.ErrorControl, String.Empty); }

                if (formBinding.TransformValue.TryParse(out XDocument? _, out Exception? xException))
                { errorProvider.SetError(transformData.ErrorControl, String.Empty); }
                else { errorProvider.SetError(transformData.ErrorControl, xException.Message); }
            }

        }

        private void SaveTransformCommand_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XSL Transform|*.XSLT;*.XSL;";
            Save(saveFileDialog, formBinding.TransformValue, onCompleted);

            void onCompleted(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is Exception exception)
                { errorProvider.SetError(transformFileData.ErrorControl, exception.Message); }
                else { errorProvider.SetError(transformFileData.ErrorControl, String.Empty); }
            }
        }

        private void GetTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void SaveResultCommand_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Plain Text|*.TXT|XML data|*.XML|SQL Script|*.SQL|C# Fragment|*.CS|VB.Net Fragment|*.VB|Other|*.*";
            Save(saveFileDialog, formBinding.OutputValue, onCompleted);

            void onCompleted(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is Exception exception)
                { errorProvider.SetError(outputData.ErrorControl, exception.Message); }
                else { errorProvider.SetError(outputData.ErrorControl, String.Empty); }
            }
        }

        private void RefreshResultCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.TryTransform(documentIndex, out Exception? exception))
            { errorProvider.SetError(outputData.ErrorControl, String.Empty); }
            else { errorProvider.SetError(outputData.ErrorControl, exception.Message); }
        }



        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            formBinding.OpenFiles(onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }


        protected override void SaveCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveCommand_Click(sender, e);

            formBinding.SaveFiles(onComplete);

            void onComplete(RunWorkerCompletedEventArgs args)
            { }
        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);

            formBinding.Remove();
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);

            formBinding.Remove();
            formBinding.Save(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);

            formBinding.Load(documentIndex, onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);

            formBinding.Save(onCompleting);

            void onCompleting(RunWorkerCompletedEventArgs args)
            { IsLocked(formBinding.GetLocked()); }
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);

            Activate(() => new ApplicationWide.HistoryView(formBinding.GetTemporal())
            {
                OpenForm = (temporal) =>
                { return new Document(documentIndex, new TemporalIndex(temporal)); }
            });
        }

        private void RootFolderData_Validated(object sender, EventArgs e)
        {
            SetCommandEnabled();
            bindingDocument.ResetCurrentItem();
        }

        private void SetCommandEnabled()
        {
            inputOpenCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;
            inputSaveCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;

            openTransformCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;
            saveTransformCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;
            getTransformCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;

            saveResultCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;
            refreshResultCommand.Enabled = formBinding.RootFolder is not DirectoryType.Null;
        }

        private void InputData_Validated(object sender, EventArgs e)
        {
            if (formBinding.InputValue.TryParse(out String? document, out Exception? exception))
            {
                formBinding.InputValue.Content = document;
                errorProvider.SetError(inputData.ErrorControl, String.Empty);
            }
            else
            { errorProvider.SetError(inputData.ErrorControl, exception.Message); }
        }

        private void TransformData_Validated(object sender, EventArgs e)
        {
            if (formBinding.TransformValue.TryParse(out String? document, out Exception? exception))
            {
                formBinding.InputValue.Content = document;
                errorProvider.SetError(transformData.ErrorControl, String.Empty);
            }
            else
            { errorProvider.SetError(transformData.ErrorControl, exception.Message); }
        }
    }
}
