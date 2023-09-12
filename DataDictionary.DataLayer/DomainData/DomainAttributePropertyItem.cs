using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DatabaseData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributePropertyItem : IDomainAttributePropertyKey, IBindingTableRow
    {
        public String? PropertyValue { get; }
        public String? DefinitionText { get; }
        public String? ExtendedPropertyValue { get; }
        public String? ChoiceValue { get; }
    }

    [Serializable]
    public class DomainAttributePropertyItem : BindingTableRow, IDomainAttributePropertyItem, ISerializable
    {
        public Nullable<Guid> AttributeId { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue<Guid>("AttributeId", value); } }
        public Nullable<Guid> PropertyId { get { return GetValue<Guid>("PropertyId"); } set { SetValue<Guid>("PropertyId", value); } }
        public String? PropertyValue { get { return GetValue("PropertyValue"); } set { SetValue("PropertyValue", value); } }
        public String? DefinitionText { get { return GetValue("DefinitionText"); } set { SetValue("DefinitionText", value); } }
        public String? ExtendedPropertyValue { get { return GetValue("ExtendedPropertyValue"); } set { SetValue("ExtendedPropertyValue", value); } }
        public String? ChoiceValue { get { return GetValue("ChoiceValue"); } set { SetValue("ChoiceValue", value); } }

        public DomainAttributePropertyItem() : base() { }

        public DomainAttributePropertyItem(IDomainAttributeKey attributeKey) : this() 
        { AttributeId = attributeKey.AttributeId; }

        public DomainAttributePropertyItem(IDomainAttributeKey attributeKey, IPropertyKey propertyKey, IDbExtendedPropertyItem value) : this() 
        {
            AttributeId = attributeKey.AttributeId;
            PropertyId = propertyKey.PropertyId;
            ExtendedPropertyValue = value.PropertyValue;
        }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("PropertyValue", typeof(String)){ AllowDBNull = true},
            new DataColumn("DefinitionText", typeof(String)){ AllowDBNull = true},
            new DataColumn("ExtendedPropertyValue", typeof(String)){ AllowDBNull = true},
            new DataColumn("ChoiceValue", typeof(String)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? attributeId, String? propertyName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeProperty]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DomainAttributePropertyItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeProperty]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeProperty]", source);
            return command;
        }

        #region ISerializable
        protected DomainAttributePropertyItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion
    }

    public static class DomainAttributePropertyItemExtension
    {
        public static IEnumerable<DomainAttributePropertyItem> GetProperties(this IEnumerable<DomainAttributePropertyItem> source, IDomainAttributeKey item)
        { return source.Where(w => item.AttributeId == w.AttributeId); }
    }
}
