using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.ApplicationData;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Application
{
    /// <inheritdoc/>
    public interface IHelpSubjectIndex :IHelpKey
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndex : HelpKey, IHelpSubjectIndex,
        IKeyEquality<IHelpSubjectIndex>, IKeyEquality<HelpSubjectIndex>
    {
        /// <inheritdoc cref="HelpKey.HelpKey(IHelpKey)"/>
        public HelpSubjectIndex(IHelpSubjectIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndex? other)
        { return other is IHelpKey value && Equals(new HelpKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndex? other)
        { return other is IHelpKey value && Equals(new HelpKey(value)); }

        /// <summary>
        /// Convert HelpSubjectIndex to a DataIndex
        /// </summary>
        public DataIndex AsDataIndex()
        { return new DataIndex() { SystemId = HelpId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexPath : IPath
    { }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexNameSpace : IHelpKeyNameSpace
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndexPath : PathIndex, IHelpSubjectIndexPath,
        IKeyEquality<IHelpSubjectIndexPath>, IKeyEquality<HelpSubjectIndexPath>
    {
        /// <inheritdoc cref="PathIndex.PathIndex(IPath[])"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexPath source) : base(source) 
        { }

        /// <inheritdoc cref="HelpKeyNameSpace(IHelpKeyNameSpace)"/>
        public HelpSubjectIndexPath(IHelpSubjectIndexNameSpace source) : base(PathIndex.Parse(source.NameSpace).ToArray())
        { }

        /// <inheritdoc cref="PathIndex(String?[])"/>
        public HelpSubjectIndexPath(params String?[] source): base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndexPath? other)
        { return other is IPath value && Equals(new PathIndex(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndexPath? other)
        { return other is IPath value && Equals(new PathIndex(value)); }
    }
}
