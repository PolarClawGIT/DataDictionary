using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessArgumentIndex : IProcessArgumentKey
    { }

    /// <inheritdoc/>
    public class ProcessArgumentIndex : ProcessArgumentKey, IProcessArgumentIndex,
        IKeyEquality<IProcessArgumentIndex>, IKeyEquality<ProcessArgumentIndex>
    {
        /// <inheritdoc cref="ProcessArgumentKey(IProcessArgumentKey)"/>
        public ProcessArgumentIndex(IProcessArgumentIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IProcessArgumentIndex? other)
        { return other is IProcessArgumentKey key && Equals(new ProcessArgumentKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ProcessArgumentIndex? other)
        { return other is IProcessArgumentKey key && Equals(new ProcessArgumentKey(key)); }
    }

}
