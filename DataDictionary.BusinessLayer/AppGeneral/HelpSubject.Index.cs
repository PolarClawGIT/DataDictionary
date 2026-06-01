using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppGeneral;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppGeneral
{
    /// <inheritdoc/>
    public interface IHelpSubjectIndex :IHelpSubjectKey
    { }

    /// <inheritdoc/>
    public class HelpSubjectIndex : HelpSubjectKey, IHelpSubjectIndex,
        IKeyEquality<IHelpSubjectIndex>, IKeyEquality<HelpSubjectIndex>,
        IAuthorization
    {
        /// <inheritdoc cref="HelpSubjectKey.HelpSubjectKey()"/>
        public HelpSubjectIndex() : base()
        { }

        /// <inheritdoc cref="HelpSubjectKey.HelpSubjectKey(IHelpSubjectKey)"/>
        public HelpSubjectIndex(IHelpSubjectIndex source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(HelpSubjectIndex? other)
        { return other is IHelpSubjectKey value && Equals(new HelpSubjectKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IHelpSubjectIndex? other)
        { return other is IHelpSubjectKey value && Equals(new HelpSubjectKey(value)); }

        /// <summary>
        /// Convert HelpSubjectIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(HelpSubjectIndex source)
        { return new DataIndex() { SystemId = source.HelpId ?? Guid.Empty }; }

        /// <summary>
        /// Convert HelpSubjectIndex to a SecurableIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator SecurableIndex(HelpSubjectIndex source)
        { return new SecurableIndex() { SecurableId = source.HelpId ?? Guid.Empty }; }

        /// <inheritdoc/>
        public (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
        {
            if (HasValue)
            {
                return (
                IsAdmin: authorizations.IsHelpAdmin,
                IsOwner: authorizations.IsHelpOwner,
                IsGrant: authorizations.IsGrant(new SecurableIndex() { SecurableId = HelpId }));
            }
            else { return (false, false, false); }
        }
    }
}
