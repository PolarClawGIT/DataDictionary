using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource.Enumerations;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessSubjectAreaValue : IProcessSubjectAreaItem, IProcessIndex, ISubjectAreaIndex,
        IScopeType, ITemporal
    { }

    /// <inheritdoc/>
    public class ProcessSubjectAreaValue : ProcessSubjectAreaItem, IProcessSubjectAreaValue
    {
        /// <inheritdoc/>
        public DataIndex Index => throw new NotImplementedException();

        /// <inheritdoc/>
        public String Title => throw new NotImplementedException();

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.ModelProcessSubjectArea; } }

        /// <inheritdoc/>
        public ProcessSubjectAreaValue() : base() { }

        /// <inheritdoc cref="ProcessSubjectAreaItem(IProcessKey, ISubjectAreaKey)"/>
        public ProcessSubjectAreaValue(IProcessIndex Process, ISubjectAreaIndex subjectArea) : base(Process, subjectArea)
        { }
    }
}
