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
    partial class DbTable : ApplicationBase<DbTableKey>
    {
        public DbTable() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Table;
        }

        private void DbTable_Load(object sender, EventArgs e)
        { BindData(); }

        protected override bool BindDataCore()
        {
            if (Program.Data.DbTables.FirstOrDefault(w => DataKey == new DbTableKey(w)) is DbTableItem data)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data, nameof(data.TableName)));
                tableTypeData.DataBindings.Add(new Binding(nameof(tableTypeData.Text), data, nameof(data.TableType)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data, nameof(data.IsSystem)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey).ToList();

                tableColumnsData.AutoGenerateColumns = false;
                tableColumnsData.DataSource = new BindingView<DbTableColumnItem>(Program.Data.DbTableColumns, w => DataKey.Equals(w));

                tableConstraintData.AutoGenerateColumns = false;
                tableConstraintData.DataSource = new BindingView<DbConstraintItem>(Program.Data.DbConstraints, w => DataKey.Equals(w));

                return true;
            }
            else { return false; }
        }

        protected override void UnbindDataCore()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            tableNameData.DataBindings.Clear();
            tableTypeData.DataBindings.Clear();
            isSystemData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
            tableColumnsData.DataSource = null;
        }

        private void tableColumnsData_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tableColumnsData.Rows[e.RowIndex].DataBoundItem is DbTableColumnItem columnItem)
            { Activate((data) => new Forms.Database.DbTableColumn() { DataKey = new DbTableColumnKey(columnItem) }, columnItem); }
        }
    }
}
