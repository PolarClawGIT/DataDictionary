using DataDictionary.DataLayer.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IPropertySubType : IProperty, IPropertyIndex
    {
        Guid? IPropertyKey.PropertyId { get { return PropertyId; } }
        String? IProperty.PropertyValue { get { return PropertyValue; } }

        /// <inheritdoc cref="IPropertyKey.PropertyId"/>
        new Guid? PropertyId { get; set; }

        /// <inheritdoc cref="IProperty.PropertyValue"/>
        new String? PropertyValue { get; set; }
    }
}
