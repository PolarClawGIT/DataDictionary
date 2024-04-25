using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IPropertyIndex : IPropertyKey
    { }

    /// <inheritdoc/>
    public class PropertyIndex : PropertyKey
    {
        /// <inheritdoc cref="PropertyKey.PropertyKey(IPropertyKey)"/>
        public PropertyIndex(IPropertyIndex source) : base(source) { }
    }
}
