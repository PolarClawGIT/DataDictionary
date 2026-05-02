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
    public class ProcessArgumentKey : ProcessKey,
        IProcessArgumentKey, IKeyEquality<IProcessArgumentKey>, IKeyEquality<ProcessArgumentKey>
    {
        /// <inheritdoc/>
        public Guid? ArgumentId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the DomainProcessArgument Key 
        /// </summary>
        /// <param name="source"></param>
        public ProcessArgumentKey(IProcessArgumentKey source) : base(source)
        {
            if (source.ArgumentId is Guid) { ArgumentId = source.ArgumentId; }
            else { ArgumentId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ProcessArgumentKey? other)
        {
            return other is ProcessArgumentKey key
                && ArgumentId.HasValue && ArgumentId != Guid.Empty
                && key.ArgumentId.HasValue && key.ArgumentId != Guid.Empty
                && EqualityComparer<Guid?>.Default.Equals(ArgumentId, other.ArgumentId)
                && base.Equals(key);
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