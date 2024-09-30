using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using DataDictionary.DataLayer.ApplicationData;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectIndexPath : IPathItem
    { }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexNameSpace : IHelpKeyNameSpace
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndexPath : PathIndex, IHelpSubjectIndexPath,
        IKeyEquality<IHelpSubjectIndexPath>, IKeyEquality<HelpSubjectIndexPath>
    {
        /// <inheritdoc cref="PathIndex.PathIndex(IPathItem[])"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexPath source) : base(source)
        { }

        /// <inheritdoc cref="HelpKeyNameSpace(IHelpKeyNameSpace)"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexNameSpace source) : base(PathIndex.Parse(source.NameSpace).ToArray())
        { }

        /// <inheritdoc cref="PathIndex(String?[])"/>
        public HelpSubjectIndexPath(params String?[] source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndexPath? other)
        { return other is IPathItem value && Equals(new PathIndex(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndexPath? other)
        { return other is IPathItem value && Equals(new PathIndex(value)); }
    }
}
