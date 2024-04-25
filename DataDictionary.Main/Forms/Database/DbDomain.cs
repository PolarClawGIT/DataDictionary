using DataDictionary.BusinessLayer.Database;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbDomain : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingDomain.Current is IDomainValue current && ReferenceEquals(current, item); }

        public DbDomain() : base()
        {
            InitializeComponent();
        }

        public DbDomain(IDomainValue domainItem) : this()
        {
            DomainIndexName key = new DomainIndexName(domainItem);
            ExtendedPropertyKeyName propertyKey = new ExtendedPropertyKeyName(key);

            bindingDomain.DataSource = new BindingView<DomainValue>(BusinessData.DatabaseModel.DbDomains, w => key.Equals(w));
            bindingDomain.Position = 0;

            Setup(bindingDomain);

            if (bindingDomain.Current is IDomainValue current)
            {
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbDomain_Load(object sender, EventArgs e)
        {
            IDomainValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingDomain, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingDomain, nameof(bindingNames.SchemaName)));
            domainNameData.DataBindings.Add(new Binding(nameof(domainNameData.Text), bindingDomain, nameof(bindingNames.DomainName)));
            domainDefaultData.DataBindings.Add(new Binding(nameof(domainDefaultData.Text), bindingDomain, nameof(bindingNames.DomainDefault)));

            dataTypeData.DataBindings.Add(new Binding(nameof(dataTypeData.Text), bindingDomain, nameof(bindingNames.DataType)));
            characterMaximumLengthData.DataBindings.Add(new Binding(nameof(characterMaximumLengthData.Text), bindingDomain, nameof(bindingNames.CharacterMaximumLength)));
            characterOctetLengthData.DataBindings.Add(new Binding(nameof(characterOctetLengthData.Text), bindingDomain, nameof(bindingNames.CharacterOctetLength)));
            numericPrecisionData.DataBindings.Add(new Binding(nameof(numericPrecisionData.Text), bindingDomain, nameof(bindingNames.NumericPrecision)));
            numericPrecisionRadixData.DataBindings.Add(new Binding(nameof(numericPrecisionRadixData.Text), bindingDomain, nameof(bindingNames.NumericPrecisionRadix)));
            numericScaleData.DataBindings.Add(new Binding(nameof(numericScaleData.Text), bindingDomain, nameof(bindingNames.NumericScale)));
            dateTimePrecisionData.DataBindings.Add(new Binding(nameof(dateTimePrecisionData.Text), bindingDomain, nameof(bindingNames.DateTimePrecision)));

            characterSetCatalogData.DataBindings.Add(new Binding(nameof(characterSetCatalogData.Text), bindingDomain, nameof(bindingNames.CharacterSetCatalog)));
            characterSetSchemaData.DataBindings.Add(new Binding(nameof(characterSetSchemaData.Text), bindingDomain, nameof(bindingNames.CharacterSetSchema)));
            characterSetNameData.DataBindings.Add(new Binding(nameof(characterSetNameData.Text), bindingDomain, nameof(bindingNames.CharacterSetName)));

            collationCatalogData.DataBindings.Add(new Binding(nameof(collationCatalogData.Text), bindingDomain, nameof(bindingNames.CollationCatalog)));
            collationSchemaData.DataBindings.Add(new Binding(nameof(collationSchemaData.Text), bindingDomain, nameof(bindingNames.CollationSchema)));
            collationNameData.DataBindings.Add(new Binding(nameof(collationNameData.Text), bindingDomain, nameof(bindingNames.CollationName)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingDomain.Current is not IDomainValue);
        }
    }
}
