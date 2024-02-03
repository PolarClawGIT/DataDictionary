using DataDictionary.DataLayer.ApplicationData.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.DomainData
{
    /// <summary>
    /// Interface for the Domain Property
    /// </summary>
    public interface IDomainProperty: IPropertyKey
    {
        /// <summary>
        /// Property Value. Type and Format is dependent on PeropertyId.
        /// </summary>
        public string? PropertyValue { get; set; }

        /// <summary>
        /// Property Value for Rich Text Definition properties.
        /// </summary>
        public string? DefinitionText { get; set; }
    }
}
