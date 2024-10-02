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
    partial class DetailDataView : ApplicationData
    {
        public DetailDataView() : base()
        {
            InitializeComponent();

            SetRowState(bindingSource);
        }

        public DetailDataView(IBindingTable data) : this()
        {
            bindingSource.DataSource = data;
            Text = GetTitle(data);
        }

        public DetailDataView(IBindingData data) : this()
        {
            bindingSource.DataSource = data;
            Text = GetTitle(data);
        }

        public DetailDataView(IBindingList data) : this()
        {
            bindingSource.DataSource = data;
            Text = GetTitle(data);
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
        }

        /// <summary>
        /// Creates a Title based on Reflection.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        String GetTitle(Object data)
        {
            var dataType = data.GetType();
            var dataArgs = dataType.GetGenericArguments();

            if (dataArgs.Length > 0)
            {
                String argsString = String.Join(", ", dataArgs.Select(s => s.Name));
                String typeName = dataType.Name.Substring(0, dataType.Name.IndexOf('`'));

                if (dataArgs.Length == 1)
                { return String.Format("Data: {0}", argsString); }

                return String.Format("Data: {0}<{1}>", typeName, argsString);
            }
            else if (data is IBindingName tableName)
            { return String.Format("Data: {0}", tableName.BindingName); }
            else { return String.Format("Data: {0}", dataType.Name); }
        }

        protected dynamic? SelectedItem = null;

        protected virtual void RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        { SelectedItem = bindingTableValue.Rows[e.RowIndex].DataBoundItem; }

    }
}
