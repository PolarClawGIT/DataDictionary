using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    public static class XElementFactory
    {
        public static XElement Build(AppModel.IAttributeValue attribute)
        {
            XElementBuilder builder = new XElementBuilder(attribute.Scope);
            builder.AddRange(XElementNode.Create(attribute.GetType()));
            builder[nameof(attribute.AttributeId)].RenderAs = TemplateNodeValueAsType.none;
            builder[nameof(attribute.Temporal)].RenderAs = TemplateNodeValueAsType.none;
            builder[nameof(attribute.Scope)].RenderAs = TemplateNodeValueAsType.none;
            builder[nameof(attribute.AttributeTitle)].RenderAs = TemplateNodeValueAsType.Attribute;

            return builder.Build(attribute);
        }

        public static IEnumerable<XObject> Build(IEnumerable<AppModel.IAttributePropertyValue> properties, IPropertyGetValue propertyGet)
        {
            List<XObject> result = new List<XObject>();

            if (properties.FirstOrDefault() is AppModel.IAttributePropertyValue property)
            {
                XElementBuilder builder = new XElementBuilder(property.Scope);

                builder.Add(
                        new XElementNode(nameof(PropertyValue.PropertyTitle))
                        {
                            GetValue = (value) =>
                            {
                                if (value is IAttributePropertyValue attributeProperty
                                    && propertyGet.TryGetValue(
                                        attributeProperty,
                                        out IPropertyValue? propertyValue))
                                { return propertyValue.PropertyTitle; }
                                else { return null; }
                            },
                            RenderAs = TemplateNodeValueAsType.Attribute
                        });

                builder.AddRange(XElementNode.Create(property.GetType()));
                builder[nameof(property.AttributeId)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(property.Temporal)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(property.Scope)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(property.PropertyId)].RenderAs = TemplateNodeValueAsType.none;

                foreach (var item in properties)
                { result.Add(builder.Build(item)); }
            }

            return result;
        }

        public static IEnumerable<XObject> Build(IEnumerable<AppModel.IAttributeDefinitionValue> definitions, IDefinitionGetValue definitionGet)
        {
            List<XObject> result = new List<XObject>();

            if (definitions.FirstOrDefault() is AppModel.IAttributeDefinitionValue definition)
            {
                XElementBuilder builder = new XElementBuilder(definition.Scope);

                builder.Add(
                        new XElementNode(nameof(DefinitionValue.DefinitionTitle))
                        {
                            GetValue = (value) =>
                            {
                                if (value is IAttributeDefinitionValue attributeDefinition
                                    && definitionGet.TryGetValue(
                                        attributeDefinition,
                                        out IDefinitionValue? definitionValue))
                                { return definitionValue.DefinitionTitle; }
                                else { return null; }
                            },
                            RenderAs = TemplateNodeValueAsType.Attribute
                        });

                builder.AddRange(XElementNode.Create(definition.GetType()));
                builder[nameof(definition.AttributeId)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(definition.Temporal)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(definition.Scope)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(definition.DefinitionId)].RenderAs = TemplateNodeValueAsType.none;
                builder[nameof(definition.DefinitionText)].RenderAs = TemplateNodeValueAsType.ElementCData;
            }

            return result;
        }
    }
}
