using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Catalog
{
    partial class DbRoutine : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingRoutine.Current is IRoutineValue current && ReferenceEquals(current, item); }

        protected DbRoutine() : base()
        {
            InitializeComponent();

            SetRowState(
                bindingRoutine, 
                bindingParameters, 
                bindingProperties,
                bindingDependencies);
            SetTitle(bindingRoutine);
        }

        public DbRoutine(IRoutineValue routineItem) : this()
        {
            RoutineIndexName key = new RoutineIndexName(routineItem);
            PropertyIndexObject propertyKey = new PropertyIndexObject(key);

            bindingRoutine.DataSource = new BindingView<RoutineValue>(BusinessData.CatalogModel.DbRoutines, w => key.Equals(w));
            bindingRoutine.Position = 0;

            if (bindingRoutine.Current is IRoutineValue current)
            {
                ReferenceIndexName referenceName = new ReferenceIndexName(current);
                bindingParameters.DataSource = new BindingView<RoutineParameterValue>(BusinessData.CatalogModel.DbRoutineParameters, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<PropertyValue>(BusinessData.CatalogModel.DbProperties, w => propertyKey.Equals(w));
                bindingDependencies.DataSource = new BindingView<ReferenceValue>(BusinessData.CatalogModel.DbReferences, w => referenceName.Equals(w));
            }
        }

        private void DbRoutine_Load(object sender, EventArgs e)
        {
            IRoutineValue bindingNames;
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), bindingRoutine, nameof(bindingNames.DatabaseName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), bindingRoutine, nameof(bindingNames.SchemaName)));
            routineNameData.DataBindings.Add(new Binding(nameof(routineNameData.Text), bindingRoutine, nameof(bindingNames.RoutineName)));
            routineTypeData.DataBindings.Add(new Binding(nameof(routineTypeData.Text), bindingRoutine, nameof(bindingNames.RoutineType)));
            isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), bindingRoutine, nameof(bindingNames.IsSystem)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = bindingProperties;

            parametersData.AutoGenerateColumns = false;
            parametersData.DataSource = bindingParameters;

            dependenciesData.AutoGenerateColumns = false;
            dependenciesData.DataSource = bindingDependencies;

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingRoutine.Current is not IRoutineValue);
        }
    }
}
