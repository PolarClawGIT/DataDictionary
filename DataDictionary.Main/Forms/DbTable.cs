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
    partial class DbTable : ApplicationBase, IApplicationDataForm
    {
        class FormData
        {
            public DbTableKey? TableKey { get; set; }
            public IDbTableItem? DbTable { get; set; }
        }
        FormData data = new FormData();

        public Object? OpenItem { get; }

        public DbTable() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbTable;
        }

        public DbTable(IDbTableItem tableItem) : this()
        {
            data.TableKey = new DbTableKey(tableItem);
            OpenItem = tableItem;
            this.Text = data.TableKey.ToString();
        }

        private void DbTable_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            if (data.TableKey is not null)
            { data.DbTable = Program.Data.DbTables.FirstOrDefault(w => data.TableKey == new DbTableKey(w)); }

            if (data.TableKey is not null && data.DbTable is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbTable, nameof(data.DbTable.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbTable, nameof(data.DbTable.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data.DbTable, nameof(data.DbTable.TableName)));
                tableTypeData.DataBindings.Add(new Binding(nameof(tableTypeData.Text), data.DbTable, nameof(data.DbTable.TableType)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data.DbTable, nameof(data.DbTable.IsSystem)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.DbExtendedProperties.GetProperties(data.DbTable);

                tableColumnsData.AutoGenerateColumns = false;
                tableColumnsData.DataSource = new BindingView<DbTableColumnItem>(Program.Data.DbColumns, w => new DbTableKey(w).Equals(data.TableKey));

                tableConstraintData.AutoGenerateColumns = false;
                tableConstraintData.DataSource = new BindingView<DbConstraintItem>(Program.Data.DbConstraints, w => new DbTableKey(w).Equals(data.TableKey));
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

        private void tableColumnsData_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tableColumnsData.Rows[e.RowIndex].DataBoundItem is DbTableColumnItem columnItem)
            { Activate((data) => new Forms.DbTableColumn(columnItem), columnItem); }
        }
    }
}
