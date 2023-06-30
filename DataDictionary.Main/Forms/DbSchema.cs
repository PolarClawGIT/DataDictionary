using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
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
    partial class DbSchema : ApplicationFormBase
    {

        class FormData
        {
            public DbSchemaName SchemaName { get; set; } = new DbSchemaName();
            public IDbSchemaItem? DbSchema { get; set; }

            public BindingList<IDbExtendedPropertyItem> ExtendedProperties { get; set; } = new BindingList<IDbExtendedPropertyItem>();
        }

        FormData data = new FormData();

        public DbSchema() : base()
        {
            InitializeComponent();
        }

        public DbSchema(IDbSchemaItem schemaItem) : this()
        {
            data.SchemaName = new DbSchemaName(schemaItem);
            data.DbSchema = schemaItem;

            this.Text = String.Format("{0}: {1}", this.Text, data.SchemaName.ToString());
        }

        private void DbSchema_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            data.DbSchema = Program.DbData.DbSchemas.FirstOrDefault(w => data.SchemaName == w);

            if (data.DbSchema is not null)
            {
                catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbSchema, nameof(data.DbSchema.CatalogName)));
                schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbSchema, nameof(data.DbSchema.SchemaName)));
                errorProvider.SetError(catalogNameLayout, String.Empty);

                data.ExtendedProperties.Clear();
                data.ExtendedProperties.AddRange(Program.DbData.DbExtendedProperties.Where(
                        w =>
                        w.CatalogName == data.SchemaName.CatalogName &&
                        w.Level0Name == data.SchemaName.SchemaName &&
                        w.PropertyObjectType == ExtendedPropertyObjectType.Schema));

                extendedPropertiesData.AutoGenerateColumns = false;
                extendedPropertiesData.DataSource = data.ExtendedProperties;
            }
            else
            { errorProvider.SetError(catalogNameLayout, "Schmema information not avaiable"); }
        }

        void UnBindData()
        {
            catalogNameData.DataBindings.Clear();
            schemaNameData.DataBindings.Clear();
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
