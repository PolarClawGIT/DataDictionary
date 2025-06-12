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
    public class XElementBuilder
    {
        /// <summary>
        /// Sets the Rendering settings for each of the properties of the data source.
        /// </summary>
        public class RenderSetting
        {
            //TODO: This is how I will inject the setting from the Template.

            /// <summary>
            /// Gets or sets the name of the rendered node.
            /// </summary>
            /// <remarks>Default is the name of the property.</remarks>
            public String NodeName { get; set; } = String.Empty;

            /// <summary>
            /// Gets or sets the rendering behavior for the node.
            /// </summary>
            public TemplateNodeValueAsType NodeRender { get; set; }

            /// <summary>
            /// Gets or sets the child object (XElement or XAttribute) to be added to the result.
            /// </summary>
            public XObject? ChildObject { get; set; }
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
        public Dictionary<String, RenderSetting> Settings { get { return renderSettings.ToDictionary(d => d.Key.Name, e => e.Value); } }
        Dictionary<PropertyInfo, RenderSetting> renderSettings = new Dictionary<PropertyInfo, RenderSetting>();

        /// <summary>
        /// Initializes a new instance of the <see cref="XElementBuilder"/> class,
        /// configuring property rendering settings for the specified data source.
        /// </summary>
        /// <remarks>The constructor inspects the properties of the provided <paramref name="source"/>
        /// object and initializes rendering settings for each property.
        /// Each property is assigned a default rendering configuration, including its node name and rendering type.</remarks>
        /// <param name="source">The object to be used as the data source. This object provides the properties that will be rendered as XML elements.</param>
        public XElementBuilder(Object source)
        {
            dataSource = source;
            ObjectType = source.GetType();

            foreach (PropertyInfo item in ObjectType.GetProperties())
            {
                renderSettings.Add(item, new RenderSetting()
                {
                    NodeName = item.Name,
                    NodeRender = TemplateNodeValueAsType.ElementText
                });
            }
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

            foreach (PropertyInfo item in renderSettings.Keys)
            {
                if(Settings.TryGetValue(item.Name, out RenderSetting? setting))
                {
                    if(BuildXObject(setting, item.GetValue(dataSource)) is XObject renderValue)
                    {
                        if (setting.ChildObject is XObject child
                            && renderValue is XElement parent)
                        { parent.Add(child); }

                        result.Add(renderValue);
                    }
                }
                else if (BuildXObject(item.GetValue(dataSource)) is XObject objectValue)
                { result.Add(objectValue); }
            }

            return result;
        }

        XObject? BuildXObject(RenderSetting setting, Object? nodeValue)
        {
            if (nodeValue is null) { return null; }

            String? value = nodeValue.ToString();
            if (String.IsNullOrWhiteSpace(value))
            { return null; }

            switch (setting.NodeRender)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.ElementText:
                    return new XElement(setting.NodeName, value);
                case TemplateNodeValueAsType.ElementCData:
                    return new XElement(setting.NodeName, new XCData(value));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(value))
                        { return new XElement(setting.NodeName, XElement.Parse(value)); }
                        else { return null; }
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(setting.NodeName), setting.NodeName);
                        fragementEx.Data.Add(nameof(nodeValue), nodeValue.ToString());
                        fragementEx.Data.Add(nameof(setting.NodeRender), setting.NodeRender.ToString());
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    return new XAttribute(setting.NodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException(String.Format("Unknown {0}", nameof(TemplateNodeValueAsType)));
                    ex.Data.Add(nameof(setting.NodeRender), setting.NodeRender.ToString());
                    throw ex;
            }
        }


        XObject? BuildXObject(Object? nodeValue)
        {
            if (nodeValue is null) { return null; }

            return BuildXObject(
                new RenderSetting()
                {
                    NodeName = nodeValue.GetType().Name,
                    NodeRender = TemplateNodeValueAsType.ElementText
                },
                nodeValue);
        }
    }
}
