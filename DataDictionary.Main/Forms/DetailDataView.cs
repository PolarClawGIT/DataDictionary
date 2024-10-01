using DataDictionary.BusinessLayer;
using DataDictionary.BusinessLayer.Database;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;
using System.Data;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class DetailDataView : ApplicationData, IApplicationDataBind
    {
        public DetailDataView() : base()
        {
            InitializeComponent();

            SetRowState(bindingSource);
        }

        public DetailDataView(IBindingTable data) : this()
        {
            bindingSource.DataSource = data;

            if (data is IBindingName dataName)
            { this.Text = String.Format("{0}: {1}", this.Text, dataName.BindingName); }
            else { this.Text = String.Format("{0}: {1}", this.Text, data.GetType().Name); }
        }

        public DetailDataView(IBindingData data) : this()
        {
            bindingSource.DataSource = data;

            if (data is IBindingName dataName)
            { this.Text = String.Format("{0}: {1}", this.Text, dataName.BindingName); }
            else { this.Text = String.Format("{0}: {1}", this.Text, data.GetType().Name); }
        }

        public DetailDataView(IBindingList data) : this()
        {
            bindingSource.DataSource = data;
            this.Text = String.Format("{0}: {1}", this.Text, data.GetType().Name);
        }

        public DetailDataView(ScopeType scope, IBindingTable data) : this(data)
        { SetIcon(scope); }

        public DetailDataView(ScopeType scope, IBindingData data) : this(data)
        { SetIcon(scope); }

        public DetailDataView(ScopeType scope, IBindingList data) : this(data)
        { SetIcon(scope); }

        public Boolean IsOpenItem(Object? item)
        { return ReferenceEquals(bindingSource.DataSource, item); }

        private void BindingDataView_Load(object sender, EventArgs e)
        { (this as IApplicationDataBind).BindData(); }


        public bool BindDataCore()
        {
            bindingTableValue.DataSource = bindingSource;

            if (bindingSource.DataSource is IBindingDataReader reader)
            {
                using (DataTable data = new DataTable())
                {
                    data.Load(reader.CreateDataReader());
                    dataTableValue.DataSource = data;
                }
            }
            return true;
        }

        public void UnbindDataCore()
        {
            bindingTableValue.DataSource = null;
            dataTableValue.DataSource = null;
        }

        protected dynamic? SelectedItem = null;

        protected virtual void RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectedItem = bindingTableValue.Rows[e.RowIndex].DataBoundItem;

            //TODO: Revise to use a Generic and pass the constructor method.
            //if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is ISchemaValue schemaItem)
            //{ Activate((data) => new Forms.Database.DbSchema(schemaItem), schemaItem); }

            //if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is ITableValue tableItem)
            //{ Activate((data) => new Forms.Database.DbTable(tableItem), tableItem); }

            //if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is ITableColumnValue columnItem)
            //{ Activate((data) => new Forms.Database.DbTableColumn(columnItem), columnItem); }

            //if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is AttributeValue attributeItem)
            //{ Activate((data) => new Forms.Domain.DomainAttribute(attributeItem), attributeItem); }

        }

    }
}
