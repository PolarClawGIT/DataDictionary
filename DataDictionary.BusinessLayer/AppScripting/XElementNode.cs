using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Reflection;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    public class XElementNode
    {
        /// <inheritdoc cref="IScriptingNode.NodeName"/>
        public String NodeName { get; set; }

        /// <inheritdoc cref="IScriptingNodeKeyName.PropertyName"/>
        public String PropertyName { get; init; }

        /// <inheritdoc cref="INodeValueAsType.NodeValueAs"/>
        public TemplateNodeValueAsType NodeValueAs { get; set; } = TemplateNodeValueAsType.none;

        public Func<Object, String?> GetValue { get; set; }

        public XElementNode(
            String name,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.Element)
            : base()
        {
            NodeName = name;
            PropertyName = name;

            if (String.IsNullOrWhiteSpace(name))
            { NodeValueAs = TemplateNodeValueAsType.none; }
            else { NodeValueAs = renderAs; }

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        public XElementNode(
            IPropertyGetValue propertyGet,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.Element)
            :this (nameof(IPropertyValue.PropertyTitle), renderAs)
        {
            GetValue = (value) => 
            { 
                if(value is IPropertyIndex index
                    && propertyGet.TryGetValue(index, out IPropertyValue? result))
                {   return result.PropertyTitle; }
                else { return null; }
            };
        }

        public XElementNode(
            IDefinitionGetValue definitionGet,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.Element)
            : this(nameof(IDefinitionValue.DefinitionTitle), renderAs)
        {
            GetValue = (value) =>
            {
                if (value is IDefinitionValue index
                    && definitionGet.TryGetValue(index, out IDefinitionValue? result))
                { return result.DefinitionTitle; }
                else { return null; }
            };
        }

        public XElementNode(ScopeType scope) : this(ScopeEnumeration.Cast(scope).Name)
        { }

        public XElementNode(PropertyInfo property) : this(property.Name)
        { GetValue = (value) => GetValueDelegate(property, value); }

        public static IEnumerable<XElementNode> Create(Type value)
        {
            List<XElementNode> result = new List<XElementNode>();

            foreach (PropertyInfo property in value.GetProperties().ToList())
            {
                result.Add(
                new XElementNode(property)
                { NodeValueAs = TemplateNodeValueAsType.ElementText });
            }

            return result;
        }

        public static IEnumerable<XElementNode> Create(ScopeType scope, IEnumerable<ITemplateNodeValue> scripting)
        {
            List<XElementNode> result = new List<XElementNode>();

            foreach (PropertyInfo property in XElementEnumeration.GetProperties(scope))
            {
                TemplateNodeIndexName key = new TemplateNodeIndexName(scope, property);

                if (scripting.FirstOrDefault(w => key.Equals(w)) is ITemplateNodeValue nodeSetting)
                {
                    result.Add(
                    new XElementNode(property)
                    {
                        NodeValueAs = nodeSetting.NodeValueAs,
                        NodeName = nodeSetting.NodeName ?? property.Name
                    });
                }
                else
                {
                    result.Add(
                    new XElementNode(property)
                    { NodeValueAs = TemplateNodeValueAsType.none });
                }


            }

            return result;
        }

        public virtual String? GetValueDelegate(Object value)
        {
            if (value is null) { return null; }
            else { return value.ToString(); }
        }

        public virtual String? GetValueDelegate(ScopeType value)
        { return ScopeEnumeration.Cast(value).Name; }

        public virtual String? GetValueDelegate(PropertyInfo property, Object value)
        {
            if (property.GetValue(value) is Object objectValue)
            { return GetValueDelegate((dynamic)objectValue); ; }
            else { return null; }
        }

        public virtual String? GetValueDelegate(PathIndex value)
        { return value.MemberFullPath; }

        public virtual String? GetValueDelegate(IPropertyGetValue property, IPropertyIndex value)
        {
            PropertyIndex key = new PropertyIndex(value);

            if(property.TryGetValue(value, out IPropertyValue? result))
            {   return result.PropertyTitle; }
            else { return null; }
               
        }

        public virtual XObject? Build(Object value)
        {
            String? nodeValue = GetValue(value);

            if (String.IsNullOrEmpty(NodeName) || NodeValueAs is TemplateNodeValueAsType.none)
            { return null; }

            switch (NodeValueAs)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.Element:
                    return new XElement(NodeName);
                case TemplateNodeValueAsType.ElementText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, nodeValue);
                case TemplateNodeValueAsType.ElementCData:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, new XCData(nodeValue));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                        return new XElement(NodeName, XElement.Parse(nodeValue));
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(NodeName), NodeName);
                        fragementEx.Data.Add(nameof(nodeValue), nodeValue);
                        fragementEx.Data.Add(nameof(NodeValueAs), NodeValueAs.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, value);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(TemplateNodeValueAsType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(NodeValueAs), NodeValueAs.ToString());
                    throw ex;
            }
        }

        public override String ToString()
        { return NodeName; }
    }
}
