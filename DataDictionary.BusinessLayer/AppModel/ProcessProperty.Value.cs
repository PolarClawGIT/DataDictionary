using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessPropertyValue : IProcessPropertyItem,
        IPropertyIndex, IProcessIndex, IPropertySubType,
        IScopeType
    { }

    /// <inheritdoc/>
    public partial class ProcessPropertyValue : ProcessPropertyItem, IProcessPropertyValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProcessProperty; } }

        /// <inheritdoc cref="ProcessPropertyItem.ProcessPropertyItem()" />
        public ProcessPropertyValue() : base() { }

        /// <inheritdoc cref="ProcessPropertyItem.ProcessPropertyItem(IProcessKey)"/>
        public ProcessPropertyValue(IProcessIndex Process) : base(Process) { }

        /// <inheritdoc cref="ProcessPropertyItem.ProcessPropertyItem(IProcessKey, IPropertyKey)"/>
        public ProcessPropertyValue(IProcessIndex Process, IPropertyIndex property) : base(Process, property)
        { }
    }
}
