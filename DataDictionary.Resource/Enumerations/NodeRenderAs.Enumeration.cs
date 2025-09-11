using System.Diagnostics.CodeAnalysis;
namespace DataDictionary.Resource.Enumerations
{
    /// <summary>
    /// Enumeration support class for Node Render As type.
    /// </summary>
    public class NodeRenderAsEnumeration : Enumeration<NodeRenderAsType, NodeRenderAsEnumeration>
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
                new NodeRenderAsEnumeration(NodeRenderAsType.none,           String.Empty){ DisplayName = "not defined" },
                new NodeRenderAsEnumeration(NodeRenderAsType.Element,        "Element"),
                new NodeRenderAsEnumeration(NodeRenderAsType.ElementText,    "Element.Text"),
                new NodeRenderAsEnumeration(NodeRenderAsType.ElementCData,   "Element.CData"),
                new NodeRenderAsEnumeration(NodeRenderAsType.ElementXML,     "Element.XML"),
                new NodeRenderAsEnumeration(NodeRenderAsType.AttributeText,  "Attribute.Text"),
            };

            BuildDictionary(data);
        }
    }

    /// <summary>
    /// Extensions on NodeRenderAs Enum. 
    /// </summary>
    public static class NodeRenderAsExtension
    {
        /// <summary>
        /// Gets the Details for the NodeRenderAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NodeRenderAsEnumeration GetEnumeration(this NodeRenderAsType value)
        { return NodeRenderAsEnumeration.Cast(value); }

        /// <summary>
        /// Try to parse the String into a NodeRenderAsType enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Boolean TryParse(this String? value, out NodeRenderAsType result)
        {
            if (NodeRenderAsEnumeration.TryParse(value, null, out NodeRenderAsEnumeration? enumeration))
            { result = enumeration.Value; return true; }
            else { result = NodeRenderAsType.none; return false; }
        }

        // No way to put a Method on an ENum type.
        // Instead, something like this will be needed.
        // return Enum.GetValues<NodeRenderAsType>().ToDictionary(k => k, v => Enum.GetName(v)); 

    }
}
