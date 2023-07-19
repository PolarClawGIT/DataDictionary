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
        String? AttributeDescription { get; set; }
    }

    public class DomainAttributeItem : BindingTableRow, IDomainAttributeItem
    {
        public Nullable<Guid> AttributeId
        { get { return GetValue<Guid>("AttributeId"); } set { SetValue<Guid>("AttributeId", value); } }

        public Nullable<Guid> ParentAttributeId
        { get { return GetValue<Guid>("ParentAttributeId"); } set { SetValue<Guid>("ParentAttributeId", value); } }

        public String? AttributeTitle { get { return GetValue("AttributeTitle"); } set { SetValue("AttributeTitle", value); } }
        public String? AttributeDescription { get { return GetValue("AttributeDescription"); } set { SetValue("AttributeDescription", value); } }
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryPrase); } set { SetValue<Boolean>("Obsolete", value); } }

        public DomainAttributeItem() : base()
        {
            if (AttributeId is null) { AttributeId = Guid.NewGuid(); }
            if(Obsolete is null) { Obsolete = false; }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("ParentAttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AttributeTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("AttributeDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
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
