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
                builders.AddRange(XmlBuilder.Create(ScopeType.ModelAttribute, typeof(AttributeValue)));
                builders.AddRange(XmlBuilder.Create(ScopeType.ModelAttributeProperty, Model.Properties));

                builders.AddRange(XmlBuilder.Create(ScopeType.ModelEntity, typeof(EntityValue)));
                builders.AddRange(XmlBuilder.Create(ScopeType.ModelEntityProperty, Model.Properties));
                
            }

            return builders;
        }
    }
}
