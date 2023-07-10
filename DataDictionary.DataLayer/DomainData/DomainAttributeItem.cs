using DataDictionary.DataLayer.DbMetaData;
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
    public interface IDomainAttributeItem : IDomainAttributeTitle, IDomainAttributeId, IDomainAttributeParentId
    {
        Nullable<Guid> CatalogId { get; }
        String? AttributeText { get; set; }
        Boolean IsComposite { get { return CompositeOrder > 0; } }
        Nullable<Int32> CompositeOrder { get; set; }
    }

    public class DomainAttributeItem : BindingTableRow, IDomainAttributeItem
    {
        public Nullable<Guid> CatalogId
        { get { return GetValue<Guid>("CatalogId"); } protected set { SetValue<Guid>("CatalogId", value); } }

        public Nullable<Guid> AttributeId
        { get { return GetValue<Guid>("AttributeId"); } set { SetValue<Guid>("AttributeId", value); } }

        public Nullable<Guid> ParentAttributeId
        { get { return GetValue<Guid>("ParentAttributeId"); } set { SetValue<Guid>("ParentAttributeId", value); } }

        public String? AttributeTitle { get { return GetValue("AttributeTitle"); } set { SetValue("AttributeTitle", value); } }
        public String? AttributeText { get { return GetValue("AttributeText"); } set { SetValue("AttributeText", value); } }
        public Nullable<Int32> CompositeOrder { get { return GetValue<Int32>("CompositeOrder"); } set { SetValue<Int32>("CompositeOrder", value); } }

        public DomainAttributeItem() : base()
        {
            if (AttributeId is null) { AttributeId = Guid.NewGuid(); }
        }

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

    public static class DomainAttributeItemExtension
    {
        public static IDomainAttributeItem? GetAttribute(this IEnumerable<IDomainAttributeItem> source, IDomainAttributeId item)
        { return source.FirstOrDefault(w => w.AttributeId == item.AttributeId); }

        public static IDomainAttributeItem? GetAttribute(this IDomainAttributeId item, IEnumerable<IDomainAttributeItem> source)
        { return source.FirstOrDefault(w => w.AttributeId == item.AttributeId); }

        public static IDomainAttributeItem? GetParentAttribute(this IEnumerable<IDomainAttributeItem> source, IDomainAttributeParentId item)
        { return source.FirstOrDefault(w => w.AttributeId == item.ParentAttributeId); }

        public static IDomainAttributeItem? GetParentAttribute(this IDomainAttributeParentId item, IEnumerable<IDomainAttributeItem> source)
        { return source.FirstOrDefault(w => w.AttributeId == item.ParentAttributeId); }
    }
}
