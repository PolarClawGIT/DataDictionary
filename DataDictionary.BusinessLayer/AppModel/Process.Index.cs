using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessIndex : IProcessKey
    { }

    /// <inheritdoc/>
    public class ProcessIndex : ProcessKey, IProcessIndex,
        IKeyEquality<IProcessIndex>, IKeyEquality<ProcessIndex>
    {
        /// <inheritdoc cref="ProcessKey()"/>
        public ProcessIndex() : base() { }

        /// <inheritdoc cref="ProcessKey(IProcessKey)"/>
        public ProcessIndex(IProcessIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IProcessIndex? other)
        { return other is IProcessKey key && Equals(new ProcessKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ProcessIndex? other)
        { return other is IProcessKey key && Equals(new ProcessKey(key)); }

        /// <summary>
        /// Convert ProcessIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ProcessIndex source)
        { return new DataIndex() { SystemId = source.ProcessId ?? Guid.Empty }; }
    }
}
