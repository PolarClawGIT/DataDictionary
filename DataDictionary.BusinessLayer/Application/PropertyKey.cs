using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public class PropertyKey : DataLayer.ApplicationData.Property.PropertyKey
    {
        /// <inheritdoc/>
        public PropertyKey(IPropertyKey source) : base(source) { }
    }
}
