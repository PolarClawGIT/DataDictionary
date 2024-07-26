using DataDictionary.Resource;

namespace DataDictionary.DataLayer.ModelData
{
    /// <summary>
    /// Interface for the Model Subject Area Key
    /// </summary>
    public interface IModelSubjectAreaKey : IKey
    {
        /// <summary>
        /// Application ID for the Model Subject Area.
        /// </summary>
        Guid? SubjectAreaId { get; }
    }

    /// <summary>
    /// Implementation for the Model Subject Area Key
    /// </summary>
    public class ModelSubjectAreaKey : IModelSubjectAreaKey,
        IKeyEquality<IModelSubjectAreaKey>, IKeyEquality<ModelSubjectAreaKey>
    {
        /// <inheritdoc/>
        public Guid? SubjectAreaId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Model Subject Area Key
        /// </summary>
        /// <param name="source"></param>
        public ModelSubjectAreaKey(IModelSubjectAreaKey source) : base()
        {
            if (source.SubjectAreaId is Guid) { SubjectAreaId = source.SubjectAreaId; }
            else { SubjectAreaId = Guid.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ModelSubjectAreaKey? other)
        { return other is ModelSubjectAreaKey key && EqualityComparer<Guid?>.Default.Equals(SubjectAreaId, key.SubjectAreaId); }

        /// <inheritdoc/>
        public Boolean Equals(IModelSubjectAreaKey? other)
        { return other is IModelSubjectAreaKey value && Equals(new ModelSubjectAreaKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IModelSubjectAreaKey value && Equals(new ModelSubjectAreaKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ModelSubjectAreaKey left, ModelSubjectAreaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ModelSubjectAreaKey left, ModelSubjectAreaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return HashCode.Combine(SubjectAreaId); }
        #endregion
    }
}
