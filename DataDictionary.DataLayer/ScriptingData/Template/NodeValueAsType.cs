using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// List of supported rendering methods for Node Values
    /// </summary>
    public enum NodeValueAsType
    {
        /// <summary>
        /// Not Defined or do not render
        /// </summary>
        none,

        /// <summary>
        /// Render value as an Element Text
        /// </summary>
        ElementText,

        /// <summary>
        /// Render value as an Element CData
        /// </summary>
        ElementCData,

        /// <summary>
        /// Render value as an Element with the data as a child XML node.
        /// </summary>
        ElementXML,

        /// <summary>
        /// Render value as an Attribute Text. Attribute Name = Data
        /// </summary>
        Attribute
    }

    /// <summary>
    /// Interface for Scripting NodeValueAs Key.
    /// </summary>
    public interface INodeValueAsType : IKey
    {
        /// <summary>
        /// How the Value of the Node is to be rendered.
        /// </summary>
        NodeValueAsType NodeValueAs { get; }
    }
}
