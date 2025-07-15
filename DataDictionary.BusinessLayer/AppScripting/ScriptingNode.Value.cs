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
    public interface IScriptingNodeValue : IScriptingNodeItem, IScriptingTemplateIndex, IScriptingNodeIndex, IScriptingNodeIndexName
    { }

    /// <inheritdoc/>
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

            switch (NodeValueAs)
            {
                case TemplateNodeValueAsType.none:
                    return null;
                case TemplateNodeValueAsType.ElementText:
                    return new XElement(nodeName, value);
                case TemplateNodeValueAsType.ElementCData:
                    return new XElement(nodeName, new XCData(nodeValue));
                case TemplateNodeValueAsType.ElementXML:
                    try
                    {
                        if (String.IsNullOrWhiteSpace(nodeValue))
                        { return new XElement(nodeName, XElement.Parse(nodeValue)); }
                        else { return null; }
                    }
                    catch (Exception fragementEx)
                    {
                        fragementEx.Data.Add(nameof(NodeValueAs), NodeValueAs.ToString());
                        fragementEx.Data.Add(nameof(PropertyName), PropertyName);
                        throw;
                    }
                case TemplateNodeValueAsType.Attribute:
                    return new XAttribute(nodeName, nodeValue);
                default:
                    Exception ex = new InvalidOperationException("Unknown NodeValueAsType");
                    ex.Data.Add(nameof(NodeValueAs), NodeValueAs.ToString());
                    ex.Data.Add(nameof(PropertyName), PropertyName);
                    throw ex;
            }
        }
    }
}
