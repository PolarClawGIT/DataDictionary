using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.Obsolete;

namespace DataDictionary.BusinessLayer.Obsolete
{
    /// <inheritdoc/>
    [Obsolete]
    public interface ITemplateNodeOwnerValue : ITemplateNodeOwnerItem,
        ITemplateIndex, ITemplateNodeIndex, ITemplateNodeOwnerIndex
    { }

    /// <inheritdoc/>
    [Obsolete]
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
