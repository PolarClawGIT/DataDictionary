using DataDictionary.DataLayer.DatabaseData.Routine;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbRoutine : ApplicationBase, IApplicationDataForm
    {
        public DbRoutineKey DataKey { get; private set; }

        public DbRoutine()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Procedure; //TODO: Need to change this on Binding, depending on type.
            DataKey = new DbRoutineKey(new DbRoutineItem());
        }

        public DbRoutine(IDbRoutineKey routineItem) : this()
        {
            DataKey = new DbRoutineKey(routineItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DbRoutine_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            this.LockForm();

            if (Program.Data.DbRoutines.FirstOrDefault(w => DataKey.Equals(w)) is DbRoutineItem data)
            {
                this.Text = DataKey.ToString();

                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
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

                this.UnLockForm();
            }
            else
            {
                this.LockForm(false);
                this.Text = "[Key Not Found]";
            }
        }

        void UnBindData()
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

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
