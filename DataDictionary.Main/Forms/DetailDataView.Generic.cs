using DataDictionary.BusinessLayer;
using DataDictionary.Main.Enumerations;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    class DetailDataView<TValue, TForm> : DetailDataView
        where TValue : class
        where TForm : ApplicationBase
    {
        /// <summary>
        /// The Constructor for the Detail/Selected Form.
        /// </summary>
        public Func<TValue, TForm>? SelectedForm { get; init; }


        public DetailDataView(ScopeType scope, IBindingData data) : base (scope, data)
        { }

        public DetailDataView(ScopeType scope, IBindingList data) : base(scope, data)
        { }

        public DetailDataView(ScopeType scope, IBindingTable data) : base(scope, data)
        { }

        protected override void RowHeaderMouseDoubleClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            base.RowHeaderMouseDoubleClick(sender, e);

            if (SelectedItem is TValue value && SelectedForm is not null)
            { Activate(() => SelectedForm(value)); }
        }
    }
}
