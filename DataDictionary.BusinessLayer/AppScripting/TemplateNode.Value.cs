using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeValue : ITemplateNodeItem,
        ITemplateIndex, ITemplateNodeIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TemplateNodeValue : TemplateNodeItem, ITemplateNodeValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope
        {
            get
            {
                switch (RenderValueAs)
                {
                    case NodeRenderAsType.none: return ScopeType.ScriptingTemplateNode;
                    case NodeRenderAsType.Element: return ScopeType.ScriptingTemplateElement;
                    case NodeRenderAsType.ElementText: return ScopeType.ScriptingTemplateElement;
                    case NodeRenderAsType.ElementCData: return ScopeType.ScriptingTemplateElement;
                    case NodeRenderAsType.ElementXML: return ScopeType.ScriptingTemplateElement;
                    case NodeRenderAsType.AttributeText: return ScopeType.ScriptingTemplateAttribute;
                    default: return ScopeType.ScriptingTemplateNode;
                }
            }
        }

        /// <inheritdoc/>
        public TemplateNodeValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateNodeIndex(this),
                GetScope = () => Scope,
                GetTitle = () => NodeName ?? Scope.GetEnumeration().Name,
                IsTitleChanged = (e) => e.PropertyName is nameof(NodeName)
            };
        }
    }
}
