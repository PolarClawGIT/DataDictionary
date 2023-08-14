using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.DataLayer.DbMetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributeAliasItem : IDomainAttributeKey, IBindingTableRow
    {
        public String? CatalogName { get; }
        public String? SchemaName { get; }
        public String? ObjectName { get; }
        public String? ElementName { get; }
    }

    public class DomainAttributeAliasItem : BindingTableRow, IDomainAttributeAliasItem
    {
        public Nullable<Guid> AttributeId
        { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue<Guid>("AttributeId", value); } }
        public String? CatalogName { get { return GetValue("CatalogName"); } set { SetValue("CatalogName", value); } }
        public String? SchemaName { get { return GetValue("SchemaName"); } set { SetValue("SchemaName", value); } }
        public String? ObjectName { get { return GetValue("ObjectName"); } set { SetValue("ObjectName", value); } }
        public String? ElementName { get { return GetValue("ElementName"); } set { SetValue("ElementName", value); } }

        [Browsable(false)]
        public DomainAttributeKey AttributeKey { get { return new DomainAttributeKey(this); } set { AttributeId = value.AttributeId; } }

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AttributeAliasId", typeof(Int32)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(String)){ AllowDBNull = true},
            new DataColumn("SchemaName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ObjectName", typeof(String)){ AllowDBNull = true},
            new DataColumn("ElementName", typeof(String)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        public DomainAttributeAliasItem() : base() { }

        public DomainAttributeAliasItem(IDomainAttributeKey key, IDbTableColumnItem source) :this ()
        {
            AttributeId = key.AttributeId;
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
            ObjectName = source.TableName;
            ElementName = source.ColumnName;
        }

        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }

        public static Command GetData(IConnection connection, IModelKey modelId)
        { return GetData(connection, (modelId.ModelId, null, null, null, null, null)); }

        static Command GetData(IConnection connection, (Guid? modelId, Guid? attributeId, String? catalogName, String? schemaName, String? objectName, String? elementName) parameters)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procGetDomainAttributeAlias]";
            command.AddParameter("@ModelId", parameters.modelId);
            command.AddParameter("@AttributeId", parameters.attributeId);
            command.AddParameter("@CatalogName", parameters.catalogName);
            command.AddParameter("@SchemaName", parameters.schemaName);
            command.AddParameter("@ObjectName", parameters.objectName);
            command.AddParameter("@ElementName", parameters.elementName);
            return command;
        }

        public static Command SetData(IConnection connection, IModelKey modelId, IBindingTable<DomainAttributeAliasItem> source)
        {
            Command command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[App_DataDictionary].[procSetDomainAttributeAlias]";
            command.AddParameter("@ModelId", modelId.ModelId);
            command.AddParameter("@Data", "[App_DataDictionary].[typeDomainAttributeAlias]", source);
            return command;
        }

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
        public static IEnumerable<DomainAttributeAliasItem> GetProperties(this IEnumerable<DomainAttributeAliasItem> source, IDomainAttributeKey item)
        { return source.Where(w => item.AttributeId == w.AttributeId); }
    }
}
