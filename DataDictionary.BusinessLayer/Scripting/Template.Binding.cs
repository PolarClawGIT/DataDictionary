using DataDictionary.BusinessLayer.DbWorkItem;
using DataDictionary.BusinessLayer.Domain;
using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData.Scope;
using DataDictionary.DataLayer.DomainData.Property;
using System.Xml.Linq;
using Toolbox.BindingTable;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Wrapper around a single Template and all its components for Data Binding.
    /// </summary>
    public class TemplateBinding
    {
        /// <inheritdoc cref="TemplateValue"/>
        public ITemplateValue Template { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateAttributes"/>
        public BindingView<TemplateAttributeValue> Attributes { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateNodes"/>
        public BindingView<TemplateNodeValue> Nodes { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplatePaths"/>
        public BindingView<TemplatePathValue> Paths { get; protected set; }

        /// <inheritdoc cref="ScriptingEngine.TemplateDocuments"/>
        public BindingView<TemplateDocumentValue> Documents { get; protected set; }

        internal TemplateBinding(ITemplateIndex templateKey, ScriptingEngine source)
        {
            TemplateIndex key = new TemplateIndex(templateKey);

            if (source.Templates.FirstOrDefault(w => key.Equals(w)) is ITemplateValue template)
            {
                Template = template;
                Attributes = new BindingView<TemplateAttributeValue>(source.TemplateAttributes, w => key.Equals(w));
                Nodes = new BindingView<TemplateNodeValue>(source.TemplateNodes, w => key.Equals(w));
                Paths = new BindingView<TemplatePathValue>(source.TemplatePaths, w => key.Equals(w));
                Documents = new BindingView<TemplateDocumentValue>(source.TemplateDocuments, w => key.Equals(w));
            }
            else
            {
                Exception ex = new ArgumentException("Template not found");
                ex.Data.Add(nameof(templateKey), templateKey);
                throw ex;
            }
        }

        /// <summary>
        /// Given the Scope, return the Node settings
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IEnumerable<TemplateNodeValue> GetNodes (ScopeType scope)
        {
            ScopeKey key = new ScopeKey(scope);
            return Nodes.Where(w => key.Equals(w));
        }

        /// <summary>
        /// Given the Scope, Return the Attributes
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IEnumerable<TemplateAttributeValue> GetAttributes(ScopeType scope)
        {
            ScopeKey key = new ScopeKey(scope);
            return Attributes.Where(w => key.Equals(w));
        }

        internal IEnumerable<XAttribute> BuildXAttributes(ScopeType scope, IPropertyData domainProperties, IEnumerable<IDomainProperty> properties)
        {
            List<XAttribute> result = new List<XAttribute>();

            foreach (ITemplateAttributeValue item in GetAttributes(scope))
            {
                PropertyIndex propertyKey = new PropertyIndex(item);

                if (properties.FirstOrDefault(w => propertyKey.Equals(w)) is IDomainProperty property)
                {
                    String attributeName = String.Empty;
                    String attributeValue = String.Empty;

                    if (!String.IsNullOrWhiteSpace(item.AttributeName))
                    { attributeName = item.AttributeName; }
                    else if (domainProperties.FirstOrDefault(w => propertyKey.Equals(w)) is PropertyValue domainProperty
                        && !String.IsNullOrWhiteSpace(domainProperty.PropertyTitle))
                    { attributeName = domainProperty.PropertyTitle; }

                    if (!String.IsNullOrWhiteSpace(property.PropertyValue))
                    { attributeValue = property.PropertyValue; }
                    else if (!String.IsNullOrWhiteSpace(item.AttributeValue))
                    { attributeValue = item.AttributeValue; }

                    if (!String.IsNullOrWhiteSpace(attributeName) && !String.IsNullOrWhiteSpace(attributeValue))
                    {
                        if (item.AsCData == true)
                        { result.Add(new XAttribute(attributeName, new XCData(attributeValue))); }
                        else { result.Add(new XAttribute(attributeName, attributeValue)); }
                    }
                }
                else if (!String.IsNullOrWhiteSpace(item.AttributeName) && !String.IsNullOrWhiteSpace(item.AttributeValue))
                {
                    if (item.AsCData == true) { result.Add(new XAttribute(item.AttributeName, new XCData(item.AttributeValue))); }
                    else { result.Add(new XAttribute(item.AttributeName, item.AttributeValue)); }
                }

            }

            return result;
        }

       
    }
}
