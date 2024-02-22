using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbSchema : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingSchema.Current is IDbSchemaItem current && ReferenceEquals(current, item); }

        public DbSchema() : base()
        {
            InitializeComponent();
        }

        public DbSchema(IDbSchemaItem schemaItem) : this()
        {
            DbSchemaKeyName key = new DbSchemaKeyName(schemaItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            bindingSchema.DataSource = new BindingView<DbSchemaItem>(BusinessData.DatabaseData.DbSchemta, w => key.Equals(w));
            bindingSchema.Position = 0;

            if (bindingSchema.Current is IDbSchemaItem current)
            {
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();
                this.Icon = new ScopeKey(current).Scope.ToIcon();

                bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseData.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbSchema_Load(object sender, EventArgs e)
        {
            IDbSchemaItem bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingSchema, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingSchema, nameof(bindingNames.SchemaName)));
            isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), bindingSchema, nameof(bindingNames.IsSystem)));
            errorProvider.SetError(schemaNameData.ErrorControl, String.Empty);

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingSchema.Current is not IDbSchemaItem);
        }


        private void RowStateChanged(object? sender, EventArgs e)
        {
            if (sender is IBindingRowState data)
            {
                RowState = data.RowState();
                if (IsHandleCreated)
                { this.Invoke(() => { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }); }
                else { this.IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted); }
            }
        }

    }
}
