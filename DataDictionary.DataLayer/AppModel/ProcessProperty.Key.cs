using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Process Property Key
    /// </summary>
    public interface IProcessPropertyKey : IProcessKey, IPropertyKey
    { }

    /// <summary>
    /// Implantation for the Model Process Property Key
    /// </summary>
    public class ProcessPropertyKey : IProcessPropertyKey,
        IKeyEquality<IProcessPropertyKey>, IKeyEquality<ProcessPropertyKey>
    {
        /// <inheritdoc/>
        public Guid? ProcessId { get; init; } = Guid.Empty;

        /// <inheritdoc/>
        public Guid? PropertyId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Domain Process Property Key
        /// </summary>
        /// <param name="source"></param>
        public ProcessPropertyKey(IProcessPropertyKey source)
        {
            ProcessId = source.ProcessId;
            PropertyId = source.PropertyId;
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ProcessPropertyKey? other)
        {
            return other is ProcessPropertyKey key &&
                   EqualityComparer<Guid?>.Default.Equals(ProcessId, key.ProcessId) &&
                   EqualityComparer<Guid?>.Default.Equals(PropertyId, key.PropertyId);
        }

        /// <inheritdoc/>
        public Boolean Equals(IProcessPropertyKey? other)
        { return other is IProcessPropertyKey value && Equals(new ProcessPropertyKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IProcessPropertyKey value && Equals(new ProcessPropertyKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ProcessPropertyKey left, ProcessPropertyKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ProcessPropertyKey left, ProcessPropertyKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(ProcessId, PropertyId); }
        #endregion
    }
}
