using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    // TODO: Thinking things thru. XElementBuilder is not exactly what is needed.

    public delegate Boolean TryGetProperty(IPropertyIndex key, [NotNullWhen(true)] out IPropertyValue? value);

    public delegate Boolean TryGetDefinition(IDefinitionIndex key, [NotNullWhen(true)] out IDefinitionValue? value);


    public class XmlBuilderDictionary : Dictionary<PathIndex, XmlBuilder>
    {
        //public required TryGetProperty GetProperty { get; init; } 

        //public required TryGetDefinition GetDefinition { get; init; }



        public XmlBuilderDictionary(IEnumerable<XmlBuilder> builders) : base()
        {
            foreach (XmlBuilder item in builders)
            { Add(item.ObjectPath, item); }
        }

        public XElement? Build(IScopeType item)
        {
            PathIndex key = new PathIndex(item.Scope);

            if (TryGetValue(key, out XmlBuilder? value)
                && value.Build(item) is XElement result)
            {
                Type source = item.GetType();

                foreach (var property in source.GetProperties())
                {
                    PathIndex childKey = new PathIndex(property.Name).Merge(key);

                    if (TryGetValue(childKey, out XmlBuilder? child))
                    { result.Add(child.Build(item)); }

                }

                return result;
            }

            return null;
        }

    }

    /// <summary>
    /// Definition to Build an Xml Element
    /// </summary>
    public class XmlBuilder
    {
        /// <summary>
        /// Path to the Object to be Rendered. This is normally a Property of the Object.
        /// </summary>
        public PathIndex ObjectPath { get; private set; }

        /// <summary>
        /// Scope of the Object to be Rendered. This is normally the owning Object Scope Type.
        /// </summary>
        protected ScopeType ObjectScope { get; private set; } = ScopeType.Null;

        /// <inheritdoc cref="INodeRenderAs.NodeRenderAs"/>
        public NodeRenderAsType NodeRenderAs { get; set; } = NodeRenderAsType.Element;

        /// <summary>
        /// The Name of the Node. Default is the Object Path.
        /// </summary>
        public String NodeName
        {
            get
            {
                String result = field;

                if (String.IsNullOrWhiteSpace(result))
                { result = ObjectPath.Format("{0}"); }

                try
                {   // TODO: How do I catch name excpetions and return result.
                    XElement element = new XElement(result);
                    return element.Name.LocalName;
                }
                catch (Exception ex)
                { return ex.GetType().Name; }
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                { field = String.Empty; }
                else { field = value; }
            }
        }

        /// <summary>
        /// Function that returns the NodeValue.
        /// </summary>
        protected Func<Object, String> GetValue { get; set; }

        public XmlBuilder(ScopeType scope) : base()
        {
            ObjectScope = scope;
            ObjectPath = new PathIndex(PathIndex.Parse(scope.GetName()));

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        protected XmlBuilder(ScopeType scope, PropertyInfo property) : this(scope)
        {
            ObjectPath = new PathIndex(property.Name).Merge(ObjectPath);
            NodeRenderAs = NodeRenderAsType.ElementText;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;
        }

        protected XmlBuilder(ScopeType scope, IPropertyValue property) : this(scope)
        {
            ObjectPath = new PathIndex(property.PropertyTitle).Merge(ObjectPath);
            NodeRenderAs = NodeRenderAsType.ElementText;
            GetValue = (value) => GetValueDelegate((dynamic)value, property) ?? String.Empty;
        }

        protected virtual String? GetValueDelegate(Object value)
        {
            if (value is null) { return null; }
            else { return value.ToString(); }
        }

        protected virtual String? GetValueDelegate(Object value, PropertyInfo property)
        {
            if (property.GetValue(value) is Object objectValue)
            { return GetValueDelegate((dynamic)objectValue); ; }
            else { return null; }
        }

        protected virtual String? GetValueDelegate(Object value, IPropertyIndex property)
        {
            PropertyIndex key = new PropertyIndex(property);

            if (value is IPropertySubType propertyValue && key.Equals(propertyValue))
            { return propertyValue.PropertyValue; }
            else { return null; }
        }

        public virtual XObject? Build<TValue>(TValue value)
            where TValue : class
        {
            if (value is not IScopeType scope || scope.Scope != ObjectScope)
            {
                Exception ex = new ArgumentException("Incorrect Scope");
                ex.Data.Add(nameof(ObjectScope), ObjectScope.GetName());
                ex.Data.Add(nameof(Type.FullName), value.GetType().FullName);
                throw ex;
            }

            String? nodeValue = GetValue(value);

            if (String.IsNullOrEmpty(NodeName) || NodeRenderAs is NodeRenderAsType.none)
            { return null; }

            switch (NodeRenderAs)
            {
                case NodeRenderAsType.none:
                    return null;
                case NodeRenderAsType.Element:
                    return new XElement(NodeName);
                case NodeRenderAsType.ElementText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, nodeValue);
                case NodeRenderAsType.ElementCData:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XElement(NodeName, new XCData(nodeValue));
                case NodeRenderAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                        return new XElement(NodeName, XElement.Parse(nodeValue));
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(NodeName), NodeName);
                        fragementEx.Data.Add(nameof(nodeValue), nodeValue);
                        fragementEx.Data.Add(nameof(NodeRenderAs), NodeRenderAs.ToString());
                        throw;
                    }
                case NodeRenderAsType.AttributeText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(NodeRenderAsType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(NodeRenderAs), NodeRenderAs.ToString());
                    throw ex;
            }
        }

        //TODO: These belong to diffrent classes?

        public virtual IEnumerable<XmlBuilder> CreateChildren<TValue>(params IEnumerable<String> properties)
        {
            List<XmlBuilder> result = new List<XmlBuilder>();
            Type value = typeof(TValue);

            foreach (PropertyInfo property in value.GetProperties().
                Where(w => properties.Count() == 0 || properties.Any(a => String.Equals(a, w.Name))))
            { result.Add(new XmlBuilder(ObjectScope, property)); }

            return result;
        }

        public virtual IEnumerable<XmlBuilder> CreateProperties(IEnumerable<IPropertyValue> values)
        {
            List<XmlBuilder> result = new List<XmlBuilder>();

            foreach (IPropertyValue item in values)
            { result.Add(new XmlBuilder(ObjectScope, item)); }

            return result;
        }

        /// <inheritdoc/>
        public override String ToString()
        { return ObjectPath.MemberFullPath; }
    }
}
