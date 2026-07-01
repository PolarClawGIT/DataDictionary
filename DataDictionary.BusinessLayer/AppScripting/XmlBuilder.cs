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
        public ScopeType ObjectScope
        {
            get
            {
                if (GetScope is not null)
                { field = GetScope(); }

                return field;
            }
            set
            {
                // Remember the current Node Name, if it is different then the MemberName of the NodePath.
                String nodeName = NodeName; 

                field = value;
                NodePath = new PathIndex(PathIndex.Parse(field.GetName()));
                NodeName = nodeName; // Reset the NodeName

                if (SetScope is not null)
                { SetScope(value); }
            }
        }

        /// <summary>
        /// Delegate used to assign a call back when Get ObjectScope is called.
        /// </summary>
        public Func<ScopeType>? GetScope { protected get; init; }

        /// <summary>
        /// Delegate used to assign a call back when Get ObjectScope is called.
        /// </summary>
        public Action<ScopeType>? SetScope { protected get; init; }

        /// <summary>
        /// The Name of the Node. Default is the MemberName of the NodePath.
        /// </summary>
        public String NodeName
        {
            get
            {
                if (GetName is not null)
                { field = GetName(); }

                String value = field ?? String.Empty;

                if (String.IsNullOrWhiteSpace(value) && NodePath is not null)
                {   // Use the ObjectPath after it has been cleaned up for XML.
                    value = String.Concat(NodePath.Member.Where(c => !Char.IsWhiteSpace(c)));
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
                    String path = String.Concat(NodePath.Member.Where(c => !Char.IsWhiteSpace(c)));
                    path = XmlConvert.EncodeName(value);

                    if (value == path) // Set to use the ObjectPath instead.
                    { field = String.Empty; }
                    else { field = value; }

                    if (SetName is not null)
                    { SetName(value); }
                }
            }
        }

        /// <summary>
        /// Delegate used to assign a call back when Get NodeName is called.
        /// </summary>
        public Func<String>? GetName { protected get; init; }

        /// <summary>
        /// Delegate used to assign a call back when Set NodeName is called.
        /// </summary>
        public Action<String>? SetName { protected get; init; }

        /// <summary>
        /// Path to the Object to be Rendered. This is normally a Property of the Object.
        /// </summary>
        public PathIndex NodePath { get; private set; }

        /// <inheritdoc cref="INodeRenderAs.NodeRenderAs"/>
        public NodeRenderAsType RenderValueAs
        {
            get
            {
                if (GetRenderAs is not null)
                { field = GetRenderAs(); }

                return field;
            }
            set
            {
                field = value;

                if (SetRenderAs is not null)
                { SetRenderAs(value); }
            }
        }

        /// <summary>
        /// Delegate used to assign a call back when Get RenderValueAs is called.
        /// </summary>
        public Func<NodeRenderAsType>? GetRenderAs { protected get; init; }

        /// <summary>
        /// Delegate used to assign a call back when Set RenderValueAs is called.
        /// </summary>
        public Action<NodeRenderAsType>? SetRenderAs { protected get; init; }

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
            NodePath = new PathIndex(PathIndex.Parse(scope.GetName()));
            RenderValueAs = NodeRenderAsType.Element;

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        /// <summary>
        /// Creates a Clone of the XML Builder
        /// </summary>
        /// <param name="source"></param>
        public XmlBuilder(XmlBuilder source) : this(source.ObjectScope)
        {
            NodePath = new PathIndex(source.NodePath);
            RenderValueAs = source.RenderValueAs;
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

            if (String.IsNullOrEmpty(NodeName) || RenderValueAs is NodeRenderAsType.none)
            { return null; }

            switch (RenderValueAs)
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
                        fragementEx.Data.Add(nameof(RenderValueAs), RenderValueAs.ToString());
                        throw;
                    }
                case NodeRenderAsType.AttributeText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(NodeRenderAsType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(RenderValueAs), RenderValueAs.ToString());
                    throw ex;
            }
        }

        /// <inheritdoc/>
        public override String ToString()
        { return NodePath.MemberFullPath; }

        /// <inheritdoc cref="ICloneable.Clone"/>
        public XmlBuilder Clone()
        {
            if (this is ValueType valueType) { return new ValueType(valueType); }
            else if (this is PropertyType propertyType) { return new PropertyType(propertyType); }
            else { return new XmlBuilder(this); }
        }
    }
}
