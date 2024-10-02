using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppGeneral
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
        /// <param name="source"></param>
        public static implicit operator DataIndex(HelpSubjectIndex source)
        { return new DataIndex() { SystemId = source.HelpId ?? Guid.Empty }; }
    }
}
