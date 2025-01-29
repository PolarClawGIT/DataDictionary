using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.Main.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class DbDomain : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingDomain.Current is IDomainValue current && ReferenceEquals(current, item); }

        protected DbDomain() : base()
        {
            InitializeComponent();

            SetRowState(bindingDomain, bindingProperties);
            SetTitle(bindingDomain);
        }

        public DbDomain(IDomainValue domainItem) : this()
        {
            DomainIndexName key = new DomainIndexName(domainItem);
            PropertyIndexObject propertyKey = new PropertyIndexObject(key);

            IBindingList data = new BindingView<DomainValue>(BusinessData.CatalogModel.DbDomains, w => key.Equals(w));
            data.ListChanged += ListChanged;

            bindingDomain.DataSource = data;
            bindingDomain.Position = 0;

            if (bindingDomain.Current is IDomainValue current)
            {
                bindingProperties.DataSource = new BindingView<PropertyValue>(BusinessData.CatalogModel.DbProperties, w => propertyKey.Equals(w));
            }

            void ListChanged(Object? sender, ListChangedEventArgs e)
            {
                // This addresses an invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                {
                    bindingDomain.RaiseListChangedEvents = false;
                    bindingProperties.RaiseListChangedEvents = false;
                }
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
