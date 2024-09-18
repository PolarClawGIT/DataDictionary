using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.Main.Forms
{
    partial class HistoryView
    {
        protected class HistoryViewItem : IModificationValue
        {
            IModificationValue sourceValue;

            public HistoryViewItem(IModificationValue source)
            { sourceValue = source; }

            //public DataLayerIndex DataLayerId => sourceValue.Get;
            public Guid Index => sourceValue.Index;
            public String Title => sourceValue.Title;
            public String Description => String.Empty;
            public String? ModifiedBy => sourceValue.ModifiedBy;
            public DateTime? ModifiedOn => sourceValue.ModifiedOn;
            public DbModificationType Modification => sourceValue.Modification;

            public event PropertyChangedEventHandler? PropertyChanged
            {
                add { sourceValue.PropertyChanged += value; }
                remove { sourceValue.PropertyChanged -= value; }
            }

            public IModificationValue AsModificationValue()
            { return sourceValue; }

            IDataValue IDataValue.AsDataValue()
            {
                throw new NotImplementedException();
            }
        }

        protected class HistoryViewItems : BindingList<HistoryViewItem>
        {
            protected override Boolean SupportsSortingCore { get { return true; } }

            protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
            {
                var x = this.Items.SelectMany(s => s.GetType().GetProperties());
                var y = x.FirstOrDefault(w => w.Name.Equals(prop.Name));
                //TODO: Figure out how to support sorting
                base.ApplySortCore(prop, direction);
            }

            protected override void RemoveSortCore()
            {
                base.RemoveSortCore();
            }
        }
    }
}
