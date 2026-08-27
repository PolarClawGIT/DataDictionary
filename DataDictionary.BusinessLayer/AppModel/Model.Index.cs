using DataDictionary.BusinessLayer.AppSecurity;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IModelIndex : IModelKey
    { }

    /// <inheritdoc/>
    public class ModelIndex : ModelKey, IModelIndex,
        IKeyEquality<IModelIndex>, IKeyEquality<ModelIndex>,
        IAuthorization
    {
        /// <inheritdoc cref="ModelKey(IModelKey)"/>
        public ModelIndex(IModelIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IModelIndex? other)
        { return other is IModelKey key && Equals(new ModelKey(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ModelIndex? other)
        { return other is IModelKey key && Equals(new ModelKey(key)); }

        /// <summary>
        /// Convert ModelIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ModelIndex source)
        { return new DataIndex() { SystemId = source.ModelId ?? Guid.Empty }; }

        /// <summary>
        /// Convert ModelIndex to a SecurableIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator SecurableIndex(ModelIndex source)
        { return new SecurableIndex() { SecurableId = source.ModelId ?? Guid.Empty }; }

        /// <inheritdoc/>
        public (Boolean IsAdmin, Boolean IsOwner, Boolean IsGrant) GetAuthorization(IAuthorizationData authorizations)
        {
            if (HasValue)
            {
                return (
                IsAdmin: authorizations.IsModelAdmin,
                IsOwner: authorizations.IsModelOwner,
                IsGrant: authorizations.IsGrant(new SecurableIndex() { SecurableId = ModelId }));
            }
            else { return (false, false, false); }
        }
    }
}
