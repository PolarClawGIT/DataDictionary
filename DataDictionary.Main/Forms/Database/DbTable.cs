using DataDictionary.BusinessLayer.Database;
using DataDictionary.Main.Controls;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTable : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTable.Current is ITableValue current && ReferenceEquals(current, item); }

        public DbTable() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(tableToolStrip, 0);
        }

        public DbTable(ITableValue tableItem) : this()
        {
<<<<<<< HEAD
            TableKeyName key = new TableKeyName(tableItem);
=======
            TableIndexName key = new TableIndexName(tableItem);
>>>>>>> RenameIndexValue
            ExtendedPropertyIndexName propertyKey = new ExtendedPropertyIndexName(key);

            bindingTable.DataSource = new BindingView<TableValue>(BusinessData.DatabaseModel.DbTables, w => key.Equals(w));
            bindingTable.Position = 0;

            Setup(bindingTable);

            if (bindingTable.Current is ITableValue current)
            {
                bindingColumns.DataSource = new BindingView<TableColumnValue>(BusinessData.DatabaseModel.DbTableColumns, w => key.Equals(w));
                bindingConstraints.DataSource = new BindingView<ConstraintValue>(BusinessData.DatabaseModel.DbConstraints, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbTable_Load(object sender, EventArgs e)
        {
            ITableValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingTable, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingTable, nameof(bindingNames.SchemaName)));
            tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), bindingTable, nameof(bindingNames.TableName)));
            tableTypeData.DataBindings.Add(new Binding(nameof(tableTypeData.Text), bindingTable, nameof(bindingNames.TableType)));
            isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), bindingTable, nameof(bindingNames.IsSystem)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            tableColumnsData.AutoGenerateColumns = false;
            tableColumnsData.DataSource = bindingColumns;

            tableConstraintData.AutoGenerateColumns = false;
            tableConstraintData.DataSource = bindingConstraints;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingTable.Current is not ITableValue);
        }

        private void exportCommand_Click(object sender, EventArgs e)
        {
            if (bindingTable.Current is ITableValue current)
            {
                BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
                BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            }
        }
    }
}
