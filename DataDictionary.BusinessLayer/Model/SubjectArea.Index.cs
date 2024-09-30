using DataDictionary.BusinessLayer.ToolSet;
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
        /// Convert SubjectAreaIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SubjectAreaIndex source)
        { return new DataIndex() { SystemId = source.SubjectAreaId ?? Guid.Empty }; }
    }


}
