using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.Resource;
using DataDictionary.DataLayer.AppGeneral;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <inheritdoc/>
    public interface IHelpSubjectIndexPath : IPathItem
    { }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexNameSpace : IHelpSubjectKeyNameSpace
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndexPath : PathIndex, IHelpSubjectIndexPath,
        IKeyEquality<IHelpSubjectIndexPath>, IKeyEquality<HelpSubjectIndexPath>
    {
        /// <inheritdoc cref="PathIndex.PathIndex(IPathItem[])"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexPath source) : base(source)
        { }

        /// <inheritdoc cref="HelpSubjectKeyNameSpace(IHelpSubjectKeyNameSpace)"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexNameSpace source) : base(PathIndex.Parse(source.NameSpace).ToArray())
        { }

        /// <inheritdoc cref="PathIndex(String?[])"/>
        public HelpSubjectIndexPath(params String?[] source) : base(source)
        { }

        /// <summary>
        /// Constructor that build from the base PathIndex
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectIndexPath(PathIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndexPath? other)
        { return other is IPathItem value && Equals(new PathIndex(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndexPath? other)
        { return other is IPathItem value && Equals(new PathIndex(value)); }

    }
}
