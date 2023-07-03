using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeItem: IDomainAttributeTitle, IDomainAttributeId
    {
        Guid? ParentAttributeId { get; set; }
        String? AttributeText { get; set; }
    }

    public class DomainAttributeItem : BindingTableRow, IDomainAttributeItem, INotifyPropertyChanged
    {
        public Guid? AttributeId { get; set; }
        public Guid? ParentAttributeId { get; set; }
        public String? AttributeTitle { get; set; }
        public String? AttributeText { get; set; }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        {
            throw new NotImplementedException();
        }
    }
}
