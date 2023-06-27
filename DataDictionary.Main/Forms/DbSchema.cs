using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.Main.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        class FormData : INotifyPropertyChanged
        {
            public IDbSchemaName? SchemaName { get; set; }

            public IDbSchemaItem? DbSchema { get { return Program.DbData.DbSchemas.FirstOrDefault(w => w == SchemaName); } }

            public BindingList<IDbExtendedPropertyItem> ExtendedProperties { get; set; } = new BindingList<IDbExtendedPropertyItem>();

            public event PropertyChangedEventHandler? PropertyChanged;
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        FormData data = new FormData();

        public DbSchema() : base()
        {
            InitializeComponent();

        }

        public DbSchema(IDbSchemaItem schemaItem) : this()
        {
            data.SchemaName = new DbSchemaName(schemaItem);

            data.ExtendedProperties.AddRange(Program.DbData.DbExtendedProperties.Where(
                w =>
                w.CatalogName == schemaItem.CatalogName &&
                w.Level0Name == schemaItem.SchemaName &&
                w.PropertyObjectType == ExtendedPropertyObjectType.Schema));

            this.Text = String.Format("{0}: {1}.{2}", this.Text, schemaItem.CatalogName, schemaItem.SchemaName);
        }

        private void DbSchema_Load(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbSchema, nameof(data.DbSchema.CatalogName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbSchema, nameof(data.DbSchema.SchemaName)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = data.ExtendedProperties;
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
