using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
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

namespace DataDictionary.Main.Forms.Scripting
{
    partial class TransformDocument : ApplicationData
    {
        DocumentIndex documentIndex = new DocumentIndex();
        TransformIndex transformIndex = new TransformIndex();
        TemporalIndex? temporalIndex = null;
        FormBinding formBinding;

        public override Boolean IsOpenItem(object? item)
        { return item is IDocumentIndex key && documentIndex.Equals(key); }

        public TransformDocument()
        {
            InitializeComponent();

            formBinding = new FormBinding(bindingTemplate, bindingTransform, bindingDocument);

            SetRowState(bindingDocument);
            SetTitle(bindingDocument);
            SetIcon(bindingDocument);

            selectDataFileCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Select);
            reloadDataFileCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Refresh);
        }

        public TransformDocument(IDocumentIndex document, Func<ITemplateData> getData) : this()
        {
            documentIndex = new DocumentIndex(document);
            formBinding.GetData = getData;
        }

        public TransformDocument(ITransformIndex transform, Func<ITemplateData> getData) : this()
        {
            transformIndex = new TransformIndex(transform);
            formBinding.GetData = getData;
        }

        private void TransformDocument_Load(object sender, EventArgs e)
        {
            if (documentIndex.HasValue)
            {
                formBinding.LoadValue(documentIndex);
                DoBinding();
            }
            else if (formBinding.TryCreate(transformIndex, ref documentIndex, out Exception? exception))
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
                formBinding.TransformData.AddBinding(transformTitleData, e => e.TransformTitle);

                dataFileNameData.ValueMember = nameof(ISchemaDocumentValue.DataFileName);
                dataFileNameData.DataSource = formBinding.SchemaDocumentData;

                // Special Handling for FK related values.
                dataFileNameData.ValueMember = nameof(ISchemaDocumentValue.DataFileName);
                dataFileNameData.DataSource = formBinding.SchemaDocumentData;
                formBinding.DocumentData.AddBinding(dataFileNameData, e => e.DataFileName, String.Empty);

                if (formBinding.TryGetSchemaDirectory(out IDirectoryValue? directory))
                { schemaDocumentPathData.Text = directory.InitialDirectory; }
                else { schemaDocumentPathData.Text = String.Empty; }

                if (formBinding.TryGetSchemaDocument(out SchemaDocumentValue? schemaDocument))
                {
                    schemaSourceObjectData.Text = schemaDocument.ObjectPath ?? String.Empty;
                    sourceDocumentData.Text = schemaDocument.FileContent;
                }
                else
                {
                    schemaSourceObjectData.Text = String.Empty;
                    sourceDocumentData.Text = String.Empty;
                }



                //formBinding.SchemaDocumentData.AddBinding(schemaSourceObjectData, e => e.ObjectPath);
                //formBinding.SchemaDocumentData.AddBinding(sourceDocumentData, e => e.FileContent);

                // Security
                ValidateChildren();
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

        private void SelectDataFileCommand_Click(object sender, EventArgs e)
        {

        }

        private void ReloadDataFileCommand_Click(object sender, EventArgs e)
        {

        }

        private void BindingSchemaDocument_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void BindingTransform_CurrentItemChanged(object sender, EventArgs e)
        {
            if (formBinding.TryGetSchemaDirectory(out IDirectoryValue? directory))
            { schemaDocumentPathData.Text = directory.InitialDirectory; }
            else { schemaDocumentPathData.Text = String.Empty; }

        }

        private void DataFileNameData_Validated(object sender, EventArgs e)
        {
            if (formBinding.TryGetSchemaDocument(out SchemaDocumentValue? schemaDocument))
            {
                schemaSourceObjectData.Text = schemaDocument.ObjectPath ?? String.Empty;
                sourceDocumentData.Text = schemaDocument.FileContent;
            }
            else
            {
                schemaSourceObjectData.Text = String.Empty;
                sourceDocumentData.Text = String.Empty;
            }
        }
    }
}
