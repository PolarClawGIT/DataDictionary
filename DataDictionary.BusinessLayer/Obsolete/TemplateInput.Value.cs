using DataDictionary.DataLayer.Obsolete;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateInputValue : ITemplateInputItem,
        ITemplateIndex, IDataSourceIndex,
        IScopeType
    { }

    /// <inheritdoc/>
    [Obsolete]
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
