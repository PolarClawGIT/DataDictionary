using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Process Key
    /// </summary>
    public interface IProcessKey : IKey
    {
        /// <summary>
        /// Application ID for the Model Process.
        /// </summary>
        Guid? ProcessId { get; }
    }

    /// <summary>
    /// Implementation for the Model Process Key
    /// </summary>
    public class ProcessKey : IProcessKey,
        IKeyEquality<IProcessKey>, IKeyEquality<ProcessKey>
    {
        /// <inheritdoc/>
        public Guid? ProcessId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty Process Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public ProcessKey()
        { }

        /// <summary>
        /// Constructor for the Domain Process Key
        /// </summary>
        /// <param name="source"></param>
        public ProcessKey(IProcessKey source) : base()
        {
            if (source.ProcessId is Guid) { ProcessId = source.ProcessId; }
            else { ProcessId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ProcessKey? other)
        { return other is ProcessKey key && key.ProcessId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(ProcessId, key.ProcessId); }

        /// <inheritdoc/>
        public Boolean Equals(IProcessKey? other)
        { return other is IProcessKey value && Equals(new ProcessKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IProcessKey value && Equals(new ProcessKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ProcessKey left, ProcessKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ProcessKey left, ProcessKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ProcessId); }

        #endregion
    }

}
