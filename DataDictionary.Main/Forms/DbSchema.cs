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
    public partial class DbSchema : Form, IColleague
    {

        class FormData : INotifyPropertyChanged
        {
            IDbSchemaItem? schemaItem;
            public IDbSchemaItem? DbSchema { get { return schemaItem; } set { schemaItem = value; OnPropertyChanged(nameof(DbSchema)); } }

            public BindingList<IDbExtendedPropertyItem> ExtendedProperties { get; set; } = new BindingList<IDbExtendedPropertyItem>();

            public event PropertyChangedEventHandler? PropertyChanged;
            public virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged is PropertyChangedEventHandler handler)
                { handler(this, new PropertyChangedEventArgs(propertyName)); }
            }
        }

        FormData data = new FormData();

        public DbSchema()
        {
            InitializeComponent();
            Program.Messenger.AddColleague(this);
            SendMessage(new Messages.FormAddMdiChild() { ChildForm = this });

        }

        public DbSchema(IDbSchemaItem schemaItem) : this()
        {
            data.DbSchema = schemaItem;

            data.ExtendedProperties.AddRange(Program.DbData.DbExtendedProperties.Where(
                w =>
                w.CatalogName == schemaItem.CatalogName &&
                w.Level0Name == schemaItem.SchemaName &&
                w.PropertyObjectType == ExtendedPropertyObjectType.Schema));

            this.Text = String.Format("{0}: {1}.{2}", this.Text, schemaItem.CatalogName, schemaItem.SchemaName);
        }

        private void DbSchema_Load(object sender, EventArgs e)
        {
            // Data Binding
            catalogNameData.DataBindings.Add(new Binding(nameof(catalogNameData.Text), data.DbSchema, nameof(data.DbSchema.CatalogName)));
            schemaNameData.DataBindings.Add(new Binding(nameof(schemaNameData.Text), data.DbSchema, nameof(data.DbSchema.SchemaName)));

            extendedPropertiesData.AutoGenerateColumns = false;
            extendedPropertiesData.DataSource = data.ExtendedProperties;
        }

        #region IColleague
        public event EventHandler<MessageEventArgs>? OnSendMessage;

        public void RecieveMessage(object? sender, MessageEventArgs message)
        { }

        void SendMessage(MessageEventArgs message)
        {
            if (OnSendMessage is EventHandler<MessageEventArgs> handler)
            { handler(this, message); }
        }
        #endregion
    }
}
