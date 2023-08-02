﻿using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class DbTableColumn : ApplicationFormBase, IApplicationDataForm
    {
        class FormData
        {
            public DbTableColumnName ColumnName { get; set; } = new DbTableColumnName();
            public IDbTableColumnItem? DbColumn { get; set; }
            public BindingList<DbExtendedPropertyItem> DbExtendedProperties { get; set; } = new BindingList<DbExtendedPropertyItem>();

        }

        FormData data = new FormData();

        public Object? OpenItem { get; }

        public DbTableColumn() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbColumn;
        }

        public DbTableColumn(IDbTableColumnItem columnItem) : this()
        {
            data.ColumnName = new DbTableColumnName(columnItem);
            OpenItem = columnItem;
            this.Text = data.ColumnName.ToString();
        }

        private void DbColumn_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            data.DbColumn = Program.Data.DbColumns.FirstOrDefault(w => data.ColumnName == w);

            if (data.DbColumn is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbColumn, nameof(data.DbColumn.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbColumn, nameof(data.DbColumn.SchemaName)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data.DbColumn, nameof(data.DbColumn.TableName)));
                columnNameData.DataBindings.Add(new Binding(nameof(columnNameData.Text), data.DbColumn, nameof(data.DbColumn.ColumnName)));
                ordinalPositionData.DataBindings.Add(new Binding(nameof(ordinalPositionData.Text), data.DbColumn, nameof(data.DbColumn.OrdinalPosition)));
                columnDefaultData.DataBindings.Add(new Binding(nameof(columnDefaultData.Text), data.DbColumn, nameof(data.DbColumn.ColumnDefault)));
                columnComputedData.DataBindings.Add(new Binding(nameof(columnComputedData.Text), data.DbColumn, nameof(data.DbColumn.ComputedDefinition)));
                isNullableData.DataBindings.Add(new Binding(nameof(isNullableData.Checked), data.DbColumn, nameof(data.DbColumn.IsNullable), true, DataSourceUpdateMode.OnValidation, false));
                isComputedData.DataBindings.Add(new Binding(nameof(isComputedData.Checked), data.DbColumn, nameof(data.DbColumn.IsComputed), true, DataSourceUpdateMode.OnValidation, false));

                dataTypeData.DataBindings.Add(new Binding(nameof(columnNameData.Text), data.DbColumn, nameof(data.DbColumn.DataType)));
                characterMaximumLengthData.DataBindings.Add(new Binding(nameof(characterMaximumLengthData.Text), data.DbColumn, nameof(data.DbColumn.CharacterMaximumLength)));
                characterOctetLengthData.DataBindings.Add(new Binding(nameof(characterOctetLengthData.Text), data.DbColumn, nameof(data.DbColumn.CharacterOctetLength)));
                numericPrecisionData.DataBindings.Add(new Binding(nameof(numericPrecisionData.Text), data.DbColumn, nameof(data.DbColumn.NumericPrecision)));
                numericPrecisionRadixData.DataBindings.Add(new Binding(nameof(numericPrecisionRadixData.Text), data.DbColumn, nameof(data.DbColumn.NumericPrecisionRadix)));
                numericScaleData.DataBindings.Add(new Binding(nameof(numericScaleData.Text), data.DbColumn, nameof(data.DbColumn.NumericScale)));
                dateTimePrecisionData.DataBindings.Add(new Binding(nameof(dateTimePrecisionData.Text), data.DbColumn, nameof(data.DbColumn.DateTimePrecision)));

                characterSetCatalogData.DataBindings.Add(new Binding(nameof(characterSetCatalogData.Text), data.DbColumn, nameof(data.DbColumn.CharacterSetCatalog)));
                characterSetSchemaData.DataBindings.Add(new Binding(nameof(characterSetSchemaData.Text), data.DbColumn, nameof(data.DbColumn.CharacterSetSchema)));
                characterSetNameData.DataBindings.Add(new Binding(nameof(characterSetNameData.Text), data.DbColumn, nameof(data.DbColumn.CharacterSetName)));

                collationCatalogData.DataBindings.Add(new Binding(nameof(collationCatalogData.Text), data.DbColumn, nameof(data.DbColumn.CollationCatalog)));
                collationSchemaData.DataBindings.Add(new Binding(nameof(collationSchemaData.Text), data.DbColumn, nameof(data.DbColumn.CollationSchema)));
                collationNameData.DataBindings.Add(new Binding(nameof(collationNameData.Text), data.DbColumn, nameof(data.DbColumn.CollationName)));

                domainCatalogData.DataBindings.Add(new Binding(nameof(domainCatalogData.Text), data.DbColumn, nameof(data.DbColumn.CollationCatalog)));
                domainSchemaData.DataBindings.Add(new Binding(nameof(domainSchemaData.Text), data.DbColumn, nameof(data.DbColumn.DomainSchema)));
                domainNameData.DataBindings.Add(new Binding(nameof(domainNameData.Text), data.DbColumn, nameof(data.DbColumn.DomainName)));

                generatedAlwayTypeData.DataBindings.Add(new Binding(nameof(generatedAlwayTypeData.Text), data.DbColumn, nameof(data.DbColumn.GeneratedAlwayType)));
                isIdentityData.DataBindings.Add(new Binding(nameof(isIdentityData.Checked), data.DbColumn, nameof(data.DbColumn.IsIdentity), true, DataSourceUpdateMode.OnValidation, false));
                isHiddenData.DataBindings.Add(new Binding(nameof(isHiddenData.Checked), data.DbColumn, nameof(data.DbColumn.IsHidden), true, DataSourceUpdateMode.OnValidation, false));

                data.DbExtendedProperties.Clear();
                data.DbExtendedProperties.AddRange(Program.Data.DbExtendedProperties.GetProperties(data.DbColumn));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = data.DbExtendedProperties;
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