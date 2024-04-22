﻿using DataDictionary.BusinessLayer.Database;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbSchema : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSchema.Current is ISchemaValue current && ReferenceEquals(current, item); }

        public DbSchema() : base()
        {
            InitializeComponent();
        }

        public DbSchema(ISchemaValue schemaItem) : this()
        {
            SchemaKeyName key = new SchemaKeyName(schemaItem);
            ExtendedPropertyKeyName propertyKey = new ExtendedPropertyKeyName(key);

            bindingSchema.DataSource = new BindingView<SchemaValue>(BusinessData.DatabaseModel.DbSchemta, w => key.Equals(w));
            bindingSchema.Position = 0;

            Setup(bindingSchema);

            if (bindingSchema.Current is ISchemaValue current)
            {
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbSchema_Load(object sender, EventArgs e)
        {
            ISchemaValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingSchema, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingSchema, nameof(bindingNames.SchemaName)));
            isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), bindingSchema, nameof(bindingNames.IsSystem)));
            errorProvider.SetError(schemaNameData.ErrorControl, String.Empty);

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSchema.Current is not ISchemaValue);
        }

    }
}
