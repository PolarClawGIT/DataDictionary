using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class DbTable : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTable.Current is ITableValue current && ReferenceEquals(current, item); }

        protected DbTable() : base()
        {
            InitializeComponent();

            SetRowState(
                bindingTable, 
                bindingColumns, 
                bindingConstraints, 
                bindingProperties, 
                bindingDependencies);
            SetTitle(bindingTable);
            SetCommand(ScopeType.DatabaseTable, CommandImageType.Export);

            CommandButtons[CommandImageType.Export].Text = "to Model";
            CommandButtons[CommandImageType.Export].DropDown = exportOptions;

            exportEntites.Image = NavigationEnumeration.GetImage(ScopeType.ModelEntity, CommandImageType.Add);
        }

        public DbTable(ITableValue tableItem) : this()
        {
            TableIndexName key = new TableIndexName(tableItem);

            bindingTable.DataSource = new BindingView<TableValue>(BusinessData.CatalogModel.DbTables, w => key.Equals(w));
            bindingTable.Position = 0;

            if (bindingTable.Current is ITableValue current)
            {
                ReferenceIndexName referenceName = new ReferenceIndexName(current);
                PropertyIndexObject propertyKey = new PropertyIndexObject(current);
                bindingColumns.DataSource = new BindingView<TableColumnValue>(BusinessData.CatalogModel.DbTableColumns, w => key.Equals(w));
                bindingConstraints.DataSource = new BindingView<ConstraintValue>(BusinessData.CatalogModel.DbConstraints, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<BusinessLayer.AppCatalog.PropertyValue>(BusinessData.CatalogModel.DbProperties, w => propertyKey.Equals(w));
                bindingDependencies.DataSource = new BindingView<ReferenceValue>(BusinessData.CatalogModel.DbReferences, w => referenceName.Equals(w));
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

            dependenciesData.AutoGenerateColumns = false;
            dependenciesData.DataSource = bindingDependencies;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingTable.Current is not ITableValue);
        }

        private void ExportEntites_Click(object sender, EventArgs e)
        {
            if (bindingTable.Current is TableValue current)
            {
                TableEntity tableEntity = new TableEntity(current)
                {
                    GetAlias = BusinessData.CatalogModel.DbTables.GetAlias,
                    GetCatalogProperty = BusinessData.CatalogModel.DbProperties.GetProperty,
                    GetModelProperty = BusinessData.Model.Properties.GetProperty,
                    GetColumns = BusinessData.CatalogModel.DbTables.GetColumns
                };

                IEntityValue entity = BusinessData.Model.Entities.Import(tableEntity);

                Activate(() => new Forms.Domain.DomainEntity(entity));
                SendMessage(new RefreshNavigation());
            }
        }
    }
}
