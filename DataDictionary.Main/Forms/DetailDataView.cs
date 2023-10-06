using DataDictionary.DataLayer.DatabaseData.Schema;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Main.Properties;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class DetailDataView : ApplicationBase, IApplicationDataBind
    {
        public IBindingTable? bindingTableSource { get; private set; }

        public DetailDataView() : base()
        {
            InitializeComponent();
        }

        public DetailDataView(IBindingTable data, Icon? icon = null) : this()
        {
            bindingTableSource = data;
            this.Text = String.Format("{0}: {1}", this.Text, data.BindingName);
            if (icon is Icon value) { this.Icon = value; }
            else { this.Icon = Resources.Icon_Application; }
        }

        public Boolean IsOpenItem(Object? item)
        { return ReferenceEquals(bindingTableSource, item); }

        private void BindingDataView_Load(object sender, EventArgs e)
        {
            (this as IApplicationDataBind).BindData();
        }


        public bool BindDataCore()
        {
            if (bindingTableSource is IBindingTable value)
            {
                bindingTableValue.DataSource = bindingTableSource;
                using (DataTable data = new DataTable())
                {
                    data.Load(bindingTableSource.CreateDataReader());
                    dataTableValue.DataSource = data;
                }
                return true;
            }
            else { return false; }
        }

        public void UnbindDataCore()
        {
            bindingTableValue.DataSource = null;
            dataTableValue.DataSource = null;
        }

        private void bindingTableValue_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DbSchemaItem schemaItem)
            { Activate((data) => new Forms.Database.DbSchema() { DataKey = new DbSchemaKey(schemaItem) }, schemaItem); }

            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DbTableItem tableItem)
            { Activate((data) => new Forms.Database.DbTable() { DataKey = new DbTableKey(tableItem) }, tableItem); }

            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DbTableColumnItem columnItem)
            { Activate((data) => new Forms.Database.DbTableColumn() { DataKey = new DbTableColumnKey(columnItem) }, columnItem); }

            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DomainAttributeItem attributeItem)
            { Activate((data) => new Forms.Domain.DomainAttribute() { DataKey = new DomainAttributeKey(attributeItem) }, attributeItem); }

        }

    }
}
