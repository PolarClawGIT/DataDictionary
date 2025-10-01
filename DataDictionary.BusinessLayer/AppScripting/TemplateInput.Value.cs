using DataDictionary.DataLayer.AppScript;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateInputValue : ITemplateInputItem,
        ITemplateIndex, IDataSourceIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    public class TemplateInputValue : TemplateInputItem, ITemplateInputValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ScriptingData; } }

        /// <inheritdoc/>
        public TemplateInputValue() : base()
        { }

        /// <inheritdoc/>
        public TemplateInputValue(ITemplateIndex template) : base(template)
        { }
    }
}
