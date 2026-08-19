using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System.Collections;
using System.ComponentModel;
using System.Text;
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
            SetIcon(ScopeType.ScriptingDocument);

            SetCommand(ButtonType.Open, ButtonType.Save, ButtonType.Delete);
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

                formBinding.SchemaData.AddBinding(localPathData, e => e.SchemaDirectory.InitialDirectory);

                ScopeNameList.Load(objectScopeData, XmlBuilder.SupportedScopes());
                formBinding.DocumentData.AddBinding(objectScopeData, e => e.ObjectScope);
                formBinding.DocumentData.AddBinding(objectNameData, e => e.ObjectName);
                formBinding.DocumentData.AddBinding(documentFileData, e => e.SchemaFile.FileName);
                formBinding.DocumentData.AddBinding(documentContentData, e => e.SchemaFile.FileContent);

                ValidateFile();
            }
        }


        protected override void AddCommand_Click(Object? sender, EventArgs e)
        {
            base.AddCommand_Click(sender, e);
            throw new NotImplementedException();
        }

        protected override void OpenCommand_Click(Object? sender, EventArgs e)
        {
            base.OpenCommand_Click(sender, e);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file)
                && openFileDialog.ShowDialog(directory, file) is DialogResult.OK)
            { DoWork(file.Open(directory), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        protected override void SaveCommand_Click(Object? sender, EventArgs e)
        {
            base.SaveCommand_Click(sender, e);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file)
                && saveFileDialog.ShowDialog(directory, file) is DialogResult.OK)
            { DoWork(file.Save(directory), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                if (args.Error is not null)
                { throw args.Error; }
            }
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

        private void ObjectNameData_SelectCommand(object sender, EventArgs e)
        {
            if (bindingDocument is not null
                && ParentForm is not null
                && formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? fileValue))
            {
                using (SelectionDialog dialog = new SelectionDialog(ParentForm))
                {
                    dialog.MultiSelect = false;
                    dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());

                    dialog.BuildData(new List<PathIndex>() { new PathIndex(fileValue.ObjectName) });

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        INamedScopeValue selected = dialog.SelectedByNamedScope().Single();
                        fileValue.ObjectName = selected.Path.MemberFullPath;
                        fileValue.ObjectScope = selected.Scope;

                        if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue))
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

        private void ValidateFile()
        {
            errorProvider.SetError(localPathData.ErrorControl, String.Empty);

            if (formBinding.SchemaData.TryGetValue(out SchemaDefinitionValue? schemaValue)
                && schemaValue.SchemaDirectory.IsInvalid(out Exception? directoryEx))
            { errorProvider.SetError(localPathData.ErrorControl, directoryEx); }

            errorProvider.SetError(documentFileData.ErrorControl, String.Empty);

            if (formBinding.DocumentData.TryGetValue(out SchemaDocumentValue? fileValue)
                && fileValue.SchemaFile.IsInvalid(out Exception? fileEx))
            { errorProvider.SetError(documentFileData.ErrorControl, fileEx); }
        }
    }
}
