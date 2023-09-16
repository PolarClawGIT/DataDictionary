using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbTableColumn : ApplicationBase, IApplicationDataForm
    {
        public DbTableColumnKey DataKey { get; private set; }

        public Object? OpenItem { get; }

        public DbTableColumn() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbColumn;
            DataKey = new DbTableColumnKey(new DbTableColumnItem());
        }

        public DbTableColumn(IDbTableColumnItem columnItem) : this()
        {
            DataKey = new DbTableColumnKey(columnItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DbColumn_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            DbTableColumnItem? data = Program.Data.DbColumns.FirstOrDefault(w => DataKey.Equals(w));

            if (data is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data, nameof(data.TableName)));
                columnNameData.DataBindings.Add(new Binding(nameof(columnNameData.Text), data, nameof(data.ColumnName)));
                ordinalPositionData.DataBindings.Add(new Binding(nameof(ordinalPositionData.Text), data, nameof(data.OrdinalPosition)));
                columnDefaultData.DataBindings.Add(new Binding(nameof(columnDefaultData.Text), data, nameof(data.ColumnDefault)));
                columnComputedData.DataBindings.Add(new Binding(nameof(columnComputedData.Text), data, nameof(data.ComputedDefinition)));
                isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), data, nameof(data.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
                isComputedData.DataBindings.Add(new Binding(nameof(isComputedData.Checked), data, nameof(data.IsComputed), true, DataSourceUpdateMode.OnValidation, false));

                dataTypeData.DataBindings.Add(new Binding(nameof(columnNameData.Text), data, nameof(data.DataType)));
                characterMaximumLengthData.DataBindings.Add(new Binding(nameof(characterMaximumLengthData.Text), data, nameof(data.CharacterMaximumLength)));
                characterOctetLengthData.DataBindings.Add(new Binding(nameof(characterOctetLengthData.Text), data, nameof(data.CharacterOctetLength)));
                numericPrecisionData.DataBindings.Add(new Binding(nameof(numericPrecisionData.Text), data, nameof(data.NumericPrecision)));
                numericPrecisionRadixData.DataBindings.Add(new Binding(nameof(numericPrecisionRadixData.Text), data, nameof(data.NumericPrecisionRadix)));
                numericScaleData.DataBindings.Add(new Binding(nameof(numericScaleData.Text), data, nameof(data.NumericScale)));
                dateTimePrecisionData.DataBindings.Add(new Binding(nameof(dateTimePrecisionData.Text), data, nameof(data.DateTimePrecision)));

                characterSetCatalogData.DataBindings.Add(new Binding(nameof(characterSetCatalogData.Text), data, nameof(data.CharacterSetCatalog)));
                characterSetSchemaData.DataBindings.Add(new Binding(nameof(characterSetSchemaData.Text), data, nameof(data.CharacterSetSchema)));
                characterSetNameData.DataBindings.Add(new Binding(nameof(characterSetNameData.Text), data, nameof(data.CharacterSetName)));

                collationCatalogData.DataBindings.Add(new Binding(nameof(collationCatalogData.Text), data, nameof(data.CollationCatalog)));
                collationSchemaData.DataBindings.Add(new Binding(nameof(collationSchemaData.Text), data, nameof(data.CollationSchema)));
                collationNameData.DataBindings.Add(new Binding(nameof(collationNameData.Text), data, nameof(data.CollationName)));

                domainCatalogData.DataBindings.Add(new Binding(nameof(domainCatalogData.Text), data, nameof(data.CollationCatalog)));
                domainSchemaData.DataBindings.Add(new Binding(nameof(domainSchemaData.Text), data, nameof(data.DomainSchema)));
                domainNameData.DataBindings.Add(new Binding(nameof(domainNameData.Text), data, nameof(data.DomainName)));

                generatedAlwayTypeData.DataBindings.Add(new Binding(nameof(generatedAlwayTypeData.Text), data, nameof(data.GeneratedAlwayType)));
                isIdentityData.DataBindings.Add(new Binding(nameof(isIdentityData.Checked), data, nameof(data.IsIdentity), true, DataSourceUpdateMode.OnValidation, false));
                isHiddenData.DataBindings.Add(new Binding(nameof(isHiddenData.Checked), data, nameof(data.IsHidden), true, DataSourceUpdateMode.OnValidation, false));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.DbExtendedProperties.GetProperties(DataKey).ToList();
            }
        }

        void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            tableNameData.DataBindings.Clear();
            columnNameData.DataBindings.Clear();
            ordinalPositionData.DataBindings.Clear();
            columnDefaultData.DataBindings.Clear();
            columnComputedData.DataBindings.Clear();
            isComputedData.DataBindings.Clear();
            isNullableData.DataBindings.Clear();
            dataTypeData.DataBindings.Clear();
            characterMaximumLengthData.DataBindings.Clear();
            characterOctetLengthData.DataBindings.Clear();
            numericPrecisionData.DataBindings.Clear();
            numericPrecisionRadixData.DataBindings.Clear();
            numericScaleData.DataBindings.Clear();
            dateTimePrecisionData.DataBindings.Clear();
            characterSetCatalogData.DataBindings.Clear();
            characterSetSchemaData.DataBindings.Clear();
            characterSetNameData.DataBindings.Clear();
            collationCatalogData.DataBindings.Clear();
            collationSchemaData.DataBindings.Clear();
            collationNameData.DataBindings.Clear();
            domainCatalogData.DataBindings.Clear();
            domainNameData.DataBindings.Clear();
            generatedAlwayTypeData.DataBindings.Clear();
            isIdentityData.DataBindings.Clear();
            isHiddenData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
        }
        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
