using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessDefinitionValue : IProcessDefinitionItem,
        IDefinitionIndex, IProcessIndex, IDefinitionSubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public class ProcessDefinitionValue : ProcessDefinitionItem, IProcessDefinitionValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProcessDefinition; } }

        /// <inheritdoc/>
        public ProcessDefinitionValue() : base() { }

        /// <inheritdoc/>
        public ProcessDefinitionValue(IProcessIndex ProcessKey) : base(ProcessKey) { }
    }
}
