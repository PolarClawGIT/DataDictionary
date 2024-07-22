using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ModelData.SubjectArea
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
    public class ModelSubjectAreaKey : IModelSubjectAreaKey, IKeyEquality<IModelSubjectAreaKey>
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
        public bool Equals(IModelSubjectAreaKey? other)
        { return other is IModelSubjectAreaKey key && EqualityComparer<Guid?>.Default.Equals(SubjectAreaId, key.SubjectAreaId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IModelSubjectAreaKey value && Equals(new ModelSubjectAreaKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ModelSubjectAreaKey left, ModelSubjectAreaKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ModelSubjectAreaKey left, ModelSubjectAreaKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override int GetHashCode()
        { return HashCode.Combine(SubjectAreaId); }
        #endregion
    }
}
