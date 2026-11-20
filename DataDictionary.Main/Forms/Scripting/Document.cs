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
    partial class Document : ApplicationData
    {
        // TODO (POC):  This brought out a design flaw.
        // This works for a single object and file. 
        // That covers the Visual Studio output where there a 1:1 relationship between object and file.
        // This does not cover an Excel story where there is M:1 relationship between objects and file.
        // This does cover the necessary coding elements and shows it all can be done.
        //
        // Possible Fix is to add a list of Objects. This will need a new table "Document Object".
        // One schema would be used as it can already define the structures for multiple object types (Attribute, Entity, and Process).
        //
        // A second issue was identified is that XSML is not supported by browsers anymore.
        // This means I cannot test the XML Transform in the browser.
        // The move appears to be to go with JSON for data (replacing XML).
        // But JSON has nothing that does the job of an XML Transform.
        // Instead JSON depends on just in time scripting by 3rd party processing (like JINT).
        // The schema would need to use System.Text.Json.Nodes.
        // See: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-dom
        // This is an area that I don't know anything about and will take considerable research.


        TemplateIndex templateIndex = new TemplateIndex();
        DocumentIndex documentIndex = new DocumentIndex();
        TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public Document() : base()
        {
            InitializeComponent();

            formBinding = new FormBinding(
                templateBinding: bindingTemplate,
                schemaBinding: bindingSchema,
                transformBinding: bindingTransform,
                documentBinding: bindingDocument,
                objectBinding: bindingObject,
                otherBinding: bindingOther);

            SetRowState(bindingDocument);
            SetIcon(bindingDocument);
            SetTitle(bindingDocument);

            SetCommand(ButtonType.Delete);

            objectAddCommand.Image = ScopeType.ModelObject.GetImage(ButtonType.Add);
            objectDeleteCommand.Image = ScopeType.ModelObject.GetImage(ButtonType.Delete);
            objectPathData.SelectIcon = ScopeType.ModelObject.GetImage(ButtonType.Select);

            schemaOpenCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Open);
            schemaBuildCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Export);
            schemaSaveCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Save);

            transformOpenCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
            transformSaveCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Save);

            scriptedOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);
            scriptedBuildCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Export);
            scriptedSaveCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Save);


        }

        public Document(ITemplateIndex template, IDocumentIndex? document, Func<ITemplateData> getData) : this()
        {
            templateIndex = new TemplateIndex(template);
            formBinding.GetData = getData;

            if (document is IDocumentIndex key)
            { documentIndex = new DocumentIndex(key); }
        }

        public Document(IDocumentIndex document) : this()
        { documentIndex = new DocumentIndex(document); }

        public Document(IDocumentIndex document, Func<ITemplateData> getData) : this(document)
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

                // Object tab (Input)
                ScopeNameList.Load(objectScopeColumn, XmlBuilder.SupportedScopes());
                ScopeNameList.Load(objectScopeData, XmlBuilder.SupportedScopes());

                formBinding.ObjectData.AddBinding(objectData);
                formBinding.ObjectData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);
                formBinding.ObjectData.AddBinding(objectPathData, e => e.ObjectPath);
                formBinding.OtherData.AddBinding(isObjectInModelData, e => e.IsObjectInModel);

                // Schema Tab (Input)
                SchemaDefinitionList.Load(schemaIdData, formBinding.SchemaData.AsBindingList());
                formBinding.DocumentData.AddBinding(schemaIdData, e => e.SchemaId, SchemaDefinitionList.NullValue);
                formBinding.OtherData.AddBinding(schemaFileLocationData, e => e.SchemaDirectory);

                formBinding.DocumentData.AddBinding(structuredFileNameData, e => e.StructuredFile.FileName);
                formBinding.DocumentData.AddBinding(structuredFileContentData, e => e.StructuredFile.FileContent);

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

            if (formBinding.DocumentData.TryGetCurrent(out DocumentValue? document))
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
            if (formBinding.DocumentData.TryGetCurrent(out DocumentValue? document))
            {
                document.StructuredFile.IsValid(out Exception? exception);
                errorProvider.SetError(structuredFileNameData.ErrorControl, exception);
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }

        private void SchemaFileContentData_Validated(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetCurrent(out DocumentValue? document))
            {
                document.StructuredFile.TryParseXml(out Exception? exception);
                errorProvider.SetError(structuredFileContentData.ErrorControl, exception);
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
        }


        private void ObjectPathData_SelectCommand(object sender, EventArgs e)
        {
            if (bindingDocument is not null
                && formBinding.ObjectData.TryGetCurrent(out DocumentObjectValue? value))
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());

                    dialog.BuildData(formBinding.ObjectData.Select(s => new PathIndex(new TemplateObjectIndex(s))));

                    if (dialog.ShowDialog(this) is DialogResult.OK)
                    {
                        foreach (INamedScopeValue item in dialog.SelectedByNamedScope())
                        {
                            if(value.ObjectScope is ScopeType.Null
                                && formBinding.DocumentData.TryGetCurrent(out DocumentValue? document)
                                && formBinding.SchemaData.TryGetCurrent(out SchemaDefinitionValue? schemaValue))
                            {
                                    document.DocumentTitle = item.Path.Member;
                                    document.StructuredFile.FileName = String.Concat(schemaValue.FilePrefix, item.Path.Member, schemaValue.FileSuffix, ".", schemaValue.FileExtension);
                            }

                            // TODO: add new, remove old, handle current
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

            if (formBinding.DocumentData.TryGetCurrent(out DocumentValue? document)
                && formBinding.OtherData.TryGetCurrent(out OtherBinding? other))
            {
                schemaBuildCommand.Enabled = other.IsObjectInModel
                    && new SchemaDefinitionIndex(document).HasValue;
            }
        }

        private void ObjectAddCommand_Click(object sender, EventArgs e)
        {

        }

        private void ObjectDeleteCommand_Click(object sender, EventArgs e)
        {

        }


        private void SchemaOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? other)
                && other.TryGetSchemaDirectory(out IDirectoryValue? directory)
                && other.TryGetSchemaFile(out IFileValue? file))
            { DoWork(openFileDialog.OpenDialog(directory, file), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                structuredFileNameData.ValidateChildren();
                structuredFileContentData.ValidateChildren();

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
            structuredFileContentData.ValidateChildren();
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

            if (formBinding.DocumentData.TryGetCurrent(out DocumentValue? document))
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
