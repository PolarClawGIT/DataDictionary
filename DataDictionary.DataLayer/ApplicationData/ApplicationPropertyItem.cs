using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IApplicationPropertyItem 
    {
        public Nullable<Guid> PropertyId { get; }
        public String? PropertyTitle { get;  }
        public Nullable<Boolean> IsExtendedProperty { get; }
        public String? PropertyName { get; }
        public String? ScopeType { get; }
        public String? ObjectType { get; }
        public String? ElementType { get; }
    }

    public class ApplicationPropertyItem : BindingTableRow, IApplicationPropertyItem
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } set { SetValue<Guid>("PropertyId", value); } }

        public String? PropertyTitle { get { return GetValue("PropertyTitle"); } set { SetValue("PropertyTitle", value); } }
        public Nullable<Boolean> IsExtendedProperty { get { return GetValue<Boolean>("IsExtendedProperty", BindingItemParsers.BooleanTryPrase)); } set { SetValue<Boolean>("IsExtendedProperty", value ); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } set { SetValue("PropertyName", value); } }
        public String? ScopeType { get { return GetValue("ScopeType"); } set { SetValue("ScopeType", value); } }
        public String? ObjectType { get { return GetValue("ObjectType"); } set { SetValue("ObjectType", value); } }
        public String? ElementType { get { return GetValue("ElementType"); } set { SetValue("ElementType", value); } }

        public ApplicationPropertyItem() : base()
        { }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyTitle", typeof(String)){ AllowDBNull = true},
            new DataColumn("IsExtendedProperty", typeof(Boolean)){ AllowDBNull = true},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ScopeType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ObjectType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementType", typeof(String)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static BindingTable<ApplicationPropertyItem> Create ()
        {
            // 

            BindingTable<ApplicationPropertyItem> result = new BindingTable<ApplicationPropertyItem>()
            {
                new ApplicationPropertyItem(){ PropertyId = new Guid() }
            };

            return result;
        }
    }
}
