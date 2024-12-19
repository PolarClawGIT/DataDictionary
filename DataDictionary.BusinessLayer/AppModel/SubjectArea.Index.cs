using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface ISubjectAreaIndex : ISubjectAreaKey
    { }

    /// <inheritdoc/>
    public class SubjectAreaIndex : SubjectAreaKey, ISubjectAreaKey,
        IKeyEquality<ISubjectAreaIndex>, IKeyEquality<SubjectAreaIndex>
    {
        /// <inheritdoc cref="SubjectAreaKey(ISubjectAreaKey)"/>
        public SubjectAreaIndex(ISubjectAreaIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISubjectAreaIndex? other)
        { return other is ISubjectAreaKey key && Equals(new SubjectAreaKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SubjectAreaIndex? other)
        { return other is ISubjectAreaKey key && Equals(new SubjectAreaKey(key)); }

        /// <summary>
        /// Convert SubjectAreaIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(SubjectAreaIndex source)
        { return new DataIndex() { SystemId = source.SubjectAreaId ?? Guid.Empty }; }
    }


}
