using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// List of XmlBuilders supported by the application.
        /// </summary>
        /// <remarks>Initialized when the Get is first called.</remarks>
        public XmlBuilderDictionary XmlBuilders
        {
            get
            {
                if (field is null || field.Count == 0)
                { field = InitXmlBuilders(); }

                return field;
            }
        }

        /// <summary>
        /// Initializes all the XML Builders.
        /// </summary>
        /// <returns></returns>
        protected XmlBuilderDictionary InitXmlBuilders()
        {
            List<XmlBuilder> result = new List<XmlBuilder>();

            XmlBuilder attributes = new XmlBuilder.ValueType(typeof(AttributeValue), ScopeType.ModelAttribute);
            result.Add(attributes);

            XmlBuilder attributeProperty = new XmlBuilder.PropertyType(ScopeType.ModelAttributeProperty, Model.Properties);
            result.Add(attributeProperty);

            return new XmlBuilderDictionary(result);
        }
    }
}
