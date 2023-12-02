using DataDictionary.BusinessLayer;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Main.Properties;
using System.ComponentModel;
using System.Data;
using Toolbox.Threading;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutine : ApplicationBase, IApplicationDataForm<DbRoutineKeyName>
    {
        public required DbRoutineKeyName DataKey { get; init; }

        public bool IsOpenItem(object? item)
        { return DataKey.Equals(item); }

        public DbRoutine() : base()
        {
            InitializeComponent();
            // Icon set in Binding
        }

        private void DbRoutine_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }

        public bool BindDataCore()
        {
            if (Program.Data.DbRoutines.FirstOrDefault(w => DataKey.Equals(w)) is DbRoutineItem data)
            {
                this.Text = DataKey.ToString();
                DbRoutineParameterItem? firstParameter = Program.Data.DbRoutineParameters.OrderBy(o => o.OrdinalPosition).FirstOrDefault(w => DataKey.Equals(w));

                if (data.ObjectScope == DbObjectScope.Procedure)
                { this.Icon = Resources.Icon_Procedure; }
                else if (data.ObjectScope == DbObjectScope.Function && firstParameter is DbRoutineParameterItem isScalar && isScalar.OrdinalPosition == 0)
                { this.Icon = Resources.Icon_ScalarFunction; }

                else if (data.ObjectScope == DbObjectScope.Function && firstParameter is DbRoutineParameterItem isTable && isTable.OrdinalPosition != 0)
                { this.Icon = Resources.Icon_TableFunction; }

                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.DatabaseName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                routineNameData.DataBindings.Add(new Binding(nameof(routineNameData.Text), data, nameof(data.RoutineName)));
                routineTypeData.DataBindings.Add(new Binding(nameof(routineTypeData.Text), data, nameof(data.RoutineType)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data, nameof(data.IsSystem)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey);

                parametersData.AutoGenerateColumns = false;
                parametersData.DataSource = Program.Data.GetRoutineParameters(DataKey);

                dependenciesData.AutoGenerateColumns = false;
                dependenciesData.DataSource = Program.Data.GetRoutineDependencies(DataKey);

                return true;
            }
            else
            { return false; }
        }

        public void UnbindDataCore()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            routineNameData.DataBindings.Clear();
            routineTypeData.DataBindings.Clear();
            isSystemData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
            parametersData.DataSource = null;
            dependenciesData.DataSource = null;
        }
    }
}
