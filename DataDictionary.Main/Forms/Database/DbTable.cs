using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTable : ApplicationBase, IApplicationDataForm<DbTableKey>
    {
        public required DbTableKey DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public DbTable() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Table;

            importDataCommand.DropDown = importOptions;
            importDataCommand.Enabled = true;
            importDataCommand.ButtonClick += ImportDataCommand_Click;
            importDataCommand.ToolTipText = "Import the Table/View to the Domain Model";
        }

        private void DbTable_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }

        public bool BindDataCore()
        {
            if (Program.Data.DbTables.FirstOrDefault(w => DataKey == new DbTableKey(w)) is DbTableItem data)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.DatabaseName)));
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

        public void UnbindDataCore()
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

        private void ImportDataCommand_Click(object? sender, EventArgs e)
        {

            List<WorkItem> work = new List<WorkItem>();

            if (Program.Data.DbTables.FirstOrDefault(w => DataKey.Equals(w)) is DbTableItem data)
            {
                if (importOptionEntity.Checked) { work.AddRange(Program.Data.ImportEntity(data)); }
                if (importOptionAttribute.Checked) { work.AddRange(Program.Data.ImportAttribute(data)); }

                SendMessage(new Messages.DoUnbindData());
                this.DoWork(work, onCompleting);
            }

            void onCompleting(RunWorkerCompletedEventArgs args)
            { SendMessage(new Messages.DoBindData()); }
        }
    }
}
