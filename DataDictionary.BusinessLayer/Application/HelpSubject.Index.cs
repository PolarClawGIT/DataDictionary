using DataDictionary.DataLayer.ApplicationData.Help;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface IHelpSubjectIndex : IHelpKey
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndex : HelpKey, IHelpSubjectIndex
    {
        /// <inheritdoc cref="HelpKey(IHelpKey)"/>
        public HelpSubjectIndex(IHelpSubjectIndex source) : base(source) { }
=======
    public interface IHelpSubjectIndex :IHelpKey
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndex : HelpKey, IHelpKey
    {
        /// <inheritdoc cref="HelpKey.HelpKey(IHelpKey)"/>
        public HelpSubjectIndex(IHelpSubjectIndex source) : base(source)
        { }
>>>>>>> RenameIndexValue
    }
}
