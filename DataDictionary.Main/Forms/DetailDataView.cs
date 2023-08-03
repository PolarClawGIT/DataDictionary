using DataDictionary.DataLayer.DbMetaData;
using DataDictionary.DataLayer.DomainData;
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
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class DetailDataView : ApplicationFormBase, IApplicationDataForm
    {
        public Object? OpenItem { get; private set; }
        IBindingTable? bindingTableSource
        { get { if (OpenItem is IBindingTable value) { return value; } else { return null; } } }

        public DetailDataView() : base()
        {
            InitializeComponent();
        }

        public DetailDataView(IBindingTable data, Icon? icon = null) : this()
        {
            OpenItem = data;
            this.Text = String.Format("{0}: {1}", this.Text, data.BindingName);
            if (icon is Icon value) { this.Icon = value; }
            else { this.Icon = Resources.DataDictionaryApplication; }
        }

        private void BindingDataView_Load(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            if (bindingTableSource is IBindingTable value)
            {
                bindingTableValue.DataSource = bindingTableSource;
                using (DataTable data = new DataTable())
                {
                    data.Load(bindingTableSource.CreateDataReader());
                    dataTableValue.DataSource = data;
                }
            }
        }

        void UnBindData()
        {
            bindingTableValue.DataSource = null;
            dataTableValue.DataSource = null;
        }

        private void bindingTableValue_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DbSchemaItem schemaItem)
            { Activate((data) => new Forms.DbSchema(schemaItem), schemaItem); }

            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DbTableItem tableItem)
            { Activate((data) => new Forms.DbTable(tableItem), tableItem); }

            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DbTableColumnItem columnItem)
            { Activate((data) => new Forms.DbTableColumn(columnItem), columnItem); }

            if (bindingTableValue.Rows[e.RowIndex].DataBoundItem is DomainAttributeItem attributeItem)
            { Activate((data) => new Forms.DomainAttribute(attributeItem), attributeItem); }

        }
    }
}
