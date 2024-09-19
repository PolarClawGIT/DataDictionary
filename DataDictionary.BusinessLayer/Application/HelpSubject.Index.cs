using DataDictionary.BusinessLayer.NamedScope;
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
        /// Convert HelpSubjectIndex to a DataLayerIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataLayerIndex(HelpSubjectIndex source)
        { return new DataLayerIndex() { DataLayerId = source.HelpId ?? Guid.Empty }; }

        /// <summary>
        /// Convert HelpSubjectIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(HelpSubjectIndex source)
        { return new DataIndex() { SystemId = source.HelpId ?? Guid.Empty }; }
    }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexPath : INamedScopePath
    { }

    /// <inheritdoc/>
    public interface IHelpSubjectIndexNameSpace : IHelpKeyNameSpace
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndexPath : NamedScopePath, IHelpSubjectIndexPath,
        IKeyEquality<IHelpSubjectIndexPath>, IKeyEquality<HelpSubjectIndexPath>
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

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndexPath? other)
        { return other is INamedScopePath value && Equals(new NamedScopePath(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndexPath? other)
        { return other is INamedScopePath value && Equals(new NamedScopePath(value)); }
    }
}
