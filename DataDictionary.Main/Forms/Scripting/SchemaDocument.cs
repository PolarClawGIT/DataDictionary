using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class SchemaDocument : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex();
        DocumentIndex documentIndex = new DocumentIndex();
        //TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is IDocumentIndex key && documentIndex.Equals(key); }

        public SchemaDocument()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingSchema, bindingDocument);
            SetRowState(bindingDocument);
            SetTitle(bindingDocument);
            SetIcon(bindingDocument);

            SetCommand(ButtonType.Open, ButtonType.Save, ButtonType.Export);
            CommandButtons[ButtonType.SaveDatabase].Visible = false;
            CommandButtons[ButtonType.OpenDatabase].Visible = false;
            CommandButtons[ButtonType.DeleteDatabase].Visible = false;
            CommandButtons[ButtonType.Export].ToolTipText = "Build XML";
        }

        public SchemaDocument(IDocumentIndex document, Func<ITemplateData> getData) : this()
        { documentIndex = new DocumentIndex(document); }

        public SchemaDocument(ISchemaDefinitionIndex schema, Func<ITemplateData> getData) : this()
        { schemaIndex = new SchemaDefinitionIndex(schema); }


        private void SchemaDocument_Load(object sender, EventArgs e)
        {
            if (documentIndex.HasValue)
            {
                formBinding.LoadValue(documentIndex);
            }
            else if (schemaIndex.HasValue)
            {
                formBinding.LoadValue(schemaIndex, out documentIndex);
            }
            else
            {   // This should never occur.
                Exception ex = new InvalidOperationException("SchemaDefinition not found");
                ex.Data.Add(nameof(schemaIndex), schemaIndex);
                throw ex;
            }

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? _))
            { DoBinding(); }
            else { IsLocked(true); }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.SchemaData.AddBinding(schemaTitleData, e => e.SchemaTitle);

                formBinding.SchemaData.AddBinding(localPathData, e => e.InitialDirectory);

                ScopeNameList.Load(objectScopeData, XmlBuilder.SupportedScopes());
                formBinding.DocumentData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);
                formBinding.DocumentData.AddBinding(objectPathData, e => e.ObjectPath);
                formBinding.DocumentData.AddBinding(documentFileData, e => e.FileName);
                formBinding.DocumentData.AddBinding(documentContentData, e => e.FileContent);

                ValidateFile();
            }
        }


        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file)
                && openFileDialog.ShowDialog(directory, file) is DialogResult.OK)
            {
                if (String.IsNullOrWhiteSpace(directory.InitialDirectory))
                { file.FileName = openFileDialog.FileName; }
                else
                { file.FileName = Path.GetRelativePath(directory.InitialDirectory, openFileDialog.FileName); }

                DoWork(file.Open(new FileInfo(openFileDialog.FileName)), onCompleting);
            }

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

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file)
                && ValidateFile()
                && saveFileDialog.ShowDialog(directory, file) is DialogResult.OK)
            {
                if (String.IsNullOrWhiteSpace(directory.InitialDirectory))
                { file.FileName = openFileDialog.FileName; }
                else
                { file.FileName = Path.GetRelativePath(directory.InitialDirectory, saveFileDialog.FileName); }

                DoWork(file.Save(new FileInfo(saveFileDialog.FileName)), onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        private void DocumentFileData_SelectCommand(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select file (does not OPEN)";
            openFileDialog.CheckFileExists = false;

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file)
                && openFileDialog.ShowDialog(directory, file) is DialogResult.OK)
            {
                if (String.IsNullOrWhiteSpace(directory.InitialDirectory))
                { file.FileName = openFileDialog.FileName; }
                else
                { file.FileName = Path.GetRelativePath(directory.InitialDirectory, openFileDialog.FileName); }

                ValidateFile();
            }
        }

        protected override void ExportCommand_Click(Object? sender, EventArgs e)
        {
            base.ExportCommand_Click(sender, e);

            formBinding.BuildFileContent();
            ValidateFile();
        }

        private void ObjectNameData_SelectCommand(object sender, EventArgs e)
        {
            if (bindingDocument is not null
                && formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? fileValue))
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.MultiSelect = false;
                    dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());

                    dialog.BuildData(new List<PathIndex>() { new PathIndex(fileValue.ObjectPath) });

                    if (dialog.ShowDialog(this) is DialogResult.OK
                        && dialog.SelectedByNamedScope().TryGetSingle(out INamedScopeValue? selected))
                    {
                        fileValue.ObjectPath = selected.Path.MemberFullPath;
                        fileValue.ObjectScope = selected.Scope;

                        if (formBinding.SchemaData.TryGetSingle(out SchemaDefinitionValue? schemaValue))
                        {
                            fileValue.FileName = String.Concat(schemaValue.FilePrefix, selected.Path.Member, schemaValue.FileSuffix, ".", schemaValue.FileExtension);
                            ValidateFile();
                        }
                    }
                }
            }
        }

        private void LocalPathData_Validated(object sender, EventArgs e)
        { ValidateFile(); }

        private void DocumentFileData_Validated(object sender, EventArgs e)
        { ValidateFile(); }

        private Boolean ValidateFile()
        {
            Boolean result = true;

            errorProvider.SetError(localPathData.ErrorControl, String.Empty);

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue)
                && !schemaValue.IsValid(out Exception? directoryEx))
            { errorProvider.SetError(localPathData.ErrorControl, directoryEx); result = false; }

            errorProvider.SetError(documentFileData.ErrorControl, String.Empty);
            errorProvider.SetError(documentContentData.ErrorControl, String.Empty);

            if (formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? fileValue))
            {
                if (!fileValue.IsValid(out Exception? fileEx))
                { errorProvider.SetError(documentFileData.ErrorControl, fileEx); result = false; }

                if (fileValue.ContentException is not null)
                { errorProvider.SetError(documentContentData.ErrorControl, fileValue.ContentException); result = false; }
            }

            CommandButtons[ButtonType.Save].Enabled = result;
            return result;
        }

        private void DocumentContentData_Validated(object sender, EventArgs e)
        { ValidateFile(); }
    }
}
