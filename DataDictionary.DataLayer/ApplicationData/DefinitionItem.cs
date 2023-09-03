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
    public interface IDefinitionItem : IDefinitionKey
    {
        String? DefinitionTitle { get; }
    }

    public class DefinitionItem : BindingTableRow, IDefinitionItem
    {
        public Nullable<Guid> DefinitionId { get { return GetValue<Guid>("DefinitionId"); } protected set { SetValue<Guid>("DefinitionId", value); } }
        public String? DefinitionTitle { get { return GetValue("DefinitionTitle"); } set { SetValue("DefinitionTitle", value); } }
        public String? DefinitionDescription { get { return GetValue("DefinitionDescription"); } set { SetValue("DefinitionDescription", value); } }
        public Nullable<Boolean> Obsolete { get { return GetValue<Boolean>("Obsolete", BindingItemParsers.BooleanTryParse); } set { SetValue<Boolean>("Obsolete", value); } }

        public DefinitionItem() : base()
        {
            DefinitionId = Guid.NewGuid();
            DefinitionTitle = "new Definition";
            Obsolete = false;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("DefinitionId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("DefinitionTitle", typeof(String)){ AllowDBNull = false},
            new DataColumn("DefinitionDescription", typeof(String)){ AllowDBNull = true},
            new DataColumn("Obsolete", typeof(Boolean)){ AllowDBNull = false},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null)); }

        static Command GetData(IConnection connection, (Guid? DefinitionId, String? DefinitionTitle, String? DefinitionName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetApplicationDefinition]";

            command.AddParameter("@DefinitionId", parameters.DefinitionId);
            command.AddParameter("@DefinitionTitle", parameters.DefinitionTitle);
            return command;
        }

        public static Command SetData(IConnection connection, IBindingTable<DefinitionItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetApplicationDefinition]";
            command.AddParameter("@Data", "[App_DataDictionary].[typeApplicationDefinition]", source);
            return command;
        }

    }
}
