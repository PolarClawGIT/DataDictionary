﻿using DataDictionary.Resource.Enumerations;
using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.DomainData.Attribute
{
    /// <summary>
    /// Interface for Domain Attribute Alias Items
    /// </summary>
    public interface IDomainAttributeAliasItem : IDomainAttributeKey, IAliasKey, IScopeType
    { }

    /// <summary>
    /// Implementation for Domain Attribute Alias Items
    /// </summary>
    [Serializable]
    public class DomainAttributeAliasItem : BindingTableRow, IDomainAttributeAliasItem
    {

        /// <inheritdoc/>
        public Guid? AttributeId
        { get { return GetValue<Guid>(nameof(AttributeId)); } protected set { SetValue(nameof(AttributeId), value); } }

        /// <inheritdoc/>
        public String? AliasName { get { return GetValue(nameof(AliasName)); } set { SetValue(nameof(AliasName), value); } }

        /// <inheritdoc/>
        public ScopeType AliasScope
        {
            get
            {
                String value = GetValue(nameof(AliasScope)) ?? String.Empty;
                if (ScopeEnumeration.TryParse(value, null, out ScopeEnumeration? result))
                { return result.Value; }
                else { return ScopeType.Null; }
            }
            set
            {
                if (value is ScopeType.Null) { SetValue(nameof(AliasScope), null); }
                else { SetValue(nameof(AliasScope), ScopeEnumeration.Cast(value).Name); }
            }
        }

        /// <inheritdoc/>
        public ScopeType Scope { get; } = ScopeType.ModelAttributeAlias;

        static readonly IReadOnlyList<DataColumn> columnDefinitions = new List<DataColumn>()
        {
            new DataColumn(nameof(AttributeId), typeof(Guid)){ AllowDBNull = true},
            new DataColumn(nameof(AliasName), typeof(String)){ AllowDBNull = true},
            new DataColumn(nameof(AliasScope), typeof(String)){ AllowDBNull = true},
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
        public override String ToString()
        {
            if (AliasName is String) { return AliasName; }
            else { return String.Empty; }
        }

    }
}
