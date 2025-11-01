using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppScript;

namespace DataDictionary.BusinessLayer.AppScripting
{
    /// <inheritdoc/>
    public interface ITemplateNodeOwnerValue : ITemplateNodeOwnerItem,
        ITemplateIndex, ITemplateNodeIndex, ITemplateNodeOwnerIndex
    { }

    /// <inheritdoc/>
    public class TemplateNodeOwnerValue : TemplateNodeOwnerItem, ITemplateNodeOwnerValue
    {
        /// <inheritdoc/>
        public TemplateNodeOwnerValue() : base() { }

        /// <inheritdoc/>
        public TemplateNodeOwnerValue(ITemplateNodeValue node) : base(node) { }

        /// <inheritdoc/>
        public TemplateNodeOwnerValue(ITemplateNodeValue node, ITemplateNodeIndex owner) : base(node, owner) { }
    }
}
