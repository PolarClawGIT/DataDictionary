using DataDictionary.DataLayer.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Model
{
    /// <inheritdoc/>
    public interface ISubjectAreaIndex : IModelSubjectAreaKey
    { }

    /// <inheritdoc/>
    public class SubjectAreaIndex : ModelSubjectAreaKey
    {
        /// <inheritdoc cref="ModelSubjectAreaKey(IModelSubjectAreaKey)"/>
        public SubjectAreaIndex(ISubjectAreaIndex source) : base(source) { }

        /// <summary>
        /// Convert SubjectAreaIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(SubjectAreaIndex source)
        { return new DataLayerIndex() { BusinessLayerId = source.SubjectAreaId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface ISubjectAreaIndexName :IModelSubjectAreaUniqueKey
    { }

    /// <inheritdoc/>
    public class SubjectAreaIndexName : ModelSubjectAreaUniqueKey
    {
        /// <inheritdoc cref="ModelSubjectAreaUniqueKey(IModelSubjectAreaUniqueKey)"/>
        public SubjectAreaIndexName(ISubjectAreaIndexName source) : base(source) { }
    }
}
