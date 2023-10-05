using DataDictionary.DataLayer.DatabaseData.Domain;
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
    partial class DbDomain : ApplicationBase<DbDomainKey>
    {
        public DbDomain() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_DomainType;
        }

        private void DbDomain_Load(object sender, EventArgs e)
        { BindData(); }

        protected override bool BindDataCore()
        {
            if (Program.Data.DbDomains.FirstOrDefault(w => DataKey.Equals(w)) is DbDomainItem data)
            {
                this.Text = DataKey.ToString();

                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                domainNameData.DataBindings.Add(new Binding(nameof(domainNameData.Text), data, nameof(data.DomainName)));
                domainDefaultData.DataBindings.Add(new Binding(nameof(domainDefaultData.Text), data, nameof(data.DomainDefault)));

                dataTypeData.DataBindings.Add(new Binding(nameof(dataTypeData.Text), data, nameof(data.DataType)));
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

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey).ToList();

                return true;
            }
            else
            { return false; }
        }

        protected override void UnbindDataCore()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            domainNameData.DataBindings.Clear();
            domainDefaultData.DataBindings.Clear();

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

            extendedPropertiesData.DataSource = null;
        }
    }
}
