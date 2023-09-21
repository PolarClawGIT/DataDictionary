using DataDictionary.DataLayer.ApplicationData.Model;
using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using DataDictionary.DataLayer.DatabaseData.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using Toolbox.DbContext;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Alias Items
    /// </summary>
    public interface IDomainAttributeAliasItem : IDomainAttributeAliasKey, IDomainAttributeKey, IDbCatalogScope, IDbObjectScope, IDbElementScope, IBindingTableRow
    { }

    /// <summary>
    /// Implementation for Domain Attribute Alias Items
    /// </summary>
    [Serializable]
    public class DomainAttributeAliasItem : BindingTableRow, IDomainAttributeAliasItem
    {
        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>("AttributeId"); } protected set { SetValue("AttributeId", value); } }

        /// <inheritdoc/>
        public string? CatalogName { get { return GetValue("CatalogName"); } set { SetValue("CatalogName", value); } }

        /// <inheritdoc/>
        public string? SchemaName { get { return GetValue("SchemaName"); } set { SetValue("SchemaName", value); } }

        /// <inheritdoc/>
        public string? ObjectName { get { return GetValue("ObjectName"); } set { SetValue("ObjectName", value); } }

        /// <inheritdoc/>
        public string? ElementName { get { return GetValue("ElementName"); } set { SetValue("ElementName", value); } }

        /// <inheritdoc/>
        public DbCatalogScope CatalogScope { get; set; } = DbCatalogScope.NULL;

        /// <inheritdoc/>
        public DbObjectScope ObjectScope { get; set; } = DbObjectScope.NULL;

        /// <inheritdoc/>
        public DbElementScope ElementScope { get; set; } = DbElementScope.NULL;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn("AttributeId", typeof(Guid)){ AllowDBNull = true},
            new DataColumn("AttributeAliasId", typeof(int)){ AllowDBNull = true},
            new DataColumn("CatalogName", typeof(string)){ AllowDBNull = true},
            new DataColumn("SchemaName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ObjectName", typeof(string)){ AllowDBNull = true},
            new DataColumn("ElementName", typeof(string)){ AllowDBNull = true},
            new DataColumn("SysStart", typeof(DateTime)){ AllowDBNull = true},
        };

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        public DomainAttributeAliasItem() : base() { }

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="key"></param>
        public DomainAttributeAliasItem(IDomainAttributeKey key) : this()
        { AttributeId = key.AttributeId; }

        /// <summary>
        /// Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="key"></param>
        /// <param name="source"></param>
        public DomainAttributeAliasItem(IDomainAttributeKey key, IDbTableColumnItem source) : this()
        {
            AttributeId = key.AttributeId;
            CatalogName = source.CatalogName;
            SchemaName = source.SchemaName;
            ObjectName = source.TableName;
            ElementName = source.ColumnName;

            ElementScope = source.ElementScope;
        }

        /// <inheritdoc/>
        public override IReadOnlyList<DataColumn> ColumnDefinitions()
        { return columnDefinitions; }


        #region ISerializable
        /// <summary>
        /// Serialization Constructor for Domain Attribute Alias Items
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        protected DomainAttributeAliasItem(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(CatalogName)) { result.Append(CatalogName); }

            if (!string.IsNullOrWhiteSpace(SchemaName)) { result.Append(SchemaName); }
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(string.Format(".{0}", SchemaName)); }
                else { result.Append(SchemaName); }
            }

            if (!string.IsNullOrWhiteSpace(ObjectName))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(string.Format(".{0}", ObjectName)); }
                else { result.Append(ObjectName); }
            }

            if (!string.IsNullOrWhiteSpace(ElementName))
            {
                if (!string.IsNullOrWhiteSpace(result.ToString()))
                { result.Append(string.Format(".{0}", ElementName)); }
                else { result.Append(ElementName); }
            }

            return result.ToString();
        }

    }
}
