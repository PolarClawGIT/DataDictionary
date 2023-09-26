using DataDictionary.DataLayer.DatabaseData.Routine;
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

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutineParameter : ApplicationBase, IApplicationDataForm
    {
        public DbRoutineParameterKey DataKey { get; private set; }

        public DbRoutineParameter()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Parameter;
            DataKey = new DbRoutineParameterKey(new DbRoutineParameterItem());
        }

        public DbRoutineParameter(IDbRoutineParameterKey domainItem) : this()
        {
            DataKey = new DbRoutineParameterKey(domainItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DbRoutineParameter_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            if (Program.Data.DbRoutineParameters.FirstOrDefault(w => DataKey.Equals(w)) is DbRoutineParameterItem data)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                routineNameData.DataBindings.Add(new Binding(nameof(routineNameData.Text), data, nameof(data.RoutineName)));
                parameterNameData.DataBindings.Add(new Binding(nameof(parameterNameData.Text), data, nameof(data.ParameterName)));
                ordinalPositionData.DataBindings.Add(new Binding(nameof(ordinalPositionData.Text), data, nameof(data.OrdinalPosition)));

                dataTypeData.DataBindings.Add(new Binding(nameof(parameterNameData.Text), data, nameof(data.DataType)));
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

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey).ToList();
            }
        }

        void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            routineNameData.DataBindings.Clear();
            parameterNameData.DataBindings.Clear();
            ordinalPositionData.DataBindings.Clear();

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
            domainSchemaData.DataBindings.Clear();
            domainNameData.DataBindings.Clear();

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
