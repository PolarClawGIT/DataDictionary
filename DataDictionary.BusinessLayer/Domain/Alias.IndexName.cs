using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DomainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Domain
{
    /// <inheritdoc/>
    public interface IAliasIndexName : IAliasKeyName
    { }

    /// <inheritdoc/>
    public class AliasIndexName : AliasKeyName, IAliasIndexName
    {
        /// <inheritdoc cref="AliasKeyName(IAliasKeyName)"/>
        public AliasIndexName(IAliasIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(AliasIndexName? other)
        { return other is IAliasIndexName key && Equals(new AliasIndexName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(IAliasIndexName? other)
        { return other is IAliasIndexName key && Equals(new AliasIndexName(key)); }
    }
}
