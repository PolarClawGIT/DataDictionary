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
    partial class SchemaDefinition : ApplicationData
    {
        SchemaDefinitionIndex schemaIndex = new SchemaDefinitionIndex();
        TemporalIndex? temporalIndex = null;

        public override Boolean IsOpenItem(object? item)
        { return item is ISchemaDefinitionIndex key && schemaIndex.Equals(key); }

        public SchemaDefinition() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.ScriptingSchema);

            SetCommand(ScopeType.ScriptingSchema,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);

            documentNewCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Add);
            documentOpenCommand.Image = ScopeType.ScriptingDocument.GetImage(ButtonType.Open);

        }

        public SchemaDefinition(ISchemaDefinitionIndex schema) : this()
        { schemaIndex = new SchemaDefinitionIndex(schema); }

        public SchemaDefinition(ISchemaDefinitionIndex schema, ITemporalIndex temporal) : this(schema)
        { temporalIndex = new TemporalIndex(); }

        private void SchemaDefinition_Load(object sender, EventArgs e)
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

        private void DocumentNewCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.SchemaDocument());
        }

        private void DocumentOpenCommand_Click(object sender, EventArgs e)
        {
            // TODO: Add Data
            Activate(static () => new Forms.Scripting.SchemaDocument());
        }
    }
}
