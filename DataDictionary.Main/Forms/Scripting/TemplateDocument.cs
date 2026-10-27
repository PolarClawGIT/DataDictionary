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

            formBinding = new FormBinding();
            SetIcon(ScopeType.ScriptingDocument);

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
    }
}
