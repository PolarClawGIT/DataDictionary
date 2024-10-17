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
        IPrincipleIndex, IObjectIndex, IScopeType
    { }

    /// <inheritdoc/>
    public class ObjectOwnerValue : ObjectOwnerItem, IObjectOwnerValue
    {
        /// <inheritdoc/>
        public ObjectOwnerValue() : base() { }

        /// <inheritdoc cref="ObjectOwnerItem.ObjectOwnerItem(IPrincipleKey)"/>
        public ObjectOwnerValue(IPrincipleIndex principleIndex) : base(principleIndex)
        { }

        /// <inheritdoc cref="ObjectOwnerItem.ObjectOwnerItem(IPrincipleKey, IObjectKey)"/>
        public ObjectOwnerValue(IPrincipleIndex principleIndex, IObjectIndex objectIndex) : base(principleIndex, objectIndex)
        { }

        /// <inheritdoc/>
        public ScopeType Scope { get { return ScopeType.SecurityOwner; } }
    }
}
