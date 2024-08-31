using DataDictionary.BusinessLayer.Database;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Enumerations;
using DataDictionary.Main.Messages;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTable : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingTable.Current is ITableValue current && ReferenceEquals(current, item); }

        protected DbTable() : base()
        { InitializeComponent(); }

        public DbTable(ITableValue tableItem) : this()
        {
            TableIndexName key = new TableIndexName(tableItem);

            bindingTable.DataSource = new BindingView<TableValue>(BusinessData.DatabaseModel.DbTables, w => key.Equals(w));
            bindingTable.Position = 0;

            Setup(bindingTable, CommandImageType.Export);
            CommandButtons[CommandImageType.Export].Text = "to Model";
            CommandButtons[CommandImageType.Export].DropDown = exportOptions;
            exportAll.Image = ImageEnumeration.GetImage(ScopeType.Model, CommandImageType.Add);
            exportAttributes.Image = ImageEnumeration.GetImage(ScopeType.ModelAttribute, CommandImageType.Add);
            exportEntites.Image = ImageEnumeration.GetImage(ScopeType.ModelEntity, CommandImageType.Add);

            if (bindingTable.Current is ITableValue current)
            {
                ReferenceIndexName referenceName = new ReferenceIndexName(current);
                ExtendedPropertyIndexName propertyKey = new ExtendedPropertyIndexName(current);
                bindingColumns.DataSource = new BindingView<TableColumnValue>(BusinessData.DatabaseModel.DbTableColumns, w => key.Equals(w));
                bindingConstraints.DataSource = new BindingView<ConstraintValue>(BusinessData.DatabaseModel.DbConstraints, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
                bindingDependencies.DataSource = new BindingView<ReferenceValue>(BusinessData.DatabaseModel.DbReferences, w => referenceName.Equals(w));
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

        private void ExportAll_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingTable.Current is ITableValue current)
            //{
            //    BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            //    BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportEntites_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingTable.Current is ITableValue current)
            //{
            //    BusinessData.DomainModel.Entities.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }

        private void ExportAttributes_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //if (bindingTable.Current is ITableValue current)
            //{
            //    BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current);
            //    SendMessage(new RefreshNavigation());
            //}
        }
    }
}
