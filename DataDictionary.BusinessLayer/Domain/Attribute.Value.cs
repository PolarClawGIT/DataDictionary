﻿using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.BusinessLayer.Scripting;
using DataDictionary.DataLayer.DomainData.Attribute;
using DataDictionary.Resource.Enumerations;
using System.ComponentModel;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAttributeValue : IDomainAttributeItem, IAttributeIndex, IAttributeIndexName
    { }

    /// <inheritdoc/>
    public class AttributeValue : DomainAttributeItem, IAttributeValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DomainAttributeItem()"/>
        public AttributeValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new AttributeIndex(this); }

        /// <inheritdoc/>
        public String GetTitle()
        { return AttributeTitle ?? ScopeEnumeration.Cast(Scope).Name; }

        /// <inheritdoc/>
        /// <remarks>Partial Path</remarks>
        public NamedScopePath GetPath()
        {
            if (String.IsNullOrWhiteSpace(MemberName))
            { return new NamedScopePath(AttributeTitle); }
            else { return new NamedScopePath(new NamedScopePath(NamedScopePath.Parse(MemberName).ToArray())); }
        }

        /// <inheritdoc/>
        public Boolean IsTitleChanged(PropertyChangedEventArgs eventArgs)
        { return eventArgs.PropertyName is nameof(AttributeTitle) or nameof(MemberName); }

        internal static IReadOnlyList<NodePropertyValue> GetXColumns()
        {
            ScopeType scope = ScopeType.ModelAttribute;
            IAttributeValue attributeNames;
            List<NodePropertyValue> result = new List<NodePropertyValue>()
            {
                new NodePropertyValue() {PropertyName = nameof(attributeNames.AttributeId),          DataType = typeof(Guid),    AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.AttributeTitle),       DataType = typeof(String),  AllowDBNull = false, PropertyScope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.AttributeDescription), DataType = typeof(String),  AllowDBNull = true,  PropertyScope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsCompositeType),      DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsSimpleType),         DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},

                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsDerived),            DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsIntegral),           DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsKey),                DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},
                //new ColumnItem() {ColumnName = nameof(attributeNames.IsNonKey),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},

              //new ColumnItem() {ColumnName = nameof(attributeNames.IsMultiValue),         DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsSingleValue),        DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},

                new NodePropertyValue() {PropertyName = nameof(attributeNames.IsNullable),           DataType = typeof(Boolean), AllowDBNull = true,  PropertyScope = scope},
              //new ColumnItem() {ColumnName = nameof(attributeNames.IsValued),             DataType = typeof(Boolean), AllowDBNull = true,  Scope = scope},
            };

            return result;
        }

    }
}
