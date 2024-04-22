using DataDictionary.DataLayer.DatabaseData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IDomainKey : IDbDomainKey { }

    /// <inheritdoc/>
    public class DomainKey : DbDomainKey, IDomainKey
    {
        /// <inheritdoc cref="DbDomainKey(IDbDomainKey)"/>
        public DomainKey(IDomainKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
    public interface IDomainKeyName : IDbDomainKeyName
    { }

    /// <inheritdoc/>
    public class DomainKeyName : DbDomainKeyName, IDomainKeyName
    {
        /// <inheritdoc cref="DbDomainKeyName(IDbDomainKeyName)"/>
        public DomainKeyName(IDomainKeyName source) : base(source) { }
    }
}
