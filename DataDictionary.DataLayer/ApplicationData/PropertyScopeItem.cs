using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyScopeItem : IPropertyIdentifier
    {
        String? PropertyName { get; }
        String? ScopeType { get; }
        String? ObjectType { get; }
        String? ElementType { get; }
    }

    public class PropertyScopeItem : BindingTableRow, IPropertyScopeItem
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } set { SetValue("PropertyName", value); } }
        public String? ScopeType { get { return GetValue("ScopeType"); } set { SetValue("ScopeType", value); } }
        public String? ObjectType { get { return GetValue("ObjectType"); } set { SetValue("ObjectType", value); } }
        public String? ElementType { get { return GetValue("ElementType"); } set { SetValue("ElementType", value); } }

        public PropertyScopeItem() : base()
        {
            PropertyId = Guid.NewGuid();
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ScopeType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ObjectType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementType", typeof(String)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

    }
}
