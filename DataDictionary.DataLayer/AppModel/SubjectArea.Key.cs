using DataDictionary.Resource;

namespace DataDictionary.DataLayer.AppModel
{
    /// <summary>
    /// Interface for the Model Subject Area Key
    /// </summary>
    public interface ISubjectAreaKey : IKey
    {
        /// <summary>
        /// Application ID for the Model Subject Area.
        /// </summary>
        Guid? SubjectAreaId { get; }
    }

    /// <summary>
    /// Implementation for the Model Subject Area Key
    /// </summary>
    public class SubjectAreaKey : ISubjectAreaKey,
        IKeyEquality<ISubjectAreaKey>, IKeyEquality<SubjectAreaKey>
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Model Subject Area Key
        /// </summary>
        /// <param name="source"></param>
        public SubjectAreaKey(ISubjectAreaKey source) : base()
        {
            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(SubjectAreaKey? other)
        { return other is SubjectAreaKey key && EqualityComparer<Guid?>.Default.Equals(SubjectAreaId, key.SubjectAreaId); }

        /// <inheritdoc/>
        public Boolean Equals(ISubjectAreaKey? other)
        { return other is ISubjectAreaKey value && Equals(new SubjectAreaKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ISubjectAreaKey value && Equals(new SubjectAreaKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(SubjectAreaKey left, SubjectAreaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(SubjectAreaKey left, SubjectAreaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(SubjectAreaId); }
        #endregion
    }
}
