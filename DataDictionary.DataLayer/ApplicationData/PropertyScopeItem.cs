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
    public interface IPropertyScopeItem : IPropertyIdentifier
    {
        String? ScopeType { get; }
        String? ObjectType { get; }
        String? ElementType { get; }
    }

    public class PropertyScopeItem : BindingTableRow, IPropertyScopeItem
    {
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } protected set { SetValue<Guid>("PropertyId", value); } }
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
            new DataColumn("ScopeType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ObjectType", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementType", typeof(String)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? PropertyId, String? ScopeType, String? ObjectType, String? ElementType) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationPropertyScope]";

            command.AddParameter("@PropertyId", parameters.PropertyId);
            command.AddParameter("@ScopeType", parameters.ScopeType);
            command.AddParameter("@ObjectType", parameters.ObjectType);
            command.AddParameter("@ElementType", parameters.ElementType);
            return command;
        }

        public static IReadOnlyList<(String? ScopeType, String? ObjectType, String? ElementType)>
            ScopeTypes = new List<(String? ScopeType, String? ObjectType, String? ElementType)>()
            {
                // Level0 supported
                // ("ASSEMBLY", null, null),
                // ("CONTRACT", null, null),
                // ("EVENT NOTIFICATION", null, null),
                // ("FILEGROUP", null, null),
                // ("MESSAGE TYPE", null, null),
                // ("PARTITION FUNCTION", null, null),
                // ("PARTITION SCHEME", null, null),
                // ("REMOTE SERVICE BINDING", null, null),
                // ("ROUTE", null, null),
                ("SCHEMA", null, null),
                // ("SERVICE", null, null),
                // ("USER", null, null),
                // ("TRIGGER", null, null),
                // ("TYPE", null, null),
                // ("PLAN GUIDE", null, null),

                // Level1 supported
	            // (null, "AGGREGATE", null),
	            // (null, "DEFAULT", null),
	            ("SCHEMA", "FUNCTION", null),
	            // (null, "LOGICAL FILE NAME", null),
	            ("SCHEMA", "PROCEDURE", null),
	            // (null, "QUEUE", null),
	            // (null, "RULE", null),
	            // (null, "SEQUENCE", null),
	            // (null, "SYNONYM", null),
	            ("SCHEMA", "TABLE", null),
	            // (null, "TABLE_TYPE", null),
	            // (null, "TYPE", null),
	            ("SCHEMA", "VIEW", null),
	            // (null, "XML SCHEMA COLLECTION'AGGREGATE", null),
                // (null, "DEFAULT", null),
                ("SCHEMA", "FUNCTION", null),
                // (null, "LOGICAL FILE NAME", null),
                ("SCHEMA", "PROCEDURE", null),
                // (null, "QUEUE", null),
                // (null, "RULE", null),
                // (null, "SEQUENCE", null),
                // (null, "SYNONYM", null),
                ("SCHEMA", "TABLE", null),
                // (null, "TABLE_TYPE", null),
                // (null, "TYPE", null),
                ("SCHEMA", "VIEW", null),
                // ("SSCHEMA", "XML SCHEMA COLLECTION", null),

                // Level2 supported
                ("SCHEMA", "TABLE", "COLUMN"),
                ("SCHEMA", "VIEW", "COLUMN"),
                // (null, null, "CONSTRAINT"),
                // (null, null, "EVENT NOTIFICATION"),
                // (null, null, "INDEX"),
                ("SCHEMA", "PROCEDURE", "PARAMETER"),
                ("SCHEMA", "FUNCTION", "PARAMETER"),
                // (null, null, "TRIGGER'
            };

    }
}
