using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
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

namespace DataDictionary.Main.Forms
{
    partial class DbTable : ApplicationFormBase
    {
        class FormData
        {
            public DbTableName TableName { get; set; } = new DbTableName();
            public IDbTableItem? DbTable { get; set; }
            public BindingList<DbExtendedPropertyItem> DbExtendedProperties { get; set; } = new BindingList<DbExtendedPropertyItem>();
            public BindingList<DbColumnItem> DbColumn { get; set; } = new BindingList<DbColumnItem>();
        }
        FormData data = new FormData();

        public DbTable() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbTable;
        }

        public DbTable(IDbTableItem tableItem) : this()
        {
            data.TableName = new DbTableName(tableItem);
            this.Text = String.Format("{0}: {1}", this.Text, data.TableName.ToString());
        }

        private void DbTable_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            data.DbTable = Program.DbData.DbTables.FirstOrDefault(w => data.TableName == w);

            if (data.DbTable is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbTable, nameof(data.DbTable.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbTable, nameof(data.DbTable.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data.DbTable, nameof(data.DbTable.TableName)));
                tableTypeData.DataBindings.Add(new Binding(nameof(tableTypeData.Text), data.DbTable, nameof(data.DbTable.TableType)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data.DbTable, nameof(data.DbTable.IsSystem)));

                data.DbExtendedProperties.Clear();
                data.DbExtendedProperties.AddRange(Program.DbData.DbExtendedProperties.GetProperties(data.DbTable));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = data.DbExtendedProperties;

                data.DbColumn.Clear();
                data.DbColumn.AddRange(Program.DbData.DbColumns.Where(w => data.TableName == w));

                tableColumnsData.DataSource = data.DbColumn;
            }
        }

        void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            tableNameData.DataBindings.Clear();
            tableTypeData.DataBindings.Clear();
            isSystemData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
            tableColumnsData.DataSource = null;
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
