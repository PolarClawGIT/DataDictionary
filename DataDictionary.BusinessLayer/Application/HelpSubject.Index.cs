using DataDictionary.DataLayer.ApplicationData.Help;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectIndex : IHelpKey
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndex : HelpKey, IHelpSubjectIndex
    {
        /// <inheritdoc cref="HelpKey(IHelpKey)"/>
        public HelpSubjectIndex(IHelpSubjectIndex source) : base(source) { }
    }
}
