using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class Document : ApplicationData, IApplicationDataForm
    {
        FormBinding formBinding;
        DocumentIndex documentIndex = new DocumentIndex();
        TemporalIndex? temporalIndex = null;

        public Boolean IsOpenItem(object? item)
        { return documentIndex.Equals(item); }

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
                exceptionData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.DocumentException)));

                inputData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.InputText)));
                inputDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.InputPath)));
                inputFileData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.InputFile)));

                TemplateNameList.Load(templateData, "(n/a)");
                templateData.DataBindings.Add(new Binding(
                    nameof(ComboBox.SelectedValue),
                    bindingDocument,
                    nameof(ITemplateValue.TemplateId)));
                transformData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.TransformScript)));

                outputData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.ResultText)));
                outputDirectoryData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.OutputPath)));
                outputFileData.DataBindings.Add(new Binding(nameof(TextBox.Text), bindingDocument, nameof(IDocumentValue.OutputFile)));

                // Security
                IsLocked(formBinding.GetLocked());
                SetAuthorization(formBinding.GetAuthorization);
            }
        }

        private void InputOpenCommand_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XML data|*.XML";

            if(formBinding.TryGetValue(out DocumentValue? document))
            {
                openFileDialog.InitialDirectory = document.InputPath ;
                openFileDialog.FileName = document.InputFile;

                DialogResult dialogResult = openFileDialog.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {
                    document.InputPath = Path.GetDirectoryName(openFileDialog.FileName)??String.Empty;
                    document.InputFile = Path.GetFileName(openFileDialog.FileName);
                }
            }
        }

        private void InputSaveCommand_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XML data|*.XML";

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (formBinding.TryGetValue(out DocumentValue? document))
            {
                saveFileDialog.InitialDirectory = document.InputPath;
                saveFileDialog.FileName = document.InputFile;

                if (dialogResult is DialogResult.OK)
                {
                    document.InputPath = Path.GetDirectoryName(saveFileDialog.FileName) ?? String.Empty;
                    document.InputFile = Path.GetFileName(saveFileDialog.FileName);
                }
            }
        }

        private void OpenTransformCommand_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "XSL Transform|*.XSL";

            if (formBinding.TryGetValue(out DocumentValue? document))
            {
                openFileDialog.InitialDirectory = document.TransformPath;
                openFileDialog.FileName = document.TransformFile;

                DialogResult dialogResult = openFileDialog.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {

                }
            }


        }

        private void SaveTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void GetTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void SaveResultCommand_Click(object sender, EventArgs e)
        {

        }

        private void RefreshResultCommand_Click(object sender, EventArgs e)
        {

        }

        protected override void DeleteCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteCommand_Click(sender, e);
        }

        protected override void DeleteFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.DeleteFromDatabaseCommand_Click(sender, e);
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);
        }

        protected override void SaveCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveCommand_Click(sender, e);
        }

        protected override void OpenFromDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenFromDatabaseCommand_Click(sender, e);
        }

        protected override void SaveToDatabaseCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveToDatabaseCommand_Click(sender, e);
        }

        protected override void HistoryCommand_Click(Object sender, EventArgs e)
        {
            base.HistoryCommand_Click(sender, e);
        }
    }
}
