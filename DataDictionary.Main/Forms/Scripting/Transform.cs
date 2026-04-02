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
    partial class Transform : ApplicationData
    {
        TransformIndex transformIndex = new TransformIndex();
        TemporalIndex? temporalIndex = null;

        public override Boolean IsOpenItem(object? item)
        { return item is ITransformIndex key && transformIndex.Equals(key); }

        public Transform() : base()
        {
            InitializeComponent();

            SetIcon(ScopeType.ScriptingTransform);

            SetCommand(ScopeType.ScriptingTransform,
                Enumerations.ButtonType.Delete,
                Enumerations.ButtonType.OpenDatabase,
                Enumerations.ButtonType.SaveDatabase,
                Enumerations.ButtonType.DeleteDatabase,
                Enumerations.ButtonType.HistoryDatabase);

        }

        public Transform(ITransformIndex transform) : this()
        { transformIndex = new TransformIndex(transform); }

        public Transform(ITransformIndex transform, ITemporalIndex temporal) : this(transform)
        { temporalIndex = new TemporalIndex(); }

        private void Transform_Load(object sender, EventArgs e)
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
