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

            if (formBinding.SchemaData.TryGetCurrent(out SchemaDefinitionValue? _))
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
                formBinding.DocumentData.AddBinding(schemaFileNameData, e => e.DataFileName);
                formBinding.DocumentData.AddBinding(documentContentData, e => e.FileContent);

                formBinding.DocumentData.AddBinding(objectIsExcluded, e => e.IsExcluded);
                formBinding.DocumentData.AddBinding(objectKeepOrphaned, e => e.KeepOrphaned);

                isInModelData.Checked = formBinding.IsInModel();

                ValidateFile();
            }
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

        private void DocumentFileData_SelectCommand(object sender, EventArgs e)
        {
            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            {
                openFileDialog.SelectDialog(directory, file);
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
                && formBinding.DocumentData.TryGetCurrent(out SchemaDocumentValue? fileValue))
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.MultiSelect = false;
                    dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());

                    dialog.BuildData(new List<PathItem>() { new PathItem(fileValue.ObjectPath) });

                    if (dialog.ShowDialog(this) is DialogResult.OK
                        && dialog.SelectedByNamedScope().TryGetSingle(out INamedScopeValue? selected))
                    {
                        fileValue.ObjectPath = selected.Path.MemberFullPath;
                        fileValue.ObjectScope = selected.Scope;

                        if (formBinding.SchemaData.TryGetSingle(out SchemaDefinitionValue? schemaValue))
                        {
                            fileValue.DataFileName = String.Concat(schemaValue.FilePrefix, selected.Path.Member, schemaValue.FileSuffix, ".", schemaValue.FileExtension);
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

        private void DocumentContentData_Validated(object sender, EventArgs e)
        { ValidateFile(); }

        private void ObjectScopeData_Validated(object sender, EventArgs e)
        { isInModelData.Checked = formBinding.IsInModel(); }

        private void ObjectPathData_Validated(object sender, EventArgs e)
        { isInModelData.Checked = formBinding.IsInModel(); }

        private Boolean ValidateFile()
        {
            CommandButtons[ButtonType.Open].Enabled = true;
            CommandButtons[ButtonType.Save].Enabled = true;
            Boolean result = true;

            errorProvider.SetError(localPathData.ErrorControl, String.Empty);
            errorProvider.SetError(schemaFileNameData.ErrorControl, String.Empty);
            errorProvider.SetError(documentContentData.ErrorControl, String.Empty);

            if (formBinding.TryGetFile(out IDirectoryValue? directory, out IFileValue? file))
            {
                if(!directory.IsValid(out Exception? dirException))
                {
                    errorProvider.SetError(localPathData.ErrorControl, dirException);
                    CommandButtons[ButtonType.Open].Enabled = false;
                    CommandButtons[ButtonType.Save].Enabled = false;
                    result = false;
                }

                if (!file.IsValid(out Exception? fileException))
                {
                    errorProvider.SetError(schemaFileNameData.ErrorControl, fileException);
                    CommandButtons[ButtonType.Save].Enabled = false;
                    result = false;
                }

                if (formBinding.DocumentData.TryGetCurrent(out SchemaDocumentValue? document))
                {
                    if (document.ContentException is not null)
                    {
                        errorProvider.SetError(documentContentData.ErrorControl, document.ContentException);
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
