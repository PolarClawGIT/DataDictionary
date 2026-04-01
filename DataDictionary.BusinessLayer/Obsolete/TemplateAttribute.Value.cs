using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateAttributeValue : 
        ITemplateAttributeItem, ITemplateNodeValue,
        ITemplateAttributeIndex, ITemplateIndex, ITemplateNodeIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    [Obsolete]
    public class TemplateAttributeValue : TemplateAttributeItem, ITemplateAttributeValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public TemplateAttributeValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateAttributeIndex(this),
                GetScope = () => Scope,
                GetTitle = () => this.AttributeName ?? Scope.GetEnumeration().Name,
                IsTitleChanged = (e) => e.PropertyName is nameof(AttributeName)
            };
        }
    }
}
