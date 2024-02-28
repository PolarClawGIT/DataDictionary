using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTableColumn : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingColumn.Current is IDbTableColumnItem current && ReferenceEquals(current, item); }

        public DbTableColumn() : base()
        {
            InitializeComponent();

            importDataCommand.Enabled = true;
            importDataCommand.Click += ImportDataCommand_Click;
            importDataCommand.ToolTipText = "Import the Table/View Column to the Domain Model";
        }

        public DbTableColumn(IDbTableColumnItem columnItem): this ()
        {
            DbTableColumnKeyName key = new DbTableColumnKeyName(columnItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            bindingColumn.DataSource = new BindingView<DbTableColumnItem>(BusinessData.DatabaseModel.DbTableColumns, w => key.Equals(w));
            bindingColumn.Position = 0;

            if (bindingColumn.Current is IDbTableColumnItem current)
            {
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();
                this.Icon = new ScopeKey(current).Scope.ToIcon();

                bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }

        }

        private void DbColumn_Load(object sender, EventArgs e)
        {
            IDbTableColumnItem bindingNames;
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

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingColumn.Current is not IDbTableColumnItem);
        }

        private void RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingRowState data)
            {
                RowState = data.RowState();
                if (IsHandleCreated)
                { this.Invoke(() => { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }); }
                else { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }
            }
        }


        private void ImportDataCommand_Click(object? sender, EventArgs e)
        {
            if (bindingColumn.Current is IDbTableItem current)
            {
                BusinessData.DomainModel.Attributes.Import(BusinessData.DatabaseModel, current);
            }
        }
    }
}
