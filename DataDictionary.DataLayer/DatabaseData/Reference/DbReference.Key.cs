using DataDictionary.Resource;

namespace DataDictionary.DataLayer.DatabaseData.Reference
{
    /// <summary>
    /// Interface for the Database Reference Key.
    /// </summary>
    public interface IDbReferenceKey: IKey
    {
        /// <summary>
        /// Application ID for the Reference.
        /// </summary>
        Guid? ReferenceId { get; }
    }

    /// <summary>
    /// Implementation for the Database Reference Key.
    /// </summary>
    public class DbReferenceKey : IDbReferenceKey,
        IKeyEquality<IDbReferenceKey>, IKeyEquality<DbReferenceKey>
    {
        /// <inheritdoc/>
        public Guid? ReferenceId { get; init; } = Guid.Empty;

    /// <summary>
    /// Constructor for the Constraint Key.
    /// </summary>
    /// <param name="source"></param>
    public DbReferenceKey(IDbReferenceKey source) : base()
    {
        if (source.ReferenceId is Guid value) { ReferenceId = value; }
        else { ReferenceId = Guid.Empty; }
    }

    #region IEquatable
    /// <inheritdoc/>
    public Boolean Equals(DbReferenceKey? other)
    { return other is DbReferenceKey && EqualityComparer<Guid?>.Default.Equals(ReferenceId, other.ReferenceId); }

    /// <inheritdoc/>
    public virtual Boolean Equals(IDbReferenceKey? other)
    { return other is IDbReferenceKey value && Equals(new DbReferenceKey(value)); }

    /// <inheritdoc/>
    public override Boolean Equals(object? other)
    { return other is IDbReferenceKey value && Equals(new DbReferenceKey(value)); }

    /// <inheritdoc/>
    public static Boolean operator ==(DbReferenceKey left, DbReferenceKey right)
    { return left.Equals(right); }

    /// <inheritdoc/>
    public static Boolean operator !=(DbReferenceKey left, DbReferenceKey right)
    { return !left.Equals(right); }

    /// <inheritdoc/>
    public override Int32 GetHashCode()
    { return HashCode.Combine(ReferenceId); }

    #endregion
}
}
