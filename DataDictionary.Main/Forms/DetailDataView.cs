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
    }
}
