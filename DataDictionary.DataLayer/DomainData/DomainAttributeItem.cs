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
    public interface IDomainAttributeItem : IDomainAttributeTitle, IDomainAttributeId
    {
        Nullable<Guid> CatalogId { get; set; }
        Nullable<Guid> ParentAttributeId { get; set; }
        String? AttributeText { get; set; }
        Boolean IsComposite { get { return CompositeOrder > 0; } }
        Nullable<Int32> CompositeOrder { get; set; }
    }

    public class DomainAttributeItem : BindingTableRow, IDomainAttributeItem, INotifyPropertyChanged
    {
        public Nullable<Guid> CatalogId
        { get { return GetValue<Guid>("CatalogId"); } set { SetValue<Guid>("CatalogId", value); } }

        public Nullable<Guid> AttributeId
        { get { return GetValue<Guid>("AttributeId"); } set { SetValue<Guid>("AttributeId", value); } }

        public Nullable<Guid> ParentAttributeId
        { get { return GetValue<Guid>("ParentAttributeId"); } set { SetValue<Guid>("ParentAttributeId", value); } }

        public String? AttributeTitle { get { return GetValue("AttributeTitle"); } set { SetValue("AttributeTitle", value); } }
        public String? AttributeText { get { return GetValue("AttributeText"); } set { SetValue("AttributeText", value); } }
        public Nullable<Int32> CompositeOrder { get { return GetValue<Int32>("CompositeOrder"); } set { SetValue<Int32>("CompositeOrder", value); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("CatalogId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("ParentAttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AttributeTitle", typeof(String)){ AllowDBNull = true},
            new DataColumn("AttributeText", typeof(String)){ AllowDBNull = true},
            new DataColumn("CompositeOrder", typeof(Int32)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }
}
