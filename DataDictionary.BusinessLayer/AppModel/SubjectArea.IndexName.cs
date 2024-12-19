using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface ISubjectAreaIndexName : ISubjectAreaUniqueKey
    { }

    /// <inheritdoc/>
    public class SubjectAreaIndexName : SubjectAreaUniqueKey, ISubjectAreaIndexName,
        IKeyEquality<ISubjectAreaIndexName>, IKeyEquality<SubjectAreaIndexName>
    {
        /// <inheritdoc cref="SubjectAreaUniqueKey(ISubjectAreaUniqueKey)"/>
        public SubjectAreaIndexName(ISubjectAreaIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ISubjectAreaIndexName? other)
        { return other is ISubjectAreaUniqueKey key && Equals(new SubjectAreaUniqueKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(SubjectAreaIndexName? other)
        { return other is ISubjectAreaUniqueKey key && Equals(new SubjectAreaUniqueKey(key)); }

        /// <summary>
        /// Convert SubjectAreaIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(SubjectAreaIndexName source)
        { return new DataIndexName() { Title = source.SubjectAreaTitle ?? String.Empty }; }
    }
}
