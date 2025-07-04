using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <summary>
    /// Builder class (see Builder pattern) that creates an XElement out of a object data source.
    /// </summary>
    /// <remarks>POC: Revised way of building the XML used by the scripting engine.</remarks>
    public class XElementBuilder_V1
    {
        /// <summary>
        /// Sets the Rendering settings for each of the properties of the data source.
        /// </summary>
        public class RenderSetting
        {
            //TODO: This is how I will inject the setting from the Template.

            /// <summary>
            /// Gets or sets a delegate that returns the text used as the XName for the XAttribute/XElement.
            /// </summary>
            public Func<String> GetNodeName { get; set; } = () => String.Empty;

            /// <summary>
            /// Gets or sets a delegate that retrieves the value of a XAttribute/XElement node as a string.
            /// </summary>
            /// <remarks>The delegate can be used to dynamically fetch the value of a node.  Ensure
            /// the function handles cases where the node value might be null.</remarks>
            public Func<String?> GetNodeValue { get; set; } = () => null;

            /// <summary>
            /// Gets or sets the rendering behavior for the node.
            /// </summary>
            public TemplateNodeValueAsType NodeRender { get; set; }

            /// <summary>
            /// Gets or sets the child object (XElement or XAttribute) to be added to the result.
            /// </summary>
            public XObject? ChildObject { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="RenderSetting"/> class.
            /// </summary>
            public RenderSetting() : base() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="RenderSetting"/> class with the specified parameters.
            /// </summary>
            /// <param name="name">The name of the node to be rendered.</param>
            /// <param name="value">The value of the node to be rendered. Can be null.</param>
            /// <param name="renderAs">The rendering type for the node. Defaults to <see cref="TemplateNodeValueAsType.ElementText"/>.</param>
            public RenderSetting(
                String name,
                String? value,
                TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText)
            {
                GetNodeName = () => name;
                GetNodeValue = () => value;
                NodeRender = renderAs;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RenderSetting"/> class with the specified property and source object.
            /// </summary>
            /// <param name="property">The property information used to configure the rendering settings.</param>
            /// <param name="source">The source object from which the property value is retrieved.</param>
            /// <param name="renderAs">The rendering type for the node. Defaults to <see cref="TemplateNodeValueAsType.ElementText"/>.</param>
            internal RenderSetting(
                PropertyInfo property,
                Object source,
                TemplateNodeValueAsType renderAs = TemplateNodeValueAsType.ElementText) : this()
            {
                dynamic? propValue = property.GetValue(source);
                GetNodeName = () => property.Name;
                GetNodeValue = GetValue(propValue);
                NodeRender = renderAs;
            }

            Func<String?> GetValue(Object value)
            {
                if (value is null) { return () => null; }
                return value.ToString;
            }

            Func<String?> GetValue(ScopeType value)
            { return () => ScopeEnumeration.Cast(value).Name; }

        }

        // refrence to the Data Source of the item.
        Object dataSource;

        /// <summary>
        /// Gets the Type of the Object associated with the data source.
        /// </summary>
        public Type ObjectType { get; }

        /// <summary>
        /// List of Properties of the data source and the rendering settings.
        /// </summary>
        public IDictionary<String, RenderSetting> Settings { get; } = new Dictionary<String, RenderSetting>();

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder_V1"/> class,
        /// configuring property rendering settings for the specified data source.
        /// </summary>
        /// <remarks>The constructor inspects the properties of the provided <paramref name="source"/>
        /// object and initializes rendering settings for each property.
        /// Each property is assigned a default rendering configuration, including its node name and rendering type.</remarks>
        /// <param name="source">The object to be used as the data source. This object provides the properties that will be rendered as XML elements.</param>
        public XElementBuilder_V1(Object source)
        {
            dataSource = source;
            ObjectType = source.GetType();

            foreach (PropertyInfo item in ObjectType.GetProperties())
            { Settings.Add(item.Name, new RenderSetting(item, source)); }
        }

        /// <summary>
        /// Generates an <see cref="XElement"/> representation of the current object and its properties.
        /// </summary>
        /// <remarks>
        /// This method creates an XML element based on the object's type and scope, and includes
        /// child elements for each property according to the specified rendering options.
        /// The resulting XML structure reflects the object's data and configuration.
        /// </remarks>
        /// <returns>
        /// An <see cref="XElement"/> representing the object and its properties.
        /// The element may contain nested child elements based on the rendering options provided.
        /// </returns>
        public XElement Build()
        {
            XElement result;

            if (dataSource is IScopeType dataValue)
            { result = new XElement(ScopeEnumeration.Cast(dataValue.Scope).Name); }
            else if (ObjectType.FullName is String)
            { result = new XElement(ObjectType.FullName); }
            else { result = new XElement(ObjectType.Name); }

            foreach (RenderSetting item in Settings.Values)
            {
                if (BuildXObject(item) is XObject value)
                {
                    if (item.ChildObject is XObject child
                        && value is XElement parent)
                    { parent.Add(child); }

                    result.Add(value);
                }
            }

            return result;
        }

        XObject? BuildXObject(RenderSetting setting)
        {
            String name = setting.GetNodeName();
            String? value = setting.GetNodeValue();
            if (String.IsNullOrWhiteSpace(value))
            { return null; }

            switch (setting.NodeRender)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.ElementText:
                    return new XElement(name, value);
                case TemplateNodeValueAsType.ElementCData:
                    return new XElement(name, new XCData(value));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(value))
                        { return new XElement(setting.GetNodeName(), XElement.Parse(value)); }
                        else { return null; }
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(setting.GetNodeName), setting.GetNodeName());
                        fragementEx.Data.Add(nameof(setting.GetNodeValue), value);
                        fragementEx.Data.Add(nameof(setting.NodeRender), setting.NodeRender.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    return new XAttribute(name, value);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(TemplateNodeValueAsType)));
                    ex.Data.Add(nameof(setting.GetNodeName), setting.GetNodeName());
                    ex.Data.Add(nameof(setting.NodeRender), setting.NodeRender.ToString());
                    throw ex;
            }
        }
    }
}
