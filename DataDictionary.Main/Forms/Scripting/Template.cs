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
                Enumerations.CommandType.Delete,
                Enumerations.CommandType.OpenDatabase,
                Enumerations.CommandType.SaveDatabase,
                Enumerations.CommandType.DeleteDatabase,
                Enumerations.CommandType.HistoryDatabase);

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
    }
}
