using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for Node Render As type.
    /// </summary>
    [Obsolete("Replace with XmlNodeType", true)]
    class NodeRenderAsEnumeration : Enumeration<NodeRenderAsType, NodeRenderAsEnumeration>
    {
        /// <summary>
        /// Internal Constructor
        /// </summary>
        /// <remarks>Prevents automatic construction of parameterless constructor.</remarks>
        NodeRenderAsEnumeration(NodeRenderAsType value, String name) : base(value, name) { }

        static NodeRenderAsEnumeration()
        {
            List<NodeRenderAsEnumeration> data = new List<NodeRenderAsEnumeration>()
            {
                new NodeRenderAsEnumeration(NodeRenderAsType.None,           String.Empty){ DisplayName = "not defined" },
                new NodeRenderAsEnumeration(NodeRenderAsType.Element,        "Element"),
                new NodeRenderAsEnumeration(NodeRenderAsType.ElementText,    "Element.Text"),
                new NodeRenderAsEnumeration(NodeRenderAsType.ElementCData,   "Element.CData"),
                new NodeRenderAsEnumeration(NodeRenderAsType.ElementXML,     "Element.XML"),
                new NodeRenderAsEnumeration(NodeRenderAsType.AttributeText,  "Attribute.Text"),
            };

            BuildDictionary(data);
        }
    }

}
