using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.ApplicationData
{
    public interface IPropertyItem
    {

    }

    public class PropertyItem : BindingTableRow, IPropertyItem
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyTitle { get { return GetValue("PropertyTitle"); } set { SetValue("PropertyTitle", value); } }
        public Nullable<Boolean> IsExtendedProperty { get { return GetValue<Boolean>("IsExtendedProperty", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("IsExtendedProperty", value); } }
        public String? PropertyName { get { return GetValue("PropertyName"); } set { SetValue("PropertyName", value); } }
        public String? ScopeType { get { return GetValue("ScopeType"); } set { SetValue("ScopeType", value); } }
        public String? ObjectType { get { return GetValue("ObjectType"); } set { SetValue("ObjectType", value); } }
        public String? ElementType { get { return GetValue("ElementType"); } set { SetValue("ElementType", value); } }

        public PropertyItem() : base()
        {
            if (PropertyId is null) { PropertyId = Guid.NewGuid(); }
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("PropertyTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("IsExtendedProperty", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("PropertyName", typeof(String)){ AllowDBNull = false},
            new DataColumn("ScopeType", typeof(String)){ AllowDBNull = false},
            new DataColumn("ObjectType", typeof(String)){ AllowDBNull = false},
            new DataColumn("ElementType", typeof(String)){ AllowDBNull = false},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? helpId, String? helpSubject, String? nameSpace, Boolean? obsolete) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationHelp]";

            command.AddParameter("@HelpId", parameters.helpId);
            command.AddParameter("@HelpSubject", parameters.helpSubject);
            command.AddParameter("@NameSpace", parameters.nameSpace);
            command.AddParameter("@Obsolete", parameters.obsolete);
            return command;
        }

        public static Command SetData(IConnection connection, IBindingTable<HelpItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationHelp]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationHelp]", source);
            return command;
        }
    }
}
