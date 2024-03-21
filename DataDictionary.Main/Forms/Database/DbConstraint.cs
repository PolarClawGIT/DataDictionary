using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.Main.Controls;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbConstraint : ApplicationData, IApplicationDataForm
    {

        public Boolean IsOpenItem(object? item)
        { return bindingConstraint.Current is IDbConstraintItem current && ReferenceEquals(current, item); }

        public DbConstraint() : base()
        {
            InitializeComponent();
        }

        public DbConstraint(IDbConstraintItem constraintItem) : this()
        {
            DbConstraintKeyName key = new DbConstraintKeyName(constraintItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            bindingConstraint.DataSource = new BindingView<DbConstraintItem>(BusinessData.DatabaseModel.DbConstraints, w => key.Equals(w));
            bindingConstraint.Position = 0;

            if (bindingConstraint.Current is IDbConstraintItem current)
            {
                this.Icon = new ScopeKey(current).Scope.ToIcon();
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();

                bindingColumn.DataSource = new BindingView<DbConstraintColumnItem>(BusinessData.DatabaseModel.DbConstraintColumns, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
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

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingConstraint.Current is not IDbConstraintItem);
        }
    }
}
