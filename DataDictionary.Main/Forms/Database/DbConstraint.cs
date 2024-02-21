using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbConstraint : ApplicationBase, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingConstraint.Current is DbConstraintItem current && ReferenceEquals(current, item); }

        public DbConstraint() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Key;
        }

        public DbConstraint(DbConstraintItem constraintItem) : this()
        {
            DbConstraintKeyName key = new DbConstraintKeyName(constraintItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            RowState = constraintItem.RowState();
            constraintItem.RowStateChanged += ConstraintItem_RowStateChanged;
            this.Text = constraintItem.ToString();

            bindingConstraint.DataSource = new BindingView<DbConstraintItem>(BusinessData.DatabaseData.DbConstraints, w => key.Equals(w));
            bindingColumn.DataSource = new BindingView<DbConstraintColumnItem>(BusinessData.DatabaseData.DbConstraintColumns, w => key.Equals(w));
            bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseData.DbExtendedProperties, w => propertyKey.Equals(w));
            bindingConstraint.Position = 0;
        }

        private void DbConstraint_Load(object sender, EventArgs e)
        {
            IDbConstraintItem bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingConstraint, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingConstraint, nameof(bindingNames.SchemaName)));
            constraintNameData.DataBindings.Add(new Binding(nameof(constraintNameData.Text), bindingConstraint, nameof(bindingNames.ConstraintName)));
            constraintTypeData.DataBindings.Add(new Binding(nameof(constraintTypeData.Text), bindingConstraint, nameof(bindingNames.ConstraintType)));
            tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), bindingConstraint, nameof(bindingNames.TableName)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            constraintColumnsData.AutoGenerateColumns = false;
            constraintColumnsData.DataSource = bindingColumn;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingConstraint.Current is not DbConstraintItem);
        }

        private void ConstraintItem_RowStateChanged(object? sender, EventArgs e)
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
