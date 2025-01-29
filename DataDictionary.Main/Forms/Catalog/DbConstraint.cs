using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class DbConstraint : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingConstraint.Current is IConstraintValue current && ReferenceEquals(current, item); }

        protected DbConstraint() : base()
        {
            InitializeComponent();

            SetRowState(bindingConstraint, bindingColumn, bindingProperties);
            SetTitle(bindingConstraint);
        }

        public DbConstraint(IConstraintValue constraintItem) : this()
        {
            ConstraintIndex key = new ConstraintIndex(constraintItem);

            IBindingList data = new BindingView<ConstraintValue>(BusinessData.CatalogModel.DbConstraints, w => key.Equals(w));
            data.ListChanged += ListChanged;

            bindingConstraint.DataSource = data;
            bindingConstraint.Position = 0;

            if (bindingConstraint.Current is IConstraintValue current)
            {
                ConstraintIndexName name = new ConstraintIndexName(current);
                PropertyIndexObject property = new PropertyIndexObject(name);

                bindingColumn.DataSource = new BindingView<ConstraintColumnValue>(BusinessData.CatalogModel.DbConstraintColumns, w => name.Equals(w), o => o.OrdinalPosition ?? 0);
                bindingProperties.DataSource = new BindingView<PropertyValue>(BusinessData.CatalogModel.DbProperties, w => property.Equals(w));
            }

            void ListChanged(Object? sender, ListChangedEventArgs e)
            {
                // This addresses an invalid operation exception fired by CurrencyManager.FindGoodRow on an empty list
                if (e.ListChangedType is ListChangedType.ItemDeleted
                    && sender is IBindingList values
                    && values.Count is 0)
                {
                    bindingConstraint.RaiseListChangedEvents = false;
                    bindingColumn.RaiseListChangedEvents = false;
                    bindingProperties.RaiseListChangedEvents = false;
                }
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
