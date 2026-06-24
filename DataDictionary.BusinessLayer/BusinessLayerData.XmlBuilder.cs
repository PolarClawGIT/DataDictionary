using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.BusinessLayer
{
    partial class BusinessLayerData
    {
        public XmlBuilderDictionary XmlBuilders
        {
            get
            {
                if (field is null || field.Count == 0)
                { field = GetXmlBuilders(); }

                return field;
            }
        }

        protected XmlBuilderDictionary GetXmlBuilders()
        {
            List<XmlBuilder> result = new List<XmlBuilder>();

            XmlBuilder attributes = new XmlBuilder.ValueType<AttributeValue>(ScopeType.ModelAttribute);
            result.Add(attributes);

            XmlBuilder attributeProperty = new XmlBuilder.PropertyType<AttributePropertyValue>(ScopeType.ModelAttributeProperty, Model.Properties);
            result.Add(attributeProperty);

            return new XmlBuilderDictionary(result);
        }
    }
}
