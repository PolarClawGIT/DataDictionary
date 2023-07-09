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
        public String? ScopeName { get; }
        public String? ObjectName { get; }
        public String? ElementName { get; }
    }

    public class DomainAttributeAliasItem : BindingTableRow, IDomainAttributeAliasItem
    {
        public Nullable<Guid> AttributeId
        { get { return GetValue<Guid>("AttributeId"); } set { SetValue<Guid>("AttributeId", value); } }
        public String? ScopeName { get { return GetValue("ScopeName"); } set { SetValue("ScopeName", value); } }
        public String? ObjectName { get { return GetValue("ObjectName"); } set { SetValue("ObjectName", value); } }
        public String? ElementName { get { return GetValue("ElementName"); } set { SetValue("ElementName", value); } }


        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("ScopeName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ObjectName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementName", typeof(String)){ AllowDBNull = true},

        };

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            if(!String.IsNullOrWhiteSpace(ScopeName)) { result.Append(ScopeName); }

            if (!String.IsNullOrWhiteSpace(ObjectName))
            {
                if (!String.IsNullOrWhiteSpace(result.ToString()))
                {   result.Append(String.Format(".{0}", ObjectName)); }
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

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

    }
}
