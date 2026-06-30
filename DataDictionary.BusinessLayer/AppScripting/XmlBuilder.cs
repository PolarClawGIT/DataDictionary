using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System.Xml;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    //public delegate Boolean TryGetProperty(IPropertyIndex key, [NotNullWhen(true)] out IPropertyValue? value);
    //public delegate Boolean TryGetDefinition(IDefinitionIndex key, [NotNullWhen(true)] out IDefinitionValue? value);

    /// <summary>
    /// Definition to Build an Xml Element
    /// </summary>
    public partial class XmlBuilder
    {
        /// <summary>
        /// Scope of the Object to be Rendered. This is normally the owning Object Scope Type.
        /// </summary>
        protected ScopeType ObjectScope { get; private set; } = ScopeType.Null;

        /// <summary>
        /// Path to the Object to be Rendered. This is normally a Property of the Object.
        /// </summary>
        public PathIndex ObjectPath { get; private set; }

        /// <inheritdoc cref="INodeRenderAs.NodeRenderAs"/>
        public NodeRenderAsType NodeRenderAs { get; set; } = NodeRenderAsType.Element;

        /// <summary>
        /// The Name of the Node. Default is the MemberName of the ObjectPath.
        /// </summary>
        public String NodeName
        {
            get
            {
                String value = field ?? String.Empty;

                if (String.IsNullOrWhiteSpace(value))
                {   // Use the ObjectPath after it has been cleaned up for XML.
                    value = String.Concat(ObjectPath.Member.Where(c => !Char.IsWhiteSpace(c)));
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
                    String path = String.Concat(ObjectPath.Member.Where(c => !Char.IsWhiteSpace(c)));
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

        /// <summary>
        /// Basic XmlBuilder constructor. The value is set to the ToString of the Build object.
        /// </summary>
        /// <param name="scope"></param>
        public XmlBuilder(ScopeType scope) : base()
        {
            ObjectScope = scope;
            ObjectPath = new PathIndex(PathIndex.Parse(scope.GetName()));

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        /// <summary>
        /// Creates a Clone of the XML Builder
        /// </summary>
        /// <param name="source"></param>
        public XmlBuilder(XmlBuilder source) : this(source.ObjectScope)
        {
            ObjectPath = new PathIndex(source.ObjectPath);
            NodeRenderAs = source.NodeRenderAs;
            NodeName = source.NodeName;
            GetValue = source.GetValue;
        }

        /// <summary>
        /// GetValue function that returns the ToString of the object passed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Override this method to return something other then ToString.
        /// Overloads handle specific data types.
        /// When calling use: GetValueDelegate((dynamic)objectValue);
        /// </remarks>
        protected virtual String? GetValueDelegate(Object value)
        {
            if (value is null) { return null; }
            else { return value.ToString(); }
        }

        /// <summary>
        /// Build an XML Object using the builder for the Object passed.
        /// </summary>
        /// <typeparam name="TMethod"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <inheritdoc/>
        public override String ToString()
        { return ObjectPath.MemberFullPath; }

        /// <inheritdoc cref="ICloneable.Clone"/>
        public XmlBuilder Clone()
        {
            if(this is ValueType valueType) { return new ValueType(valueType); }
            else if (this is PropertyType propertyType){ return new PropertyType(propertyType); }
            else { return new XmlBuilder(this); }
        }
    }
}
