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
        {   //TODO: Once working, move to various classes?

            List<XmlBuilder> result = new List<XmlBuilder>();

            result.Add(new XmlBuilder.ValueType(typeof(AttributeValue), ScopeType.ModelAttribute));
            result.Add(new XmlBuilder.PropertyType(ScopeType.ModelAttributeProperty, Model.Properties));

            result.Add(new XmlBuilder.ValueType(typeof(EntityValue), ScopeType.ModelEntity));
            result.Add(new XmlBuilder.PropertyType(ScopeType.ModelEntityProperty, Model.Properties));

            return new XmlBuilderDictionary(result);
        }
    }
}
