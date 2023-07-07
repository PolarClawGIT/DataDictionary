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
    partial class UnitTestGridView : ApplicationFormBase
    {
        interface IUnitTestItem
        {
            String? Title { get; }
            String? Description { get; }
            Nullable<Int32> IntValue { get; }
            Nullable<Boolean> BoolValue { get; }
            Nullable<Guid> GuidValue { get; }
        }

        class UnitTestItem : BindingTableRow, IUnitTestItem, INotifyPropertyChanged
        {
            public Nullable<Guid> GuidValue { get { return GetValue<Guid>("GuidValue"); } protected set { SetValue<Guid>("GuidValue", value); } }
            public Nullable<Boolean> BoolValue { get { return GetValue<Boolean>("BoolValue", BindingItemParsers.BooleanTryPrase); } set { SetValue<Boolean>("BoolValue", value); } }
            public Nullable<Int32> IntValue { get { return GetValue<Int32>("IntValue"); } set { SetValue<Int32>("IntValue", value); } }

            public String? Title { get { return GetValue("Title"); } set { SetValue("Title", value); } }
            public String? Description { get { return GetValue("Description"); } set { SetValue("Description", value); } }

            public UnitTestItem() : base()
            {
                GuidValue = Guid.NewGuid();
            }

            static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("GuidValue", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("BoolValue", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("IntValue", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("Title", typeof(String)){ AllowDBNull = true},
            new DataColumn("Description", typeof(String)){ AllowDBNull = true},
        };

            public override IReadOnlyList<DataColumn> ColumnDefinitions()
            { return columnDefinitions; }
        }


        BindingTable<UnitTestItem> data = new BindingTable<UnitTestItem>();

        public UnitTestGridView() : base()
        {
            InitializeComponent();
        }

        private void UnitTestGridView_Load(object sender, EventArgs e)
        { BindData(); }

        void BindData()
        {
            unitTestData.DataSource = data;

            var x = unitTestData.ReadOnly;
            

            //foreach (DataGridViewColumn item in unitTestData.Columns)
            //{ item.ReadOnly = false; }
        }

        void UnBindData() { unitTestData.DataSource = null; }
    }
}
