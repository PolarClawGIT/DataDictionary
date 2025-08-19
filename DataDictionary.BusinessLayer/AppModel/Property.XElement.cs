using DataDictionary.BusinessLayer.AppScripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    partial class PropertyValue : IXElementFactory<IPropertyIndex, IPropertyValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class,
        /// configuring how the node retrieves and renders <see cref="PropertyValue"/>.
        /// </summary>
        /// <param name="propertyGet"></param>
        /// <returns></returns>
        public static IEnumerable<XElementBuilder> CreateXElements(
            TryGetValue<IPropertyIndex, IPropertyValue> propertyGet)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();

            result.Add(new XElementBuilder(
                nameof(IDefinitionValue.DefinitionTitle),
                (source) =>
                {
                    if (source is IPropertyIndex value
                        && propertyGet(value, out IPropertyValue? property)
                        && property.PropertyTitle is String)
                    { return property.PropertyTitle; }
                    else { return String.Empty; }
                }));

            return result;
        }
    }
}
