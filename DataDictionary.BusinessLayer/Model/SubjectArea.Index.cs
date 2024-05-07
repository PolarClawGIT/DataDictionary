using DataDictionary.DataLayer.ModelData.SubjectArea;
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
