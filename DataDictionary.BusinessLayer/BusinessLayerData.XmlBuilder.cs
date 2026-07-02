using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        //TODO: How to move this into TemplateData?

        /// <summary>
        /// List of XmlBuilders supported by the application.
        /// </summary>
        /// <remarks>Initialized when the Get is first called.</remarks>
        public XmlBuilderDictionary XmlBuilders { get; }

        /// <summary>
        /// Initializes all the XML Builders.
        /// </summary>
        /// <returns></returns>
        protected XmlBuilderDictionary InitXmlBuilders()
        {   //TODO: Once working, move to various classes?

            List<XmlBuilder> builders = new List<XmlBuilder>();

            builders.Add(new XmlBuilder.ValueType(typeof(AttributeValue), ScopeType.ModelAttribute));
            builders.Add(new XmlBuilder.PropertyType(ScopeType.ModelAttributeProperty, Model.Properties));

            builders.Add(new XmlBuilder.ValueType(typeof(EntityValue), ScopeType.ModelEntity));
            builders.Add(new XmlBuilder.PropertyType(ScopeType.ModelEntityProperty, Model.Properties));

            return new XmlBuilderDictionary(builders);
        }
    }
}
