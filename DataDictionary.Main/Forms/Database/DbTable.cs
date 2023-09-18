using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
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

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTable : ApplicationBase, IApplicationDataForm
    {
        public DbTableKey DataKey { get; private set; }

        public DbTable() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Table;
            DataKey = new DbTableKey(new DbTableItem());
        }

        public DbTable(IDbTableKey tableItem) : this()
        {
            DataKey = new DbTableKey(tableItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DbTable_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            DbTableItem? data = Program.Data.DbTables.FirstOrDefault(w => DataKey == new DbTableKey(w));

            if (data is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data, nameof(data.TableName)));
                tableTypeData.DataBindings.Add(new Binding(nameof(tableTypeData.Text), data, nameof(data.TableType)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data, nameof(data.IsSystem)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.DbExtendedProperties.GetProperties(DataKey).ToList();

                tableColumnsData.AutoGenerateColumns = false;
                tableColumnsData.DataSource = new BindingView<DbTableColumnItem>(Program.Data.DbTableColumns, w => DataKey.Equals(w));

                tableConstraintData.AutoGenerateColumns = false;
                tableConstraintData.DataSource = new BindingView<DbConstraintItem>(Program.Data.DbConstraints, w => DataKey.Equals(w));
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
            { Activate((data) => new Forms.Database.DbTableColumn(columnItem), columnItem); }
        }
    }
}
