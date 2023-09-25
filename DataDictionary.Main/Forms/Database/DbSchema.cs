using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.Main.Messages;
using DataDictionary.Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Toolbox.BindingTable;
using Toolbox.Mediator;

namespace DataDictionary.Main.Forms.Database
{
    partial class DbSchema : ApplicationBase, IApplicationDataForm
    {
        public DbSchemaKey DataKey { get; private set; }

        public DbSchema() : base()
        {
            InitializeComponent();
            this.Icon = Resources.Icon_Schema;
            DataKey = new DbSchemaKey(new DbSchemaItem() { });
        }

        public DbSchema(IDbSchemaKey schemaItem) : this()
        {
            DataKey = new DbSchemaKey(schemaItem);
            this.Text = DataKey.ToString();
        }

        public Boolean IsOpenItem(Object? item)
        { return DataKey.Equals(item); }

        private void DbSchema_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            DbSchemaItem? data = Program.Data.DbSchemta.FirstOrDefault(w => DataKey.Equals(w));

            if (data is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data, nameof(data.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data, nameof(data.SchemaName)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data, nameof(data.IsSystem)));
                errorProvider.SetError(schemaNameData.ErrorControl, String.Empty);

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = Program.Data.GetExtendedProperty(DataKey).ToList();
            }
        }

        void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
            isSystemData.DataBindings.Clear();
            extendedPropertiesData.DataSource = null;
        }

        #region IColleague
        protected override void HandleMessage(DbDataBatchStarting message)
        { UnBindData(); }

        protected override void HandleMessage(DbDataBatchCompleted message)
        { BindData(); }
        #endregion
    }
}
