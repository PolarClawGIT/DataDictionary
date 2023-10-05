using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Main.Properties;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbSchema : ApplicationBase<DbSchemaKey>
    {
        public DbSchema() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Schema;
        }

        private void DbSchema_Load(object sender, EventArgs e)
        { BindData(); }

        protected override bool BindDataCore()
        {
            if (Program.Data.DbSchemta.FirstOrDefault(w => DataKey.Equals(w)) is DbSchemaItem data)
            {
                this.Text = DataKey.ToString();

                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data, nameof(data.IsSystem)));
                errorProvider.SetError(schemaNameData.ErrorControl, String.Empty);

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey).ToList();

                return true;
            }
            else { return false; }
        }

        protected override void UnbindDataCore()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            isSystemData.DataBindings.Clear();
            extendedPropertiesData.DataSource = null;
        }
    }
}
