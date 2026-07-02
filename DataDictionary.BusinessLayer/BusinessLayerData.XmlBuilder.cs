using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        /// <summary>
        /// Generates a list of <b>default</b> XmlBuilders.
        /// </summary>
        /// <returns></returns>
        /// <example><![CDATA[
        ///     templateValues = new AppScripting.TemplateData()
        ///     { XmlBuilders = new AppScripting.XmlBuilderDictionary(GetXmlBuilders()) };]]>
        ///</example>
        public List<XmlBuilder> GetXmlBuilders()
        {   //TODO: Once working, move to various classes?
            
            List<XmlBuilder> builders = new List<XmlBuilder>();

            builders.Add(new XmlBuilder.ValueType(typeof(AttributeValue), ScopeType.ModelAttribute));
            builders.Add(new XmlBuilder.PropertyType(ScopeType.ModelAttributeProperty, Model.Properties));

            builders.Add(new XmlBuilder.ValueType(typeof(EntityValue), ScopeType.ModelEntity));
            builders.Add(new XmlBuilder.PropertyType(ScopeType.ModelEntityProperty, Model.Properties));

            return builders;
        }
    }
}
