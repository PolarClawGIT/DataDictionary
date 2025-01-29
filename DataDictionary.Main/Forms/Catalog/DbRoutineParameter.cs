using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class DbRoutineParameter : ApplicationData
    {

        public Boolean IsOpenItem(object? item)
        { return bindingParameter.Current is IRoutineParameterValue current && ReferenceEquals(current, item); }

        protected DbRoutineParameter() : base()
        {
            InitializeComponent();

            SetRowState(bindingParameter, bindingProperties);
            SetTitle(bindingParameter);
        }

        public DbRoutineParameter(IRoutineParameterValue parameterItem) : this()
        {
            RoutineParameterIndexName key = new RoutineParameterIndexName(parameterItem);
            PropertyIndexObject propertyKey = new PropertyIndexObject(key);
            
            IBindingList data = new BindingView<RoutineParameterValue>(BusinessData.CatalogModel.DbRoutineParameters, w => key.Equals(w));
            data.ListChanged += ListChanged;

            bindingParameter.DataSource = data;
            bindingParameter.Position = 0;

            if (bindingParameter.Current is IRoutineParameterValue current)
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
                    bindingParameter.RaiseListChangedEvents = false;
                    bindingProperties.RaiseListChangedEvents = false;
                }
            }
        }

        private void DbRoutineParameter_Load(object sender, EventArgs e)
        {
            IRoutineParameterValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingParameter, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingParameter, nameof(bindingNames.SchemaName)));
            routineNameData.DataBindings.Add(new Binding(nameof(routineNameData.Text), bindingParameter, nameof(bindingNames.RoutineName)));
            parameterNameData.DataBindings.Add(new Binding(nameof(parameterNameData.Text), bindingParameter, nameof(bindingNames.ParameterName)));
            ordinalPositionData.DataBindings.Add(new Binding(nameof(ordinalPositionData.Text), bindingParameter, nameof(bindingNames.OrdinalPosition)));

            dataTypeData.DataBindings.Add(new Binding(nameof(parameterNameData.Text), bindingParameter, nameof(bindingNames.DataType)));
            characterMaximumLengthData.DataBindings.Add(new Binding(nameof(characterMaximumLengthData.Text), bindingParameter, nameof(bindingNames.CharacterMaximumLength)));
            characterOctetLengthData.DataBindings.Add(new Binding(nameof(characterOctetLengthData.Text), bindingParameter, nameof(bindingNames.CharacterOctetLength)));
            numericPrecisionData.DataBindings.Add(new Binding(nameof(numericPrecisionData.Text), bindingParameter, nameof(bindingNames.NumericPrecision)));
            numericPrecisionRadixData.DataBindings.Add(new Binding(nameof(numericPrecisionRadixData.Text), bindingParameter, nameof(bindingNames.NumericPrecisionRadix)));
            numericScaleData.DataBindings.Add(new Binding(nameof(numericScaleData.Text), bindingParameter, nameof(bindingNames.NumericScale)));
            dateTimePrecisionData.DataBindings.Add(new Binding(nameof(dateTimePrecisionData.Text), bindingParameter, nameof(bindingNames.DateTimePrecision)));

            characterSetCatalogData.DataBindings.Add(new Binding(nameof(characterSetCatalogData.Text), bindingParameter, nameof(bindingNames.CharacterSetCatalog)));
            characterSetSchemaData.DataBindings.Add(new Binding(nameof(characterSetSchemaData.Text), bindingParameter, nameof(bindingNames.CharacterSetSchema)));
            characterSetNameData.DataBindings.Add(new Binding(nameof(characterSetNameData.Text), bindingParameter, nameof(bindingNames.CharacterSetName)));

            collationCatalogData.DataBindings.Add(new Binding(nameof(collationCatalogData.Text), bindingParameter, nameof(bindingNames.CollationCatalog)));
            collationSchemaData.DataBindings.Add(new Binding(nameof(collationSchemaData.Text), bindingParameter, nameof(bindingNames.CollationSchema)));
            collationNameData.DataBindings.Add(new Binding(nameof(collationNameData.Text), bindingParameter, nameof(bindingNames.CollationName)));

            domainCatalogData.DataBindings.Add(new Binding(nameof(domainCatalogData.Text), bindingParameter, nameof(bindingNames.CollationCatalog)));
            domainSchemaData.DataBindings.Add(new Binding(nameof(domainSchemaData.Text), bindingParameter, nameof(bindingNames.DomainSchema)));
            domainNameData.DataBindings.Add(new Binding(nameof(domainNameData.Text), bindingParameter, nameof(bindingNames.DomainName)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingParameter.Current is not IRoutineParameterValue);
        }
    }
}
