using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributePropertyItem : IDomainAttributeId
    {
        public Nullable<Guid> PropertyId { get; }
        public String? PropertyName { get; }
        public String? PropertyValue { get; }
    }

    public class DomainAttributePropertyItem : BindingTableRow, IDomainAttributePropertyItem
    {
        public Nullable<Guid> AttributeId { get { return GetValue<Guid>("AttributeId"); } set { SetValue<Guid>("AttributeId", value); } }
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } set { SetValue("PropertyName", value); } }
        public String? PropertyValue { get { return GetValue("PropertyValue"); } set { SetValue("PropertyValue", value); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = true},
            new DataColumn("PropertyValue", typeof(String)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }
    }

    public static class DomainAttributePropertyItemExtension
    {
        public static IEnumerable<DomainAttributePropertyItem> GetProperties(this IEnumerable<DomainAttributePropertyItem> source, IDomainAttributeId item)
        { return source.Where(w => item.AttributeId == w.AttributeId); }
    }
}
