using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IObjectOwnerValue : IObjectOwnerItem,
        IPrincipalIndex, IObjectIndex, IScopeType
    { }

    /// <inheritdoc/>
    public class ObjectOwnerValue : ObjectOwnerItem, IObjectOwnerValue
    {
        /// <inheritdoc/>
        public ObjectOwnerValue() : base() { }

        /// <inheritdoc cref="ObjectOwnerItem.ObjectOwnerItem(IPrincipalKey)"/>
        public ObjectOwnerValue(IPrincipalIndex principalIndex) : base(principalIndex)
        { }

        /// <inheritdoc cref="ObjectOwnerItem.ObjectOwnerItem(IPrincipalKey, IObjectKey)"/>
        public ObjectOwnerValue(IPrincipalIndex principalIndex, IObjectIndex objectIndex) : base(principalIndex, objectIndex)
        { }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.SecurityOwner; } }
    }
}
