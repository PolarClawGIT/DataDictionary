using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeAliasItem : IDomainAttributeId
    {
        public String? CatalogName { get; }
        public String? SchemaName { get; }
        public String? ObjectName { get; }
        public String? ElementName { get; }
    }

    public class DomainAttributeAliasItem : BindingTableRow, IDomainAttributeAliasItem
    {
        public Nullable<Guid> AttributeId
        { get { return GetValue<Guid>("AttributeId"); } set { SetValue<Guid>("AttributeId", value); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } set { SetValue("CatalogName", value); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } set { SetValue("SchemaName", value); } }
        public String? ObjectName { get { return GetValue("ObjectName"); } set { SetValue("ObjectName", value); } }
        public String? ElementName { get { return GetValue("ElementName"); } set { SetValue("ElementName", value); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = true},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ObjectName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementName", typeof(String)){ AllowDBNull = true},

        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(CatalogName)) { result.Append(CatalogName); }

            if (!String.IsNullOrWhiteSpace(SchemaName)) { result.Append(SchemaName); }
            {
                if (!String.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(String.Format(".{0}", SchemaName)); }
                else { result.Append(SchemaName); }
            }

            if (!String.IsNullOrWhiteSpace(ObjectName))
            {
                if (!String.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(String.Format(".{0}", ObjectName)); }
                else { result.Append(ObjectName); }
            }

            if (!String.IsNullOrWhiteSpace(ElementName))
            {
                if (!String.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(String.Format(".{0}", ElementName)); }
                else { result.Append(ElementName); }
            }

            return result.ToString();
        }

    }

    public static class DomainAttributeAliasItemExtension
    {
        public static IEnumerable<DomainAttributeAliasItem> GetProperties(this IEnumerable<DomainAttributeAliasItem> source, IDomainAttributeId item)
        { return source.Where(w => item.AttributeId == w.AttributeId); }
    }
}
