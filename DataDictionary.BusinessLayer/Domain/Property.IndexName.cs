using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.DomainData.Property;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IPropertyIndexName : IDomainPropertyKeyName
    { }

    /// <inheritdoc/>
    public class PropertyIndexName : DomainPropertyKeyName, IPropertyIndexName,
        IKeyEquality<IPropertyIndexName>, IKeyEquality<PropertyIndexName>
    {
        /// <inheritdoc cref="DomainPropertyKeyName(IDomainPropertyKeyName)"/>
        public PropertyIndexName(IPropertyIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyIndexName? other)
        { return other is IDomainPropertyKeyName key && Equals(new DomainPropertyKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(PropertyIndexName? other)
        { return other is IDomainPropertyKeyName key && Equals(new DomainPropertyKeyName(key)); }

        /// <summary>
        /// Convert PropertyIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(PropertyIndexName source)
        { return new DataIndexName() { Title = source.PropertyTitle ?? String.Empty }; }
    }
}
