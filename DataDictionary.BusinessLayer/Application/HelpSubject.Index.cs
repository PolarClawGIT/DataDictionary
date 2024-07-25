using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.ApplicationData;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectIndex :IHelpKey
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndex : HelpKey, IHelpKey
    {
        /// <inheritdoc cref="HelpKey.HelpKey(IHelpKey)"/>
        public HelpSubjectIndex(IHelpSubjectIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexPath : INamedScopePath
    { }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexNameSpace : IHelpKeyNameSpace
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndexPath : NamedScopePath
    {
        /// <inheritdoc cref="NamedScopePath.NamedScopePath(INamedScopePath[])"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexPath source) : base(source) 
        { }

        /// <inheritdoc cref="HelpKeyNameSpace(IHelpKeyNameSpace)"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexNameSpace source) : base(NamedScopePath.Parse(source.NameSpace).ToArray())
        { }

        /// <inheritdoc cref="NamedScopePath(String?[])"/>
        public HelpSubjectIndexPath(params String?[] source): base(source)
        { }

    }
}
