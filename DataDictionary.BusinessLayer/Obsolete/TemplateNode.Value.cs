using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateNodeValue : ITemplateNodeItem,
        ITemplateIndex, ITemplateNodeIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateNodeValue : TemplateNodeItem, ITemplateNodeValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingTemplateNode; } }

        /// <inheritdoc cref="TemplateNodeItem()"/>
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

        /// <inheritdoc cref="TemplateNodeItem(ITemplateKey)"/>
        public TemplateNodeValue(TemplateIndex template): base(template)
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
