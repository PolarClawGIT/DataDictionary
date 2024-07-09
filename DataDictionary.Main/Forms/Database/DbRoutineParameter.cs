using DataDictionary.BusinessLayer.Database;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutineParameter : ApplicationData
    {

        public Boolean IsOpenItem(object? item)
        { return bindingParameter.Current is IRoutineParameterValue current && ReferenceEquals(current, item); }

        public DbRoutineParameter() : base()
        {
            InitializeComponent();
        }

        public DbRoutineParameter(IRoutineParameterValue parameterItem) : this()
        {
            RoutineParameterKeyName key = new RoutineParameterKeyName(parameterItem);
            ExtendedPropertyIndexName propertyKey = new ExtendedPropertyIndexName(key);

            bindingParameter.DataSource = new BindingView<RoutineParameterValue>(BusinessData.DatabaseModel.DbRoutineParameters, w => key.Equals(w));
            bindingParameter.Position = 0;

            Setup(bindingParameter);

            if (bindingParameter.Current is IRoutineParameterValue current)
            {
                if (current.RoutineType is DbRoutineType.Function)
                { this.Icon = WinFormEnumeration.GetIcon(ScopeType.DatabaseFunctionParameter); }
                else if (current.RoutineType is DbRoutineType.Procedure)
                { this.Icon = WinFormEnumeration.GetIcon(ScopeType.DatabaseProcedureParameter); }

                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
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
