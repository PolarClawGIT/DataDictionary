using DataDictionary.Main.Properties;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.Main.Enumerations
{
    partial class NodeRenderAsIcon
    {
        static Dictionary<NodeRenderAsType, Icon> nodeRenderIconMap = new Dictionary<NodeRenderAsType, Icon>() 
        {
            { NodeRenderAsType.None,            Resources.Icon_XMLElementNone },
            { NodeRenderAsType.Element,         Resources.Icon_XMLElement },
            { NodeRenderAsType.ElementText,     Resources.Icon_XMLElementText },
            { NodeRenderAsType.ElementCData,    Resources.Icon_XMLCDataTag },
            { NodeRenderAsType.ElementXML,      Resources.Icon_XMLDescendant },
            { NodeRenderAsType.AttributeText,   Resources.Icon_XMLAttribute },
        };
    }
}
