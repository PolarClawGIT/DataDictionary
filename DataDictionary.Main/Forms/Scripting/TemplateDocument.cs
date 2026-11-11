using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TemplateDocument : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        DocumentIndex documentIndex = new DocumentIndex();
        TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public TemplateDocument() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingSchema, bindingTransform, bindingDocument, bindingOther);

            SetRowState(bindingDocument);
            SetIcon(bindingDocument);
            SetTitle(bindingDocument);

            SetCommand(ButtonType.Delete);

            schemaOpenCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Open);
            schemaBuildCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Export);
            schemaSaveCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Save);

            transformOpenCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
            transformSaveCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Save);

            scriptedOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
            scriptedBuildCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Export);
            scriptedSaveCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Save);

            objectPathData.SelectIcon = ScopeType.ModelObject.GetImage(ButtonType.Select);
        }

        public TemplateDocument(ITemplateIndex template, IDocumentIndex? document, Func<ITemplateData> getData) : this()
        {
            templateIndex = new TemplateIndex(template);
            formBinding.GetData = getData;

            if (document is IDocumentIndex key)
            { documentIndex = new DocumentIndex(key); }
        }

        public TemplateDocument(IDocumentIndex document) : this()
        { documentIndex = new DocumentIndex(document); }

        public TemplateDocument(IDocumentIndex document, Func<ITemplateData> getData) : this(document)
        { formBinding.GetData = getData; }

        private void Document_Load(object sender, EventArgs e)
        {
            if (documentIndex.HasValue)
            {
                formBinding.LoadValue(documentIndex);
                DoBinding();
            }
            else if (formBinding.TryCreate(templateIndex, ref documentIndex, out Exception? exception))
            {
                formBinding.LoadValue(documentIndex);
                SendMessage(new RefreshNavigation());
                DoBinding();
            }
            else
            { IsLocked(true); throw exception; }

            void DoBinding()
            {
                formBinding.TemplateData.AddBinding(templateTitleData, e => e.TemplateTitle);
                formBinding.DocumentData.AddBinding(documentTitleData, e => e.DocumentTitle);

                // Schema Tab (Input)
                SchemaDefinitionList.Load(schemaIdData, formBinding.SchemaData.AsBindingList());
                formBinding.DocumentData.AddBinding(schemaIdData, e => e.SchemaId, SchemaDefinitionList.NullValue);
                formBinding.OtherData.AddBinding(schemaFileLocationData, e => e.SchemaDirectory);

                ScopeNameList.Load(objectScopeData);
                formBinding.DocumentData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);
                formBinding.DocumentData.AddBinding(objectPathData, e => e.ObjectPath);
                formBinding.OtherData.AddBinding(isObjectInModelData, e => e.IsObjectInModel);

                formBinding.DocumentData.AddBinding(schemaFileNameData, e => e.DataFile.FileName);
                formBinding.DocumentData.AddBinding(schemaFileContentData, e => e.DataFile.FileContent);

                // Transform Tab
                TransformList.Load(transformIdData, formBinding.TransformData.AsBindingList());
                formBinding.DocumentData.AddBinding(transformIdData, e => e.TransformId, TransformList.NullValue);
                formBinding.OtherData.AddBinding(transformFileLocationData, e => e.TransformDirectory);

                formBinding.OtherData.AddBinding(transformFileNameData, e => e.TransformFileName);
                formBinding.OtherData.AddBinding(transformFileContentData, e => e.TransformFileContent);

                // Scripted Tab
                formBinding.OtherData.AddBinding(scriptedFileLocationData, e => e.ScriptedDirectory);
                formBinding.OtherData.AddBinding(scriptedFileName, e => e.ScriptedFileName);
                formBinding.OtherData.AddBinding(scriptedFileContent, e => e.ScriptedFileContent);

                ValidateChildren();

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


        private void SchemaIdData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(schemaIdData.ErrorControl, String.Empty);

            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                SchemaDefinitionIndex key = new SchemaDefinitionIndex(document);

                if (key.HasValue)
                {
                    formBinding.SchemaData.TrySetCurrent(w => key.Equals(w));
                    objectPathData.ValidateChildren();
                }
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }

        private void SchemaFileNameData_Validated(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                document.DataFile.IsValid(out Exception? exception);
                errorProvider.SetError(schemaFileNameData.ErrorControl, exception);
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }

        private void SchemaFileContentData_Validated(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                document.DataFile.TryParseXml(out Exception? exception);
                errorProvider.SetError(schemaFileContentData.ErrorControl, exception);
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }


        private void ObjectPathData_SelectCommand(object sender, EventArgs e)
        {
            if (bindingDocument is not null
                && formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.MultiSelect = false;
                    dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());

                    dialog.BuildData(new List<PathItem>() { new PathItem(document.ObjectPath) });

                    if (dialog.ShowDialog(this) is DialogResult.OK
                        && dialog.SelectedByNamedScope().TryGetSingle(out INamedScopeValue? selected))
                    {
                        document.DocumentTitle = selected.Path.Member;
                        document.ObjectPath = selected.Path.MemberFullPath;
                        document.ObjectScope = selected.Scope;

                        if (formBinding.SchemaData.TryGetSingle(out SchemaDefinitionValue? schemaValue))
                        {
                            document.DataFile.FileName = String.Concat(schemaValue.FilePrefix, selected.Path.Member, schemaValue.FileSuffix, ".", schemaValue.FileExtension);
                            objectPathData.ValidateChildren();
                            schemaFileNameData.ValidateChildren();
                        }
                    }
                }
            }
        }

        private void ObjectScopeData_Validated(object sender, EventArgs e)
        { objectPathData.ValidateChildren(); }

        private void ObjectPathData_Validated(object sender, EventArgs e)
        {
            schemaBuildCommand.Enabled = false;

            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document)
                && formBinding.OtherData.TryGetCurrent(out OtherBinding? other))
            {
                schemaBuildCommand.Enabled = other.IsObjectInModel
                    && new SchemaDefinitionIndex(document).HasValue;
            }
        }


        private void SchemaOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? other)
                && other.TryGetSchemaDirectory(out IDirectoryValue? directory)
                && other.TryGetSchemaFile(out IFileValue? file))
            { DoWork(openFileDialog.OpenDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                schemaFileNameData.ValidateChildren();
                schemaFileContentData.ValidateChildren();

                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        private void SchemaSaveCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SchemaBuildCommand_Click(object sender, EventArgs e)
        {
            formBinding.BuildFileContent();
            schemaFileContentData.ValidateChildren();
        }

        private void BindingOther_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? otherData))
            {
                if (otherData.TryGetSchemaDirectory(out IDirectoryValue? schemaDirectory))
                {
                    schemaDirectory.IsValid(out Exception? schema);
                    schemaOpenCommand.Enabled = schema is null;
                    schemaSaveCommand.Enabled = schema is null;
                    errorProvider.SetError(schemaFileLocationData.ErrorControl, schema);
                }

                if (otherData.TryGetTransformDirectory(out IDirectoryValue? transformDirectory))
                {
                    transformDirectory.IsValid(out Exception? transform);
                    transformOpenCommand.Enabled = transform is null;
                    transformSaveCommand.Enabled = transform is null;
                    errorProvider.SetError(transformFileLocationData.ErrorControl, transform);
                }

                if (otherData.TryGetScriptedDirectory(out IDirectoryValue? scriptedDirectory))
                {
                    scriptedDirectory.IsValid(out Exception? scripted);
                    scriptedOpenCommand.Enabled = scripted is null;
                    scriptedSaveCommand.Enabled = scripted is null;
                    errorProvider.SetError(scriptedFileLocationData.ErrorControl, scripted);
                }
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }

        private void TransformIdData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(transformIdData.ErrorControl, String.Empty);

            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                TransformIndex key = new TransformIndex(document);

                if (key.HasValue)
                { formBinding.TransformData.TrySetCurrent(w => key.Equals(w)); }

                if (formBinding.OtherData.TryGetCurrent(out OtherBinding? other))
                {
                    other.OnPropertyChanged(nameof(other.TransformFileName));
                    other.OnPropertyChanged(nameof(other.TransformFileContent));
                }
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }

        private void TransformOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? other)
                && other.TryGetTransformDirectory(out IDirectoryValue? directory)
                && other.TryGetTransformFile(out IFileValue? file))
            { DoWork(openFileDialog.OpenDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                other.OnPropertyChanged(nameof(other.TransformFileName));
                other.OnPropertyChanged(nameof(other.TransformFileContent));

                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        private void TransformSaveCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ScriptedOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? other)
                && other.TryGetScriptedDirectory(out IDirectoryValue? directory)
                && other.TryGetScriptedFile(out IFileValue? file))
            { DoWork(openFileDialog.OpenDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                other.OnPropertyChanged(nameof(other.ScriptedFileName));
                other.OnPropertyChanged(nameof(other.ScriptedFileContent));

                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        private void ScriptedBuildCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? other))
            {
                other.TryExecuteTransform(out Exception? exception);
                other.OnPropertyChanged(nameof(other.ScriptedFileContent));
                errorProvider.SetError(scriptedFileContent.ErrorControl, exception);
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }

        private void ScriptedSaveCommand_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private Exception ThrowDataNotFound(object source, params IEnumerable<KeyValuePair<String, String>> data)
        {   // This is never expected to be called as Data should always exist.

            Exception result = new InvalidOperationException("data not found");

            if (source is Control control)
            { result.Data.Add(nameof(control.Name), control.Name); }

            foreach (KeyValuePair<String, String> item in data)
            { result.Data.Add(item.Key, item.Value); }

            return result;
        }


    }
}
