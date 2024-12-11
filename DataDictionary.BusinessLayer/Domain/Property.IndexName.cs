using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IPropertyIndexName : IPropertyKeyName
    { }

    /// <inheritdoc/>
    public class PropertyIndexName : PropertyKeyName, IPropertyIndexName,
        IKeyEquality<IPropertyIndexName>, IKeyEquality<PropertyIndexName>
    {
        /// <inheritdoc cref="PropertyKeyName(IPropertyKeyName)"/>
        public PropertyIndexName(IPropertyIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IPropertyIndexName? other)
        { return other is IPropertyKeyName key && Equals(new PropertyKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(PropertyIndexName? other)
        { return other is IPropertyKeyName key && Equals(new PropertyKeyName(key)); }

        /// <summary>
        /// Convert PropertyIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(PropertyIndexName source)
        { return new DataIndexName() { Title = source.PropertyTitle ?? String.Empty }; }
    }
}
