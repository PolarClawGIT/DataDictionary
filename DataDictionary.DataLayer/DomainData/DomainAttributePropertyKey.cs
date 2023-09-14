﻿using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    public interface IDomainAttributePropertyKey : IDomainAttributeKey, IPropertyKey
    { }

    public class DomainAttributePropertyKey : IDomainAttributePropertyKey, IEquatable<DomainAttributePropertyKey>
    {
        public Guid? AttributeId { get; init; } = Guid.Empty;
        public Guid? PropertyId { get; init; } = Guid.Empty;

        public DomainAttributePropertyKey(IDomainAttributePropertyKey source)
        {
            AttributeId = source.AttributeId;
            PropertyId = source.PropertyId;
        }

        #region IEquatable
        public bool Equals(DomainAttributePropertyKey? other)
        {
            return other is IDomainAttributePropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(AttributeId, key.AttributeId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
        }

        public override bool Equals(object? obj)
        { return obj is IDomainAttributePropertyKey value && this.Equals(new DomainAttributePropertyKey(value)); }

        public override int GetHashCode()
        { return HashCode.Combine(AttributeId, PropertyId); }
        #endregion
    }
}
