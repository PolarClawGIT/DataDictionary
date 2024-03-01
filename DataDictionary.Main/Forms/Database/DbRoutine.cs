using DataDictionary.BusinessLayer;
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
    partial class DbRoutine : ApplicationBase, IApplicationDataForm
    {
        public Boolean IsOpenItem(object? item)
        { return bindingRoutine.Current is IDbRoutineItem current && ReferenceEquals(current, item); }

        public DbRoutine() : base()
        {
            InitializeComponent();
        }

        public DbRoutine(IDbRoutineItem routineItem): this()
        {
            DbRoutineKeyName key = new DbRoutineKeyName(routineItem);
            DbExtendedPropertyKeyName propertyKey = new DbExtendedPropertyKeyName(key);

            bindingRoutine.DataSource = new BindingView<DbRoutineItem>(BusinessData.DatabaseModel.DbRoutines, w => key.Equals(w));
            bindingRoutine.Position = 0;

            if (bindingRoutine.Current is IDbRoutineItem current)
            {
                RowState = current.RowState();
                current.RowStateChanged += RowStateChanged;
                this.Text = current.ToString();
                this.Icon = new ScopeKey(current).Scope.ToIcon();

                bindingParameters.DataSource = new BindingView<DbRoutineParameterItem>(BusinessData.DatabaseModel.DbRoutineParameters, w => key.Equals(w));
                bindingDependencies.DataSource = new BindingView<DbRoutineDependencyItem>(BusinessData.DatabaseModel.DbRoutineDependencies, w => key.Equals(w));
                bindingProperties.DataSource = new BindingView<DbExtendedPropertyItem>(BusinessData.DatabaseModel.DbExtendedProperties, w => propertyKey.Equals(w));
            }
        }

        private void DbRoutine_Load(object sender, EventArgs e)
        {
            IDbRoutineItem bindingNames;
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

            IsLocked(RowState is DataRowState.Detached or DataRowState.Deleted || bindingRoutine.Current is not IDbRoutineItem);
        }
    }
}
