using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Mediator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DataDictionary.Main.Forms
{
    partial class DbColumn : ApplicationFormBase
    {
        class FormData
        {
            public DbColumnName ColumnName { get; set; } = new DbColumnName();
            public IDbColumnItem? DbColumn { get; set; }
            public BindingList<DbExtendedPropertyItem> DbExtendedProperties { get; set; } = new BindingList<DbExtendedPropertyItem>();

        }
        FormData data = new FormData();

        public DbColumn() : base()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new FormAddMdiChild() { ChildForm = this });
        }

        public DbColumn(IDbColumnItem columnItem) : this()
        {
            data.ColumnName = new DbColumnName(columnItem);
            this.Text = String.Format("{0}: {1}", this.Text, data.ColumnName.ToString());
        }

        private void DbColumn_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            data.DbColumn = Program.DbData.DbColumns.FirstOrDefault(w => data.ColumnName == w);

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

                generatedAlwayTypeData.DataBindings.Add(new Binding(nameof(generatedAlwayTypeData.Text), data.DbColumn, nameof(data.DbColumn.GeneratedAlwayType)));
                isSparseData.DataBindings.Add(new Binding(nameof(isSparseData.Checked), data.DbColumn, nameof(data.DbColumn.IsSparse), true, DataSourceUpdateMode.OnValidation, false));
                isColumnSetData.DataBindings.Add(new Binding(nameof(isColumnSetData.Checked), data.DbColumn, nameof(data.DbColumn.IsColumnSet), true, DataSourceUpdateMode.OnValidation, false));
                isFileStreamData.DataBindings.Add(new Binding(nameof(isFileStreamData.Checked), data.DbColumn, nameof(data.DbColumn.IsFileStream), true, DataSourceUpdateMode.OnValidation, false));
                isIdentityData.DataBindings.Add(new Binding(nameof(isIdentityData.Checked), data.DbColumn, nameof(data.DbColumn.IsIdentity), true, DataSourceUpdateMode.OnValidation, false));
                isHiddenData.DataBindings.Add(new Binding(nameof(isHiddenData.Checked), data.DbColumn, nameof(data.DbColumn.IsHidden), true, DataSourceUpdateMode.OnValidation, false));

                data.DbExtendedProperties.Clear();
                data.DbExtendedProperties.AddRange(Program.DbData.DbExtendedProperties.Where(
                        w =>
                        w.CatalogName == data.DbColumn.CatalogName &&
                        w.Level0Name == data.DbColumn.SchemaName &&
                        w.Level1Name == data.DbColumn.TableName &&
                        w.Level2Name == data.DbColumn.ColumnName &&
                        w.PropertyObjectType == ExtendedPropertyObjectType.Column));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = data.DbExtendedProperties;
            }
            else { errorProvider.SetError(catalogNameLayout, "Schema information not available"); }

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
            generatedAlwayTypeData.DataBindings.Clear();

            isSparseData.DataBindings.Clear();
            isColumnSetData.DataBindings.Clear();
            isFileStreamData.DataBindings.Clear();
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
