using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateAttributeValue : 
        ITemplateAttributeItem, ITemplateNodeValue,
        ITemplateAttributeIndex, ITemplateIndex, ITemplateNodeIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
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
                GetTitle = () => this.AttributeName ?? ScopeEnumeration.Cast(Scope).Name,
                IsTitleChanged = (e) => e.PropertyName is nameof(AttributeName)
            };
        }
    }
}
