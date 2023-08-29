using DataDictionary.DataLayer.DbMetaData;
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

namespace DataDictionary.Main.Forms
{
    partial class DbSchema : ApplicationBase, IApplicationDataForm
    {

        class FormData
        {
            public DbSchemaKey? SchemaKey { get; set; }
            public IDbSchemaItem? DbSchema { get; set; }

            public BindingList<DbExtendedPropertyItem> DbExtendedProperties { get; set; } = new BindingList<DbExtendedPropertyItem>();
        }

        FormData data = new FormData();

        public Object? OpenItem { get; }

        public DbSchema() : base()
        {
            InitializeComponent();
            this.Icon = Resources.DbSchema;
        }

        public DbSchema(IDbSchemaItem schemaItem) : this()
        {
            data.SchemaKey = new DbSchemaKey(schemaItem);
            OpenItem = schemaItem;
            this.Text = data.SchemaKey.ToString();
        }

        private void DbSchema_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            if (data.SchemaKey is not null)
            { data.DbSchema = Program.Data.DbSchemta.FirstOrDefault(w => data.SchemaKey == new DbSchemaKey(w)); }

            if (data.DbSchema is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbSchema, nameof(data.DbSchema.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbSchema, nameof(data.DbSchema.SchemaName)));
                isSystemData.DataBindings.Add(new Binding(nameof(isSystemData.Checked), data.DbSchema, nameof(data.DbSchema.IsSystem)));
                errorProvider.SetError(schemaNameData.ErrorControl, String.Empty);

                data.DbExtendedProperties.Clear();
                data.DbExtendedProperties.AddRange(Program.Data.DbExtendedProperties.GetProperties(data.DbSchema));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = data.DbExtendedProperties;
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
