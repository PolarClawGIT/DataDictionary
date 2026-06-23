using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.AppScripting;
using DataDictionary.BusinessLayer.ToolSet;
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

            XmlBuilder attributes = new XmlBuilder(Resource.Enumerations.ScopeType.ModelAttribute);
            result.Add(attributes);
            result.AddRange(attributes.CreateChildren<AttributeValue>());
            result.AddRange(attributes.CreateProperties(Model.Properties));

            return new XmlBuilderDictionary(result);
        }
    }
}
