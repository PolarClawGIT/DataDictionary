using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
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
    partial class Document : ApplicationData
    {
        DocumentIndex documentIndex = new DocumentIndex();
        TemporalIndex? temporalIndex = null;

        public override Boolean IsOpenItem(object? item)
        { return item is IDocumentIndex key && documentIndex.Equals(key); }

        public Document() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.ScriptingDocument);

            SetCommand(ScopeType.ScriptingDocument,
                Enumerations.CommandType.Delete,
                Enumerations.CommandType.OpenDatabase,
                Enumerations.CommandType.SaveDatabase,
                Enumerations.CommandType.DeleteDatabase,
                Enumerations.CommandType.HistoryDatabase);

        }

        public Document(IDocumentIndex document) : this()
        { documentIndex = new DocumentIndex(document); }

        public Document(IDocumentIndex document, ITemporalIndex temporal) : this(document)
        { temporalIndex = new TemporalIndex(); }

        private void Document_Load(object sender, EventArgs e)
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
    }
}
