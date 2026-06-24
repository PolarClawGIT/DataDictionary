using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;
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

        public XElement Build(IScopeType root, params IEnumerable<IEnumerable<IScopeType>> childData)
        {
            PathIndex key = new PathIndex(root.Scope);

            if (TryGetValue(key, out XmlBuilder? rootBuilder)
                && rootBuilder.Build(root) is XElement result)
            {

                foreach (var data in childData)
                {
                    foreach (var child in data)
                    {
                        PathIndex childKey = new PathIndex(child.Scope);

                        if (TryGetValue(childKey, out XmlBuilder? childBuilder))
                        {
                            result.Add(childBuilder.Build(child));
                        }
                    }
                }


                return result;
            }
            else
            { return new XElement(new XmlBuilder(root.Scope).NodeName); }
        }

        // TODO: Need to return a list including children so a tree structure can be built.
        // TODO: Need a way to load and save to the database. Rebuild into SchemaNode?
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
                String value = field;

                if (String.IsNullOrWhiteSpace(value))
                {   // Use the ObjectPath after it has been cleaned up for XML.
                    value = String.Concat(ObjectPath.Format("{0}").Where(c => !Char.IsWhiteSpace(c)));
                    value = XmlConvert.EncodeName(value);
                }

                return value;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                { field = String.Empty; }
                else
                {   // Clean up the value before storing.
                    value = String.Concat(value.Where(c => !Char.IsWhiteSpace(c)));
                    value = XmlConvert.EncodeName(value);

                    // compare this to the ObjectPath
                    String path = String.Concat(ObjectPath.Format("{0}").Where(c => !Char.IsWhiteSpace(c)));
                    path = XmlConvert.EncodeName(value);

                    if (value == path) // Set to use the ObjectPath instead.
                    { field = String.Empty; }
                    else { field = value; }
                }
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

        public virtual XObject? Build<TMethod>(TMethod value)
            where TMethod : class
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



        /// <inheritdoc/>
        public override String ToString()
        { return ObjectPath.MemberFullPath; }

        // --------------------------------------------- Sub Types --------------------------

        public class ValueType<TValue> : XmlBuilder
            where TValue : class, IScopeType
        {
            public Dictionary<PropertyInfo, XmlBuilder> Children = new Dictionary<PropertyInfo, XmlBuilder>();

            public ValueType(ScopeType scope, params IEnumerable<String> properties) : base(scope)
            {
                GetValue = (value) => GetValueDelegate((dynamic)value);

                Type value = typeof(TValue);

                foreach (PropertyInfo item in value.GetProperties().
                    Where(w => properties.Count() == 0 || properties.Any(a => String.Equals(a, w.Name))))
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item);
                    Children.Add(item, child);
                }
            }

            public override XObject? Build<TMethod>(TMethod value)
            {
                XObject? result = base.Build(value);

                if (result is XElement element)
                {
                    foreach (var item in Children.Values)
                    { element.Add(item.Build(value)); }
                }
                else
                {
                    throw new NotImplementedException(); // Not sure what to do here.
                }

                return result;
            }
        }

        public class PropertyType<TValue> : XmlBuilder
            where TValue : IPropertySubType
        {
            public Dictionary<PropertyIndex, XmlBuilder> Children = new Dictionary<PropertyIndex, XmlBuilder>();

            public PropertyType(ScopeType scope, IEnumerable<PropertyValue> properties) : base(scope)
            {
                foreach (PropertyValue item in properties)
                {
                    XmlBuilder child = new XmlBuilder(ObjectScope, item);
                    Children.Add(new PropertyIndex(item), child);
                }
            }

            public override XObject? Build<TMethod>(TMethod value)
            {
                XObject? result = base.Build(value);

                if (result is XElement element
                    && value is IPropertySubType property)
                {
                    PropertyIndex key = new PropertyIndex(property);
                    if (Children.TryGetValue(key, out XmlBuilder? builder))
                    { result = builder.Build(value); }
                }
                else
                {
                    throw new NotImplementedException(); // Not sure what to do here.
                }

                return result;
            }
        }

    }
}
