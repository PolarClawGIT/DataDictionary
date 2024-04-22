using DataDictionary.BusinessLayer.Database;
//using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
//using DataDictionary.DataLayer.DatabaseData.Routine;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutine : ApplicationData, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingRoutine.Current is IRoutineValue current && ReferenceEquals(current, item); }

        public DbRoutine() : base()
        {
            InitializeComponent();
        }

        public DbRoutine(IRoutineValue routineItem): this()
        {
            RoutineKeyName key = new RoutineKeyName(routineItem);
            ExtendedPropertyKeyName propertyKey = new ExtendedPropertyKeyName(key);

            bindingRoutine.DataSource = new BindingView<RoutineValue>(BusinessData.DatabaseModel.DbRoutines, w => key.Equals(w));
            bindingRoutine.Position = 0;

            Setup(bindingRoutine);

            if (bindingRoutine.Current is IRoutineValue current)
            {
                bindingParameters.DataSource = new BindingView<RoutineParameterValue>(BusinessData.DatabaseModel.DbRoutineParameters, w => key.Equals(w));
                bindingDependencies.DataSource = new BindingView<RoutineDependencyValue>(BusinessData.DatabaseModel.DbRoutineDependencies, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<ExtendedPropertyValue>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
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
