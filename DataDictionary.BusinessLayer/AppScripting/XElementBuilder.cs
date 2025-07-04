using DataDictionary.Resource.Enumerations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Base class for building XML nodes. Provides functionality to define node names, values, and rendering behavior.
    /// </summary>
    public class XmlBuilderBase
    {
        /// <summary>
        /// Gets or sets a delegate that returns the text used as the XName for the XAttribute/XElement.
        /// </summary>
        public Func<String> GetNodeName { get; set; } = () => String.Empty;

        /// <summary>
        /// Gets or sets a delegate that retrieves the value of a XAttribute/XElement node as a string.
        /// </summary>
        /// <remarks>The delegate can be used to dynamically fetch the value of a node. Ensure
        /// the function handles cases where the node value might be null.</remarks>
        public Func<String?> GetNodeValue { get; set; } = () => null;

        /// <summary>
        /// Gets or sets the rendering behavior for the node.
        /// </summary>
        public TemplateNodeValueAsType RenderAs { get; set; } = TemplateNodeValueAsType.none;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderBase"/> class with a specified node name and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XmlBuilderBase(
            ScopeType nodeName,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.Element)
            : base()
        {
            GetNodeName = () => ScopeEnumeration.Cast(nodeName).Name;
            RenderAs = renderAs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderBase"/> class with a specified node name and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XmlBuilderBase(
            String nodeName,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.Element)
            : base()
        {
            GetNodeName = () => nodeName;
            RenderAs = TemplateNodeValueAsType.Element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderBase"/> class with a specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="nodeValue">The value of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XmlBuilderBase(
            ScopeType nodeName,
            String? nodeValue,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            : this(nodeName, renderAs)
        { GetNodeValue = () => nodeValue; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderBase"/> class with a specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="nodeValue">The value of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XmlBuilderBase(
            ScopeType nodeName,
            Object? nodeValue,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            : this(nodeName, renderAs)
        { GetNodeValue = GetValue((dynamic?)nodeValue); }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderBase"/> class with a specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="nodeValue">The value of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XmlBuilderBase(
            String nodeName,
            String? nodeValue,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            : this(nodeName, renderAs)
        { GetNodeValue = () => nodeValue; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBuilderBase"/> class with a specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="nodeValue">The value of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XmlBuilderBase(
            String nodeName,
            Object? nodeValue,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            : this(nodeName, renderAs)
        { GetNodeValue = GetValue((dynamic?)nodeValue); }

        /// <summary>
        /// Retrieves the value of the node as a string.
        /// </summary>
        /// <param name="value">The value to retrieve.</param>
        /// <returns>A delegate that returns the value as a string.</returns>
        Func<String?> GetValue(Object value)
        {
            if (value is null) { return () => null; }
            return value.ToString;
        }

        /// <summary>
        /// Retrieves the value of the node as a string.
        /// </summary>
        /// <param name="value">The value to retrieve.</param>
        /// <returns>A delegate that returns the value as a string.</returns>
        Func<String?> GetValue(ScopeType value)
        { return () => ScopeEnumeration.Cast(value).Name; }

        /// <inheritdoc/>
        public override String ToString()
        { return GetNodeName(); }
    }


    /// <summary>
    /// Represents a builder for creating XML elements (<see cref="XElement"/>).
    /// </summary>
    /// <remarks>
    /// This class extends <see cref="XmlBuilderBase"/> and provides functionality to manage child elements
    /// and build a complete <see cref="XElement"/> structure.
    /// </remarks>
    public class XElementBuilder : XmlBuilderBase
    {
        /// <summary>
        /// Gets the list of child <see cref="XElementBuilder"/> objects.
        /// </summary>
        public List<XElementBuilder> Children { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class with a specified node name.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        public XElementBuilder(
            ScopeType nodeName)
            : base(nodeName)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class with a specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="nodeValue">The value of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XElementBuilder(
            String nodeName,
            String? nodeValue,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            : base(nodeName, nodeValue, renderAs)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class with a specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="nodeValue">The value of the node.</param>
        /// <param name="renderAs">The rendering behavior for the node.</param>
        public XElementBuilder(
            String nodeName,
            Object? nodeValue,
            TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            : base(nodeName, nodeValue, renderAs)
        { }

        /// <summary>
        /// Creates a collection of <see cref="XElementBuilder"/> objects from the properties of the specified object.
        /// </summary>
        /// <param name="value">The object whose properties will be used to create the builders.</param>
        /// <returns>A collection of <see cref="XElementBuilder"/> objects.</returns>
        public static IEnumerable<XElementBuilder> Create(Object value)
        {
            List<XElementBuilder> result = new List<XElementBuilder>();

            foreach (PropertyInfo property in value.GetType().GetProperties().ToList())
            {
                if (property.CanRead && property.GetIndexParameters().Length == 0)
                { result.Add(new XElementBuilder(property.Name, property.GetValue(value))); }
            }

            return result;
        }

        /// <summary>
        /// Builds the <see cref="XElement"/> represented by this builder and its children.
        /// </summary>
        /// <returns>The constructed <see cref="XElement"/>.</returns>
        public XElement Build()
        {
            XElement result;
            XObject? value = BuildXObject(this);

            if (value is XElement elementValue)
            { result = elementValue; }
            else if (value is XObject objectValue)
            {
                result = new XElement(GetNodeName());
                result.Add(objectValue);
            }
            else { result = new XElement(GetNodeName()); }

            return result;
        }

        /// <summary>
        /// Builds an <see cref="XObject"/> (either an <see cref="XElement"/> or <see cref="XAttribute"/>) based on the current builder's settings.
        /// </summary>
        /// <param name="setting">The builder settings to use for constructing the object.</param>
        /// <returns>The constructed <see cref="XObject"/>, or <c>null</c> if the object cannot be constructed.</returns>
        private XObject? BuildXObject(XElementBuilder setting)
        {
            String name = setting.GetNodeName();
            String? value = setting.GetNodeValue();
            XElement element;

            if (String.IsNullOrWhiteSpace(name))
            { setting.RenderAs = TemplateNodeValueAsType.none; }

            switch (setting.RenderAs)
            {
                case TemplateNodeValueAsType.none:
                    element = new XElement("not.rendered"); break;
                case TemplateNodeValueAsType.Element:
                    element = new XElement(name); break;
                case TemplateNodeValueAsType.ElementText:
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    element = new XElement(name, value); break;
                case TemplateNodeValueAsType.ElementCData:
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    element = new XElement(name, new XCData(value)); break;
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(value)) { return null; }
                        element = new XElement(setting.GetNodeName(), XElement.Parse(value)); break;
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(setting.GetNodeName), setting.GetNodeName());
                        fragementEx.Data.Add(nameof(setting.GetNodeValue), value);
                        fragementEx.Data.Add(nameof(setting.RenderAs), setting.RenderAs.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    if (String.IsNullOrWhiteSpace(value)) { return null; }
                    return new XAttribute(name, value);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(TemplateNodeValueAsType)));
                    ex.Data.Add(nameof(setting.GetNodeName), setting.GetNodeName());
                    ex.Data.Add(nameof(setting.RenderAs), setting.RenderAs.ToString());
                    throw ex;
            }

            // Add the Children, if any
            foreach (XElementBuilder item in setting.Children)
            {
                var values = BuildXObject(item);
                if (values is XElement elementValue && item.RenderAs is TemplateNodeValueAsType.none)
                { element.Add(elementValue.Nodes()); }
                else { element.Add(values); }
            }

            return element;
        }
    }

    /// <summary>
    /// Provides extension methods for working with a list of <see cref="XElementBuilder"/> objects.
    /// </summary>
    public static class XElementExtension
    {
        /// <summary>
        /// Adds a new <see cref="XElementBuilder"/> to the list with the specified node name, value, and rendering behavior.
        /// </summary>
        /// <param name="values">The list of <see cref="XElementBuilder"/> objects.</param>
        /// <param name="nodeName">The name of the node to add.</param>
        /// <param name="nodeValue">The value of the node to add. Defaults to <c>null</c>.</param>
        /// <param name="renderAs">The rendering behavior for the node. Defaults to <see cref="TemplateNodeValueAsType.ElementText"/>.</param>
        public static void Add(this IList<XElementBuilder> values, String nodeName, Object? nodeValue = null, TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
        { values.Add(new XElementBuilder(nodeName, nodeValue, renderAs)); }

        /// <summary>
        /// Removes all <see cref="XElementBuilder"/> objects with the specified node name from the list.
        /// </summary>
        /// <param name="values">The list of <see cref="XElementBuilder"/> objects.</param>
        /// <param name="nodeName">The name of the node to remove.</param>
        public static void Remove(this IList<XElementBuilder> values, String nodeName)
        {
            while (values.FirstOrDefault(w => nodeName.Equals(w.GetNodeName())) is XElementBuilder child)
            { values.Remove(child); }
        }

        /// <summary>
        /// Attempts to retrieve a <see cref="XmlBuilderBase"/> object with the specified node name from the list.
        /// </summary>
        /// <param name="values">The list of <see cref="XElementBuilder"/> objects.</param>
        /// <param name="nodeName">The name of the node to retrieve.</param>
        /// <param name="result">When this method returns, contains the <see cref="XmlBuilderBase"/> object if found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if a node with the specified name was found; otherwise, <c>false</c>.</returns>
        public static Boolean TryGet(this IList<XElementBuilder> values, String nodeName, [NotNullWhen(true)] out XmlBuilderBase? result)
        {
            if (values.FirstOrDefault(w => nodeName.Equals(w.GetNodeName())) is XmlBuilderBase value)
            { result = value; return true; }
            else { result = null; return false; }
        }

        /// <summary>
        /// Retrieves a <see cref="XmlBuilderBase"/> object with the specified node name from the list.
        /// </summary>
        /// <param name="values">The list of <see cref="XElementBuilder"/> objects.</param>
        /// <param name="nodeName">The name of the node to retrieve.</param>
        /// <returns>The <see cref="XmlBuilderBase"/> object with the specified name.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if no node with the specified name is found.</exception>
        public static XmlBuilderBase Get(this IList<XElementBuilder> values, String nodeName)
        {
            if (values.FirstOrDefault(w => nodeName.Equals(w.GetNodeName())) is XmlBuilderBase value)
            { return value; }
            else
            {
                Exception ex = new IndexOutOfRangeException();
                ex.Data.Add(nameof(nodeName), nodeName);
                throw ex;
            }
        }
    }
}
