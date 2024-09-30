using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ModelData;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface ISubjectAreaIndexName : IModelSubjectAreaUniqueKey
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

        /// <summary>
        /// Convert SubjectAreaIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(SubjectAreaIndexName source)
        { return new DataIndexName() { Title = source.SubjectAreaTitle ?? String.Empty }; }
    }
}
