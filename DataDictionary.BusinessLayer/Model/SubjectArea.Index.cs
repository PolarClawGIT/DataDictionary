using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface ISubjectAreaIndex : IModelSubjectAreaKey
    { }

    /// <inheritdoc/>
    public class SubjectAreaIndex : ModelSubjectAreaKey, IModelSubjectAreaKey,
        IKeyEquality<ISubjectAreaIndex>, IKeyEquality<SubjectAreaIndex>
    {
        /// <inheritdoc cref="ModelSubjectAreaKey(IModelSubjectAreaKey)"/>
        public SubjectAreaIndex(ISubjectAreaIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISubjectAreaIndex? other)
        { return other is IModelSubjectAreaKey key && Equals(new ModelSubjectAreaKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SubjectAreaIndex? other)
        { return other is IModelSubjectAreaKey key && Equals(new ModelSubjectAreaKey(key)); }

        /// <summary>
        /// Convert SubjectAreaIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(SubjectAreaIndex source)
        { return new DataLayerIndex() { DataLayerId = source.SubjectAreaId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ISubjectAreaIndexName :IModelSubjectAreaUniqueKey
    { }

    /// <inheritdoc/>
    public class SubjectAreaIndexName : ModelSubjectAreaUniqueKey, ISubjectAreaIndexName,
        IKeyEquality<ISubjectAreaIndexName>, IKeyEquality<SubjectAreaIndexName>
    {
        /// <inheritdoc cref="ModelSubjectAreaUniqueKey(IModelSubjectAreaUniqueKey)"/>
        public SubjectAreaIndexName(ISubjectAreaIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISubjectAreaIndexName? other)
        { return other is IModelSubjectAreaUniqueKey key && Equals(new ModelSubjectAreaUniqueKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SubjectAreaIndexName? other)
        { return other is IModelSubjectAreaUniqueKey key && Equals(new ModelSubjectAreaUniqueKey(key)); }
    }
}
