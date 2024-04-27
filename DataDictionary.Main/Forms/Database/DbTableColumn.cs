using DataDictionary.BusinessLayer.Database;
using DataDictionary.Main.Controls;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTableColumn : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingColumn.Current is ITableColumnValue current && ReferenceEquals(current, item); }

        public DbTableColumn() : base()
        {
            InitializeComponent();
            toolStrip.TransferItems(tableColumnToolStrip, 0);
        }

        public DbTableColumn(ITableColumnValue columnItem) : this()
        {
<<<<<<< HEAD
            TableColumnKeyName key = new TableColumnKeyName(columnItem);
=======
            TableColumnIndexName key = new TableColumnIndexName(columnItem);
>>>>>>> RenameIndexValue
            ExtendedPropertyIndexName propertyKey = new ExtendedPropertyIndexName(key);

            bindingColumn.DataSource = new BindingView<TableColumnValue>(BusinessData.DatabaseModel.DbTableColumns, w => key.Equals(w));
            bindingColumn.Position = 0;

            Setup(bindingColumn);

            if (bindingColumn.Current is ITableColumnValue current)
            {
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }

        }

        private void DbColumn_Load(object sender, EventArgs e)
        {
            ITableColumnValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingColumn, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingColumn, nameof(bindingNames.SchemaName)));
            tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), bindingColumn, nameof(bindingNames.TableName)));
            columnNameData.DataBindings.Add(new Binding(nameof(columnNameData.Text), bindingColumn, nameof(bindingNames.ColumnName)));
            ordinalPositionData.DataBindings.Add(new Binding(nameof(ordinalPositionData.Text), bindingColumn, nameof(bindingNames.OrdinalPosition)));
            columnDefaultData.DataBindings.Add(new Binding(nameof(columnDefaultData.Text), bindingColumn, nameof(bindingNames.ColumnDefault)));
            columnComputedData.DataBindings.Add(new Binding(nameof(columnComputedData.Text), bindingColumn, nameof(bindingNames.ComputedDefinition)));
            isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), bindingColumn, nameof(bindingNames.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
            isComputedData.DataBindings.Add(new Binding(nameof(isComputedData.Checked), bindingColumn, nameof(bindingNames.IsComputed), true, DataSourceUpdateMode.OnValidation, false));

            dataTypeData.DataBindings.Add(new Binding(nameof(columnNameData.Text), bindingColumn, nameof(bindingNames.DataType)));
            characterMaximumLengthData.DataBindings.Add(new Binding(nameof(characterMaximumLengthData.Text), bindingColumn, nameof(bindingNames.CharacterMaximumLength)));
            characterOctetLengthData.DataBindings.Add(new Binding(nameof(characterOctetLengthData.Text), bindingColumn, nameof(bindingNames.CharacterOctetLength)));
            numericPrecisionData.DataBindings.Add(new Binding(nameof(numericPrecisionData.Text), bindingColumn, nameof(bindingNames.NumericPrecision)));
            numericPrecisionRadixData.DataBindings.Add(new Binding(nameof(numericPrecisionRadixData.Text), bindingColumn, nameof(bindingNames.NumericPrecisionRadix)));
            numericScaleData.DataBindings.Add(new Binding(nameof(numericScaleData.Text), bindingColumn, nameof(bindingNames.NumericScale)));
            dateTimePrecisionData.DataBindings.Add(new Binding(nameof(dateTimePrecisionData.Text), bindingColumn, nameof(bindingNames.DateTimePrecision)));

            characterSetCatalogData.DataBindings.Add(new Binding(nameof(characterSetCatalogData.Text), bindingColumn, nameof(bindingNames.CharacterSetCatalog)));
            characterSetSchemaData.DataBindings.Add(new Binding(nameof(characterSetSchemaData.Text), bindingColumn, nameof(bindingNames.CharacterSetSchema)));
            characterSetNameData.DataBindings.Add(new Binding(nameof(characterSetNameData.Text), bindingColumn, nameof(bindingNames.CharacterSetName)));

            collationCatalogData.DataBindings.Add(new Binding(nameof(collationCatalogData.Text), bindingColumn, nameof(bindingNames.CollationCatalog)));
            collationSchemaData.DataBindings.Add(new Binding(nameof(collationSchemaData.Text), bindingColumn, nameof(bindingNames.CollationSchema)));
            collationNameData.DataBindings.Add(new Binding(nameof(collationNameData.Text), bindingColumn, nameof(bindingNames.CollationName)));

            domainCatalogData.DataBindings.Add(new Binding(nameof(domainCatalogData.Text), bindingColumn, nameof(bindingNames.CollationCatalog)));
            domainSchemaData.DataBindings.Add(new Binding(nameof(domainSchemaData.Text), bindingColumn, nameof(bindingNames.DomainSchema)));
            domainNameData.DataBindings.Add(new Binding(nameof(domainNameData.Text), bindingColumn, nameof(bindingNames.DomainName)));

            generatedAlwayTypeData.DataBindings.Add(new Binding(nameof(generatedAlwayTypeData.Text), bindingColumn, nameof(bindingNames.GeneratedAlwayType)));
            isIdentityData.DataBindings.Add(new Binding(nameof(isIdentityData.Checked), bindingColumn, nameof(bindingNames.IsIdentity), true, DataSourceUpdateMode.OnValidation, false));
            isHiddenData.DataBindings.Add(new Binding(nameof(isHiddenData.Checked), bindingColumn, nameof(bindingNames.IsHidden), true, DataSourceUpdateMode.OnValidation, false));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingColumn.Current is not ITableColumnValue);
        }

        private void exportCommand_Click(object sender, EventArgs e)
        {
            if (bindingColumn.Current is ITableColumnValue current)
            {   BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, BusinessData.ApplicationData.Properties, current); }
        }
    }
}
