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
        public List<XmlBuilder> GetXmlBuilders()
        {   //TODO: Once working, move to various classes?

            List<XmlBuilder> builders = new List<XmlBuilder>();

            if (Model.Properties.Count > 0)
            {   // This could be called before Model.Properties has been loaded.
                var attributeValue = new XmlBuilder.ValueType(typeof(AttributeValue), ScopeType.ModelAttribute);
                builders.Add(attributeValue);
                builders.AddRange(attributeValue.Children.Select(s => s.Value));

                var attributeProperty = new XmlBuilder.PropertyType(ScopeType.ModelAttributeProperty, Model.Properties);
                builders.Add(attributeProperty);
                builders.AddRange(attributeProperty.Children.Select(s => s.Value));

                var entityValue = new XmlBuilder.ValueType(typeof(EntityValue), ScopeType.ModelEntity);
                builders.Add(entityValue);
                builders.AddRange(entityValue.Children.Select(s => s.Value));
                
                var entityProperty = new XmlBuilder.PropertyType(ScopeType.ModelEntityProperty, Model.Properties);
                builders.Add(entityProperty);
                builders.AddRange(entityProperty.Children.Select(s => s.Value));
            }

            return builders;
        }
    }
}
