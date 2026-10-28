using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Controls.ComboBoxList;
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

            formBinding = new FormBinding(bindingTemplate, bindingSchema, bindingTransform, bindingDocument);
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

            if (document is IDocumentIndex key)
            { documentIndex = new DocumentIndex(key); }
        }

        public TemplateDocument(IDocumentIndex document, Func<ITemplateData> getData) : this()
        {
            documentIndex = new DocumentIndex(document);
            //formBinding.GetData = getData;
        }

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
                SchemaDefinitionList.Load(schemaIdData, formBinding.SchemaData.AsBindingList(), "none");
                formBinding.DocumentData.AddBinding(schemaIdData, e => e.SchemaId, SchemaDefinitionList.NullValue);

                ScopeNameList.Load(objectScopeData);
                formBinding.DocumentData.AddBinding(objectScopeData, e => e.ObjectScope, ScopeNameList.NullValue);
                formBinding.DocumentData.AddBinding(objectPathData, e => e.ObjectPath);

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
            dataFileLocationData.Text = String.Empty;

            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                SchemaDefinitionIndex key = new SchemaDefinitionIndex(document);
                if (key.HasValue && formBinding.SchemaData.TrySetCurrent(w => key.Equals(w), out SchemaDefinitionValue? schema))
                { dataFileLocationData.Text = schema.InitialDirectory; }
                else { errorProvider.SetError(schemaIdData.ErrorControl, "schema not found"); }
            }
            else { errorProvider.SetError(schemaIdData.ErrorControl, "document not found"); } // Should never occur
        }

        private void SchemaFileNameData_Validated(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                document.ScriptedFile.IsValid(out Exception? exception);
                errorProvider.SetError(schemaFileNameData.ErrorControl, exception);
            }
            else { errorProvider.SetError(schemaIdData.ErrorControl, "document not found"); } // Should never occur
        }

        private void SchemaFileContentData_Validated(object sender, EventArgs e)
        {
            if (formBinding.DocumentData.TryGetCurrent(out TemplateDocumentValue? document))
            {
                var x = schemaFileContentData.Text;
                var y = document.DataFile.FileContent;

                document.DataFile.TryParseXml(out Exception? exception);
                errorProvider.SetError(schemaFileContentData.ErrorControl, exception);
            }
            else { errorProvider.SetError(schemaIdData.ErrorControl, "document not found"); } // Should never occur
        }
    }
}
