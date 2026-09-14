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
    public class HelpSubjectIndexPath : PathItem, IHelpSubjectIndexPath,
        IKeyEquality<IHelpSubjectIndexPath>, IKeyEquality<HelpSubjectIndexPath>
    {
        /// <inheritdoc cref="PathItem.PathItem(IPathItem[])"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexPath source) : base(source)
        { }

        /// <inheritdoc cref="HelpSubjectKeyNameSpace(IHelpSubjectKeyNameSpace)"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexNameSpace source) : base(PathItem.Parse(source.NameSpace).ToArray())
        { }

        /// <inheritdoc cref="PathItem(String?[])"/>
        public HelpSubjectIndexPath(params String?[] source) : base(source)
        { }

        /// <summary>
        /// Constructor that build from the base PathIndex
        /// </summary>
        /// <param name="source"></param>
        public HelpSubjectIndexPath(PathItem source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndexPath? other)
        { return other is IPathItem value && Equals(new PathItem(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndexPath? other)
        { return other is IPathItem value && Equals(new PathItem(value)); }

    }
}
