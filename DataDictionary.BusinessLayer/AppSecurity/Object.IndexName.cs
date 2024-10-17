using DataDictionary.DataLayer.AppSecurity;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppSecurity
{
    /// <inheritdoc/>
    public interface IObjectIndexName : IObjectKeyName
    { }

    /// <inheritdoc/>
    public class ObjectIndexName : ObjectKeyName, IObjectIndexName,
        IKeyEquality<IObjectIndexName>, IKeyEquality<ObjectIndexName>
    {
        /// <inheritdoc cref="ObjectKey.ObjectKey(IObjectKey)"/>
        public ObjectIndexName(IObjectIndexName source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(ObjectIndexName? other)
        { return other is IObjectKey value && Equals(new ObjectKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(IObjectIndexName? other)
        { return other is IObjectKey value && Equals(new ObjectKey(value)); }
    }
}
