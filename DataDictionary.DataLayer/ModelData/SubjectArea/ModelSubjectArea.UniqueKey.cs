using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ModelData.SubjectArea
{
    /// <summary>
    /// Interface for the Model Subject Area Unique Key
    /// </summary>
    public interface IModelSubjectAreaUniqueKey : IKey
    {
        /// <summary>
        /// Title of the Subject Area
        /// </summary>
        String? SubjectAreaTitle { get; }

    }

    /// <summary>
    /// Implementation for Model Subject Area Unique Key
    /// </summary>
    public class ModelSubjectAreaUniqueKey : IModelSubjectAreaUniqueKey,
        IKeyComparable<IModelSubjectAreaUniqueKey>, IKeyComparable<ModelSubjectAreaUniqueKey>
    {
        /// <inheritdoc/>
        public String SubjectAreaTitle { get; init; } = string.Empty;

        /// <summary>
        /// Constrictor for Model Subject Area Unique Key
        /// </summary>
        /// <param name="source"></param>
        public ModelSubjectAreaUniqueKey(IModelSubjectAreaUniqueKey source) : base()
        {
            if (source.SubjectAreaTitle is String) { SubjectAreaTitle = source.SubjectAreaTitle; }
            else { SubjectAreaTitle = String.Empty; }
        }

        #region IEquatable, IComparable
        /// <inheritdoc/>
        public Boolean Equals(ModelSubjectAreaUniqueKey? other)
        {
            return
                other is ModelSubjectAreaUniqueKey &&
                !String.IsNullOrEmpty(SubjectAreaTitle) &&
                !String.IsNullOrEmpty(other.SubjectAreaTitle) &&
                SubjectAreaTitle.Equals(other.SubjectAreaTitle, KeyExtension.CompareString);

        }

        /// <inheritdoc/>
        public virtual Boolean Equals(IModelSubjectAreaUniqueKey? other)
        { return other is IModelSubjectAreaUniqueKey value && Equals(new ModelSubjectAreaUniqueKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IModelSubjectAreaUniqueKey value && Equals(new ModelSubjectAreaUniqueKey(value)); }

        /// <inheritdoc/>
        public Int32 CompareTo(ModelSubjectAreaUniqueKey? other)
        {
            if (other is ModelSubjectAreaUniqueKey value)
            { return string.Compare(SubjectAreaTitle, value.SubjectAreaTitle, true); }
            else { return 1; }
        }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(IModelSubjectAreaUniqueKey? other)
        { if (other is IModelSubjectAreaUniqueKey value) { return CompareTo(new ModelSubjectAreaUniqueKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public virtual Int32 CompareTo(object? obj)
        { if (obj is IModelSubjectAreaUniqueKey value) { return CompareTo(new ModelSubjectAreaUniqueKey(value)); } else { return 1; } }

        /// <inheritdoc/>
        public static Boolean operator ==(ModelSubjectAreaUniqueKey left, ModelSubjectAreaUniqueKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ModelSubjectAreaUniqueKey left, ModelSubjectAreaUniqueKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator <(ModelSubjectAreaUniqueKey left, ModelSubjectAreaUniqueKey right)
        { return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0; }

        /// <inheritdoc/>
        public static Boolean operator <=(ModelSubjectAreaUniqueKey left, ModelSubjectAreaUniqueKey right)
        { return ReferenceEquals(left, null) || left.CompareTo(right) <= 0; }

        /// <inheritdoc/>
        public static Boolean operator >(ModelSubjectAreaUniqueKey left, ModelSubjectAreaUniqueKey right)
        { return !ReferenceEquals(left, null) && left.CompareTo(right) > 0; }

        /// <inheritdoc/>
        public static Boolean operator >=(ModelSubjectAreaUniqueKey left, ModelSubjectAreaUniqueKey right)
        { return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0; }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        { return SubjectAreaTitle.GetHashCode(KeyExtension.CompareString); }
        #endregion

        /// <inheritdoc/>
        public override String ToString()
        { return this.SubjectAreaTitle; }


    }
}
