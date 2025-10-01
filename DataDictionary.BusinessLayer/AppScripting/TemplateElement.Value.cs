using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateElementValue : 
        ITemplateElementItem, ITemplateNodeValue,
        ITemplateElementIndex, ITemplateIndex, ITemplateNodeIndex, ITemplateElementIndexParent,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class TemplateElementValue : TemplateElementItem, ITemplateElementValue, IDataValue
    {
        IDataValue dataValue; // Backing field for IDataValue

        /// <inheritdoc/>
        DataIndex IDataValue.Index { get { return dataValue.Index; } }

        /// <inheritdoc/>
        String IDataValue.Title { get { return dataValue.Title; } }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public TemplateElementValue() : base()
        {
            dataValue = new DataValue(this)
            {
                GetIndex = () => new TemplateElementIndex(this),
                GetScope = () => Scope,
                GetTitle = () => this.ElementName ?? Scope.GetEnumeration().Name,
                IsTitleChanged = (e) => e.PropertyName is nameof(ElementName)
            };
        }
    }
}
