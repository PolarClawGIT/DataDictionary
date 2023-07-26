using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
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
    public interface IDomainAttributePropertyItem : IDomainAttributeIdentifier
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
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection, IModelIdentifier modelId)
        { return GetData(connection, (modelId.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? attributeId, String? propertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            command.AddParameter("@PropertyName", parameters.propertyName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelIdentifier modelId, IBindingTable<DomainAttributePropertyItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeProperty]", source);
            return command;
        }

        public override String ToString()
        {
            if (PropertyName is not null && PropertyValue is not null)
            { return String.Format("{0}: {1}", PropertyName, PropertyValue); }
            else { return String.Empty; }
        }
    }

    public static class DomainAttributePropertyItemExtension
    {
        public static IEnumerable<DomainAttributePropertyItem> GetProperties(this IEnumerable<DomainAttributePropertyItem> source, IDomainAttributeIdentifier item)
        { return source.Where(w => item.AttributeId == w.AttributeId); }
    }
}
