using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public interface IScriptingNodeValue : IScriptingNodeItem, IScriptingTemplateIndex, IScriptingNodeIndex, IScriptingNodeIndexName
    { }

    /// <inheritdoc/>
    [Obsolete("replace", true)]
    public class ScriptingNodeValue : ScriptingNodeItem, IScriptingNodeValue
    {
        /// <inheritdoc/>
        public ScriptingNodeValue() : base() { }

        /// <inheritdoc cref="ScriptingNodeItem(IScriptingTemplateKey)"/>
        public ScriptingNodeValue(IScriptingTemplateIndex source) : base(source)
        { }

        [Obsolete("Replace with XELementNode")]
        internal XObject? BuildXObject(Object? value)
        {
            String? nodeName = NodeName ?? PropertyName;
            if (nodeName is null) { return null; }

            if (value is null) { return null; }
            String? nodeValue = value.ToString();
            if (String.IsNullOrWhiteSpace(nodeValue))
            { return null; }

            switch (NodeRenderAs)
            {
                case NodeRenderAsType.none:
                    return null;
                case NodeRenderAsType.ElementText:
                    return new XElement(nodeName, value);
                case NodeRenderAsType.ElementCData:
                    return new XElement(nodeName, new XCData(nodeValue));
                case NodeRenderAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue))
                        { return new XElement(nodeName, XElement.Parse(nodeValue)); }
                        else { return null; }
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(NodeRenderAs), NodeRenderAs.ToString());
                        fragementEx.Data.Add(nameof(PropertyName), PropertyName);
                        throw;
                    }
                case NodeRenderAsType.AttributeText:
                    return new XAttribute(nodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException("Unknown NodeValueAsType");
                    ex.Data.Add(nameof(NodeRenderAs), NodeRenderAs.ToString());
                    ex.Data.Add(nameof(PropertyName), PropertyName);
                    throw ex;
            }
        }
    }
}
