using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DataDictionary.Main.Forms
{
    partial class DbColumn : ApplicationFormBase
    {
        class FormData
        {
            public DbColumnName ColumnName { get; set; } = new DbColumnName();
            public IDbColumnItem? DbColumn { get; set; }
            public BindingList<DbExtendedPropertyItem> DbExtendedProperties { get; set; } = new BindingList<DbExtendedPropertyItem>();

        }
        FormData data = new FormData();

        public DbColumn() : base()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new FormAddMdiChild() { ChildForm = this });
        }

        public DbColumn(IDbColumnItem columnItem) : this()
        {
            data.ColumnName = new DbColumnName(columnItem);
            this.Text = String.Format("{0}: {1}", this.Text, data.ColumnName.ToString());
        }

        private void DbColumn_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            data.DbColumn = Program.DbData.DbColumns.FirstOrDefault(w => data.ColumnName == w);

            if (data.DbColumn is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbColumn, nameof(data.DbColumn.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbColumn, nameof(data.DbColumn.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data.DbColumn, nameof(data.DbColumn.ObjectName)));
                columnNameData.DataBindings.Add(new Binding(nameof(columnNameData.Text), data.DbColumn, nameof(data.DbColumn.ColumnName)));

                data.DbExtendedProperties.Clear();
                data.DbExtendedProperties.AddRange(Program.DbData.DbExtendedProperties.Where(
                        w =>
                        w.CatalogName == data.DbColumn.CatalogName &&
                        w.Level0Name == data.DbColumn.SchemaName &&
                        w.Level1Name == data.DbColumn.ObjectName &&
                        w.Level2Name == data.DbColumn.ColumnName &&
                        w.PropertyObjectType == ExtendedPropertyObjectType.Column));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = data.DbExtendedProperties;
            }
            else { errorProvider.SetError(catalogNameLayout, "Schmema information not avaiable"); }

        }

        void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            tableNameData.DataBindings.Clear();
            columnNameData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
        }
        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
