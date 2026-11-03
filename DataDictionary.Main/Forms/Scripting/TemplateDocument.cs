using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
using DataDictionary.Main.Dialogs;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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

                formBinding.OtherData.AddBinding(schemaFileLocationData, e => e.SchemaDirectory.InitialDirectory);

                ScopeNameList.Load(objectScopeData);
                formBinding.DocumentData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);
                formBinding.DocumentData.AddBinding(objectPathData, e => e.ObjectPath);
                formBinding.OtherData.AddBinding(isObjectInModelData, e => e.IsObjectInModel);

                formBinding.DocumentData.AddBinding(schemaFileNameData, e => e.DataFile.FileName);
                formBinding.DocumentData.AddBinding(schemaFileContentData, e => e.DataFile.FileContent);

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

        private void DocumentOpenSchemaCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.SchemaDocument());
        }

        private void DocumentOpenTransformCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.TransformDocument());
        }

        private void SchemaIdData_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(schemaIdData.ErrorControl, String.Empty);
            schemaFileLocationData.Text = String.Empty;

            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                SchemaDefinitionIndex key = new SchemaDefinitionIndex(document);

                if (key.HasValue)
                { formBinding.SchemaData.TrySetCurrent(w => key.Equals(w)); }
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
                && formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? fileValue))
            {
                using (SelectionDialog dialog = new SelectionDialog(this))
                {
                    dialog.MultiSelect = false;
                    dialog.FilterScopes.AddRange(XmlBuilder.SupportedScopes());

                    dialog.BuildData(new List<PathItem>() { new PathItem(fileValue.ObjectPath) });

                    if (dialog.ShowDialog(this) is DialogResult.OK
                        && dialog.SelectedByNamedScope().TryGetSingle(out INamedScopeValue? selected))
                    {
                        fileValue.DocumentTitle = selected.Path.Member;
                        fileValue.ObjectPath = selected.Path.MemberFullPath;
                        fileValue.ObjectScope = selected.Scope;

                        if (formBinding.SchemaData.TryGetSingle(out SchemaDefinitionValue? schemaValue))
                        { fileValue.DataFileName = String.Concat(schemaValue.FilePrefix, selected.Path.Member, schemaValue.FileSuffix, ".", schemaValue.FileExtension); }
                    }
                }
            }
        }

        private void SchemaOpenCommand_Click(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? directory)
                && formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            { DoWork(openFileDialog.OpenDialog(directory.SchemaDirectory, document.DataFile), onCompleting); }

            void onCompleting(RunWorkerCompletedEventArgs args)
            {
                schemaFileNameData.ValidateChildren();
                schemaFileContentData.ValidateChildren();

                if (args.Error is not null)
                { throw args.Error; }
            }
        }

        private void BindingOther_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.OtherData.TryGetCurrent(out OtherBinding? directory))
            {
                directory.SchemaDirectory.IsValid(out Exception? schema);
                schemaOpenCommand.Enabled = schema is null;
                schemaSaveCommand.Enabled = schema is null;
                errorProvider.SetError(schemaFileLocationData.ErrorControl, schema);

                directory.TransformDirectory.IsValid(out Exception? transform);
                transformOpenCommand.Enabled = transform is null;
                transformSaveCommand.Enabled = transform is null;
                errorProvider.SetError(transformFileLocationData.ErrorControl, transform);

                directory.ScriptedDirectory.IsValid(out Exception? scripted);
                scriptedOpenCommand.Enabled = transform is null;
                scriptedSaveCommand.Enabled = transform is null;
                errorProvider.SetError(scriptedFileLocationData.ErrorControl, transform);
            }
            else { throw ThrowDataNotFound(sender); } // Should never occur
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
