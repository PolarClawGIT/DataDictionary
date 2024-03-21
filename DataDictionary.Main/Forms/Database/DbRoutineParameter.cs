﻿using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutineParameter : ApplicationData
    {

        public Boolean IsOpenItem(object? item)
        { return bindingParameter.Current is IDbRoutineParameterItem current && ReferenceEquals(current, item); }

        public DbRoutineParameter() : base()
        {
            InitializeComponent();
        }

        public DbRoutineParameter(IDbRoutineParameterItem parameterItem) : this()
        {
            DbRoutineParameterKeyName key = new DbRoutineParameterKeyName(parameterItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            bindingParameter.DataSource = new BindingView<DbRoutineParameterItem>(BusinessData.DatabaseModel.DbRoutineParameters, w => key.Equals(w));
            bindingParameter.Position = 0;

            if (bindingParameter.Current is IDbRoutineParameterItem current)
            {
                this.Icon = new ScopeKey(current).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbRoutineParameter_Load(object sender, EventArgs e)
        {
            IDbRoutineParameterItem bindingNames;
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

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingParameter.Current is not IDbRoutineParameterItem);
        }
    }
}
