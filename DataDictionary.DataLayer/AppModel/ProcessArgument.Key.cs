using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model ProcessArgument Key 
    /// </summary>
    public interface IProcessArgumentKey : IProcessKey
    {
        /// <summary>
        /// Application ID for the ProcessArgument.
        /// </summary>
        Guid? ArgumentId { get; }
    }

    /// <summary>
    /// Implementation of the ProcessArgument Key 
    /// </summary>
    public class ProcessArgumentKey : IProcessArgumentKey,
        IKeyEquality<IProcessArgumentKey>, IKeyEquality<ProcessArgumentKey>
    {
        /// <inheritdoc/>
        public Guid? ProcessId { get; init; }

        /// <inheritdoc/>
        public Guid? ArgumentId { get; init; }

        /// <summary>
        /// Constructor for the DomainProcessArgument Key 
        /// </summary>
        /// <param name="source"></param>
        public ProcessArgumentKey(IProcessArgumentKey source) : base()
        {
            if (source.ProcessId is Guid) { ProcessId = source.ProcessId; }
            else { ProcessId = Guid.Empty; }

            if (source.ArgumentId is Guid) { ArgumentId = source.ArgumentId; }
            else { ArgumentId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ProcessArgumentKey? other)
        {
            return other is IProcessArgumentKey key
                && EqualityComparer<Guid?>.Default.Equals(ProcessId, key.ProcessId)
                && EqualityComparer<Guid?>.Default.Equals(ArgumentId, key.ArgumentId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IProcessArgumentKey? other)
        { return other is IProcessArgumentKey value && Equals(new ProcessArgumentKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IProcessArgumentKey value && Equals(new ProcessArgumentKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ProcessArgumentKey left, ProcessArgumentKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ProcessArgumentKey left, ProcessArgumentKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ProcessId, ArgumentId); }

        #endregion
    }
}