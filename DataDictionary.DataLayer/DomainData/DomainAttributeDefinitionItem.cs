using DataDictionary.DataLayer.ApplicationData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeDefinitionItem : IDomainAttributeDefinitionKey, IBindingTableRow
    {
        String? DefinitionText { get; }
    }

    public class DomainAttributeDefinitionItem : BindingTableRow, IDomainAttributeDefinitionItem
    {
        public Nullable<Guid> AttributeId { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue<Guid>("AttributeId", value); } }
        public Nullable<Guid> DefinitionId { get { return GetValue<Guid>("DefinitionId"); } protected set { SetValue<Guid>("DefinitionId", value); } }
        public String? DefinitionText { get { return GetValue("DefinitionText"); } set { SetValue("DefinitionText", value); } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("DefinitionId", typeof(Guid)){ AllowDBNull = false},
            new DataColumn("DefinitionText", typeof(String)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection)
        { return GetData(connection, (null, null, null)); }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? AttributeId, Guid? DefinitionId) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeDefinition]";
            command.AddParameter("@modelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.AttributeId);
            command.AddParameter("@DefinitionId", parameters.DefinitionId);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DomainAttributeDefinitionItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeDefinition]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeDefinition]", source);
            return command;
        }
    }
}
