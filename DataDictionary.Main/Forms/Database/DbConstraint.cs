using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.Main.Properties;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbConstraint : ApplicationBase<DbConstraintKey>
    {

        public DbConstraint() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Key;
        }

        private void DbConstraint_Load(object sender, EventArgs e)
        { BindData(); }

        protected override bool BindDataCore()
        {
            if (Program.Data.DbConstraints.FirstOrDefault(w => DataKey.Equals(w)) is DbConstraintItem data)
            {
                this.Text = new DbConstraintKey(data).ToString();
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                constraintNameData.DataBindings.Add(new Binding(nameof(constraintNameData.Text), data, nameof(data.ConstraintName)));
                constraintTypeData.DataBindings.Add(new Binding(nameof(constraintTypeData.Text), data, nameof(data.ConstraintType)));
                tableNameData.DataBindings.Add(new Binding(nameof(tableNameData.Text), data, nameof(data.TableName)));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey).ToList();

                constraintColumnsData.AutoGenerateColumns = false;
                constraintColumnsData.DataSource = new BindingView<DbConstraintColumnItem>(Program.Data.DbConstraintColumns, w => DataKey.Equals(w));

                return true;
            }
            else { return false; }
        }

        protected override void UnbindDataCore()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            constraintNameData.DataBindings.Clear();
            constraintTypeData.DataBindings.Clear();
            tableNameData.DataBindings.Clear();

            extendedPropertiesData.DataSource = null;
            constraintColumnsData.DataSource = null;
        }
    }
}
