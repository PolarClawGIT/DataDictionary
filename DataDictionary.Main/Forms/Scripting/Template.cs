using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Main.Enumerations;
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
    partial class Template : ApplicationData
    {
        TemplateIndex templateIndex = new TemplateIndex();
        TemporalIndex? temporalIndex = null;

        public override Boolean IsOpenItem(object? item)
        { return item is ITemplateIndex key && templateIndex.Equals(key); }

        public Template() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.ScriptingTemplate);

            SetCommand(ScopeType.ScriptingTemplate,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);


            openObjectCommand.Image = ScopeType.ScriptingObject.GetImage(ButtonType.Open);
            openSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Open);
            openTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Open);
            addObjectCommand.Image = ScopeType.ScriptingObject.GetImage(ButtonType.Add);
            addSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Add);
            addTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Add);

            executeSchemaCommand.Image = ScopeType.ScriptingSchema.GetImage(ButtonType.Export);
            executeTransformCommand.Image = ScopeType.ScriptingTransform.GetImage(ButtonType.Export);

            openDocumentCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);

        }

        public Template(ITemplateIndex template) : this()
        { templateIndex = new TemplateIndex(template); }

        public Template(ITemplateIndex template, ITemporalIndex temporal) : this(template)
        { temporalIndex = new TemporalIndex(); }

        private void Template_Load(object sender, EventArgs e)
        {

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





        private void ContextTemplate_Opening(object sender, CancelEventArgs e)
        {
            // TODO: Not Needed
        }



        private void AddObjectCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenObjectCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.TemplateObject());
        }

        private void AddSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenSchemaCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.SchemaDefinition());
        }

        private void ExecuteSchemaCommand_Click(object sender, EventArgs e)
        {

        }

        private void AddTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenTransformCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.Transform());
        }

        private void ExecuteTransformCommand_Click(object sender, EventArgs e)
        {

        }

        private void OpenDocumentCommand_Click(object sender, EventArgs e)
        {
            // TODO: Added data
            Activate(static () => new Forms.Scripting.Document());
        }
    }
}
