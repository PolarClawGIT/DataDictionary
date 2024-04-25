using DataDictionary.BusinessLayer.Database;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbConstraint : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingConstraint.Current is IConstraintValue current && ReferenceEquals(current, item); }

        public DbConstraint() : base()
        {
            InitializeComponent();
        }

        public DbConstraint(IConstraintValue constraintItem) : this()
        {
            ConstraintIndexName key = new ConstraintIndexName(constraintItem);
            ExtendedPropertyKeyName propertyKey = new ExtendedPropertyKeyName(key);

            bindingConstraint.DataSource = new BindingView<ConstraintValue>(BusinessData.DatabaseModel.DbConstraints, w => key.Equals(w));
            bindingConstraint.Position = 0;

            Setup(bindingConstraint);

            if (bindingConstraint.Current is IConstraintValue current)
            {
                bindingColumn.DataSource = new BindingView<ConstraintColumnValue>(BusinessData.DatabaseModel.DbConstraintColumns, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbConstraint_Load(object sender, EventArgs e)
        {
            IConstraintValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingConstraint, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingConstraint, nameof(bindingNames.SchemaName)));
            constraintNameData.DataBindings.Add(new Binding(nameof(constraintNameData.Text), bindingConstraint, nameof(bindingNames.ConstraintName)));
            constraintTypeData.DataBindings.Add(new Binding(nameof(constraintTypeData.Text), bindingConstraint, nameof(bindingNames.ConstraintType)));
            tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), bindingConstraint, nameof(bindingNames.TableName)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            constraintColumnsData.AutoGenerateColumns = false;
            constraintColumnsData.DataSource = bindingColumn;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingConstraint.Current is not IConstraintValue);
        }
    }
}
