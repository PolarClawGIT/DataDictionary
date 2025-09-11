using DataDictionary.BusinessLayer.AppModel;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Interface for results of the factory method that builds the XElement Builders.
    /// </summary>
    public interface IXElementBuilderList : IReadOnlyDictionary<ScopeType, IEnumerable<XElementBuilder>>
    { }

    /// <summary>
    /// Base Class used for the factory method that builds the XElement Builders.
    /// </summary>
    class XElementBuilderList : Dictionary<ScopeType, IEnumerable<XElementBuilder>>, IXElementBuilderList 
    { }

    /// <summary>
    /// Represents a node in an XML structure with customizable behavior for rendering and value retrieval.
    /// </summary>
    /// <remarks>
    /// This class provides functionality to define and manipulate XML nodes, including their names,
    /// rendering types, and value retrieval logic. It supports various rendering modes such as elements, attributes,
    /// and CDATA sections. Instances of this class can be created directly or generated from types and
    /// properties.
    /// </remarks>
    public class XElementBuilder
    {
        /// <summary>
        /// Name to apply to the Node (default is PropertyName)
        /// </summary>
        public String NodeName { get; set; }

        /// <summary>
        ///Name of the Property (column) of the Node (used to select the item to render).
        /// </summary>
        public String PropertyName { get; init; }

        /// <inheritdoc cref="INodeRenderAs.NodeRenderAs"/>
        public NodeRenderAsType NodeValueAs { get; set; } = NodeRenderAsType.none;

        /// <summary>
        /// Gets or sets the function used to retrieve a string representation of an object.
        /// </summary>
        /// <remarks>
        /// The function can be customized to define how objects are converted to strings.
        /// If the function is not set, ensure that any usage of this property accounts for its nullability.
        /// </remarks>
        public Func<Object, String?> GetValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class with the specified name and rendering
        /// type.
        /// </summary>
        /// <remarks>
        /// If the <paramref name="name"/> parameter is null, empty, or consists only of
        /// whitespace, the <see cref="NodeValueAs"/> property will be set to <see cref="NodeRenderAsType.none"/>.
        /// Otherwise, it will be set to the value of the <paramref name="renderAs"/> parameter.</remarks>
        /// <param name="name">The name of the node. This value is used to set both the <see cref="NodeName"/> and <see cref="PropertyName"/> properties.</param>
        /// <param name="renderAs">Specifies how the node's value should be rendered. The default is <see cref="NodeRenderAsType.Element"/>.</param>
        public XElementBuilder(
            String name,
            NodeRenderAsType renderAs = NodeRenderAsType.Element)
            : base()
        {
            NodeName = name;
            PropertyName = name;

            if (String.IsNullOrWhiteSpace(name))
            { NodeValueAs = NodeRenderAsType.none; }
            else { NodeValueAs = renderAs; }

            GetValue = (value) => GetValueDelegate((dynamic)value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class,
        /// configuring how property values are retrieved and rendered.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getValue"></param>
        /// <param name="renderAs"></param>
        public XElementBuilder(
            String name,
            Func<Object, String> getValue,
            NodeRenderAsType renderAs = NodeRenderAsType.Element)
            : this(name)
        { GetValue = getValue; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class with the specified scope.
        /// </summary>
        /// <remarks>The <paramref name="scope"/> parameter is converted to its corresponding name.</remarks>
        /// <param name="scope">The scope of the node, represented as a <see cref="ScopeType"/>.</param>
        public XElementBuilder(ScopeType scope) : this(ScopeEnumeration.Cast(scope).Name)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class using the specified property.
        /// </summary>
        /// <remarks>
        /// The constructor sets up a delegate to retrieve the value of the specified property from an object instance.</remarks>
        /// <param name="property">The <see cref="PropertyInfo"/> representing the property to associate with this node.</param>
        public XElementBuilder(PropertyInfo property) : this(property.Name)
        { GetValue = (value) => GetValueDelegate(property, value); }

        /// <summary>
        /// Creates a collection of <see cref="XElementBuilder"/> objects based on the properties of the specified type.
        /// </summary>
        /// <remarks>
        /// Each <see cref="XElementBuilder"/> in the returned collection represents a property of the specified type,
        /// with its value set to <see cref="NodeRenderAsType.ElementText"/>.
        /// </remarks>
        /// <param name="value">The <see cref="Type"/> whose properties will be used to create the <see cref="XElementBuilder"/> objects.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing <see cref="XElementBuilder"/> objects,  where each node corresponds to a property of the specified type.</returns>
        public static IEnumerable<XElementBuilder> Create(Type value)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();

            foreach (PropertyInfo property in value.GetProperties().ToList())
            {
                result.Add(
                new XElementBuilder(property)
                { NodeValueAs = NodeRenderAsType.ElementText });
            }

            return result;
        }

        /// <summary>
        /// Converts the specified object to its string representation.
        /// </summary>
        /// <param name="value">The object to convert. Can be <see langword="null"/>.</param>
        /// <returns>A string representation of the specified object, or <see langword="null"/> if <paramref name="value"/> is <see langword="null"/>.</returns>
        public virtual String? GetValueDelegate(Object value)
        {
            if (value is null) { return null; }
            else { return value.ToString(); }
        }

        /// <summary>
        /// Retrieves the name associated with the specified <see cref="ScopeType"/> value.
        /// </summary>
        /// <param name="value">The <see cref="ScopeType"/> value for which to retrieve the name.</param>
        /// <returns>A <see cref="string"/> representing the name of the specified <see cref="ScopeType"/> value, or <see langword="null"/> if the value does not have an associated name.</returns>
        public virtual String? GetValueDelegate(ScopeType value)
        { return ScopeEnumeration.Cast(value).Name; }

        /// <summary>
        /// Retrieves a string representation of the value of the specified property from the given object.
        /// </summary>
        /// <remarks>
        /// This method dynamically resolves the property's value and attempts to convert it to a
        /// string. If the property's value is an object, the method recursively processes it.
        /// </remarks>
        /// <param name="property">The <see cref="PropertyInfo"/> representing the property to retrieve the value from.</param>
        /// <param name="value">The object instance containing the property.</param>
        /// <returns>A string representation of the property's value, or <see langword="null"/> if the property's value is <see langword="null"/>  or cannot be converted to a string.</returns>
        public virtual String? GetValueDelegate(PropertyInfo property, Object value)
        {
            if (property.GetValue(value) is Object objectValue)
            { return GetValueDelegate((dynamic)objectValue); ; }
            else { return null; }
        }

        /// <summary>
        /// Retrieves the full path of the member associated with the specified <see cref="PathIndex"/>.
        /// </summary>
        /// <param name="value">The <see cref="PathIndex"/> instance containing the member information.</param>
        /// <returns>The full path of the member as a <see cref="string"/>, or <see langword="null"/> if the path is not available.</returns>
        public virtual String? GetValueDelegate(PathIndex value)
        { return value.MemberFullPath; }

        /// <summary>
        /// Builds an XML object based on the specified value and the current configuration of the node.
        /// </summary>
        /// <remarks>
        /// The type of XML object created depends on the value of the <c>NodeValueAs</c>.
        /// </remarks>
        /// <param name="value">The input value used to determine the content of the XML object.</param>
        /// <returns>An <see cref="XObject"/> representing the constructed XML element, attribute, or other node type,  or <see langword="null"/> if the node cannot be created due to invalid configuration or input.</returns>
        public virtual XObject? Build(Object value)
        {
            String? nodeValue = GetValue(value);

            if (String.IsNullOrEmpty(NodeName) || NodeValueAs is NodeRenderAsType.none)
            { return null; }

            switch (NodeValueAs)
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
                        fragementEx.Data.Add(nameof(NodeValueAs), NodeValueAs.ToString());
                        throw;
                    }
                case NodeRenderAsType.AttributeText:
                    if (String.IsNullOrWhiteSpace(nodeValue)) { return null; }
                    return new XAttribute(NodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(NodeRenderAsType)));
                    ex.Data.Add(nameof(NodeName), NodeName);
                    ex.Data.Add(nameof(NodeValueAs), NodeValueAs.ToString());
                    throw ex;
            }
        }


        /// <inheritdoc/>
        public override String ToString()
        { return NodeName; }
    }

    /// <summary>
    /// Provides extension methods for working with <see cref="XElementBuilder"/> objects, enabling operations such as
    /// building, appending, and retrieving XML elements based on custom node definitions.
    /// </summary>
    /// <remarks>
    /// This static class contains methods designed to simplify the manipulation of XML structures
    /// using <see cref="XElementBuilder"/> objects. These methods support scenarios such as constructing XML elements from
    /// data models, updating node definitions, and retrieving specific nodes by property name.
    /// </remarks>
    public static class XElementBuilderExtension
    {
        /// <summary>
        /// Appends the specified nodes to the given XML element after building them with the provided value.
        /// </summary>
        /// <remarks>
        /// Each node in the <paramref name="nodes"/> collection is processed using its
        /// <c>Build</c> method, which uses the provided <paramref name="value"/>.
        /// The resulting elements are then added to the <paramref name="root"/> element.</remarks>
        /// <param name="root">The root <see cref="XElement"/> to which the nodes will be appended.</param>
        /// <param name="value">An object used to build the nodes before appending them to the root element.</param>
        /// <param name="nodes">A collection of <see cref="XElementBuilder"/> objects to be built and appended to the root element.</param>
        public static void Append(this XElement root, Object value, IEnumerable<XElementBuilder> nodes)
        {
            foreach (XElementBuilder item in nodes)
            { root.Add(item.Build(value)); }
        }

        /// <summary>
        /// Builds an XML element tree based on the specified nodes and scope type.
        /// </summary>
        /// <param name="nodes">A collection of <see cref="XElementBuilder"/> objects representing the nodes to include in the XML tree.</param>
        /// <param name="value">An object implementing <see cref="IScopeType"/> that defines the scope and root element of the XML tree.</param>
        /// <returns>An <see cref="XElement"/> representing the root of the constructed XML tree. The root element's name is determined by the scope of the provided <paramref name="value"/>.</returns>
        public static XElement Build(this IEnumerable<XElementBuilder> nodes, IScopeType value)
        {
            XElement root = new XElement(ScopeEnumeration.Cast(value.Scope).Name);
            Append(root, value, nodes);

            return root;
        }

        /// <summary>
        /// Builds a collection of <see cref="XElement"/> objects from the specified nodes and scope values.
        /// </summary>
        /// <remarks>
        /// This method iterates over the provided <paramref name="values"/> and uses the
        /// <paramref name="nodes"/> to build an <see cref="XElement"/> for each value.
        /// The resulting collection preserves the order of the input values.</remarks>
        /// <param name="nodes">The collection of <see cref="XElementBuilder"/> objects used to construct the elements.</param>
        /// <param name="values">The collection of <see cref="IScopeType"/> values that define the scope for building the elements.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="XElement"/> objects, where each element is constructed based on the corresponding scope value.</returns>
        public static IEnumerable<XElement> Build(this IEnumerable<XElementBuilder> nodes, IEnumerable<IScopeType> values)
        {
            List<XElement> result = new List<XElement>();

            foreach (var value in values)
            { result.Add(nodes.Build(value)); }

            return result;
        }

        /// <summary>
        /// Updates the properties of a collection of <see cref="XElementBuilder"/> objects based on matching templates.
        /// </summary>
        /// <param name="nodes">The collection of <see cref="XElementBuilder"/> objects to update.</param>
        /// <param name="templates">A collection of <see cref="IScriptingNodeValue"/> objects used to update the nodes. Each template is matched to a node by the <see cref="XElementBuilder.PropertyName"/> property.</param>
        [Obsolete("replace", true)]
        public static void Set(this IEnumerable<XElementBuilder> nodes, IEnumerable<IScriptingNodeValue> templates)
        {
            foreach (XElementBuilder node in nodes)
            {
                if (templates.FirstOrDefault(w => node.PropertyName.Equals(w.PropertyName)) is IScriptingNodeValue template)
                {
                    node.NodeName = template.NodeName ?? node.NodeName;
                    node.NodeValueAs = template.NodeRenderAs;
                }
            }
        }

        /// <summary>
        /// Attempts to retrieve the first <see cref="XElementBuilder"/> from the collection that matches the specified property name.
        /// </summary>
        /// <param name="nodes">The collection of <see cref="XElementBuilder"/> objects to search.</param>
        /// <param name="propertyName">The name of the property to match against the <see cref="XElementBuilder.PropertyName"/>.</param>
        /// <param name="result">When this method returns, contains the first <see cref="XElementBuilder"/> that matches the specified property name, if found; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if a matching <see cref="XElementBuilder"/> is found; otherwise, <see langword="false"/>.</returns>
        public static Boolean TryGetValue(this IEnumerable<XElementBuilder> nodes, String propertyName, [NotNullWhen(true)] out XElementBuilder? result)
        {
            if (nodes.FirstOrDefault(w => w.PropertyName.Equals(propertyName)) is XElementBuilder value)
            { result = value; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Retrieves the <see cref="XElementBuilder"/> associated with the specified property name from the collection.
        /// </summary>
        /// <remarks>
        /// This method attempts to locate the <see cref="XElementBuilder"/> corresponding to the given property name.
        /// If the property name is not found, an <see cref="IndexOutOfRangeException"/> is thrown with additional context about the missing property.
        /// </remarks>
        /// <param name="nodes">The collection of <see cref="XElementBuilder"/> objects to search.</param>
        /// <param name="propertyName">The name of the property to locate in the collection. Cannot be <see langword="null"/> or empty.</param>
        /// <returns>The <see cref="XElementBuilder"/> associated with the specified property name.</returns>
        public static XElementBuilder GetValue(this IEnumerable<XElementBuilder> nodes, String propertyName)
        {
            if (nodes.TryGetValue(propertyName, out XElementBuilder? result))
            { return result; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(propertyName), propertyName);
                throw ex;
            }
        }
    }
}
