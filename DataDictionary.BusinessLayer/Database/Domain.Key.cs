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
}
