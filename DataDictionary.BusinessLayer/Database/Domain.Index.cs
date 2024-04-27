using DataDictionary.DataLayer.DatabaseData.Domain;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public interface IDomainIndex : IDbDomainKey
    { }
=======
    public interface IDomainIndex : IDbDomainKey { }
>>>>>>> RenameIndexValue

    /// <inheritdoc/>
    public class DomainIndex : DbDomainKey, IDomainIndex
    {
        /// <inheritdoc cref="DbDomainKey(IDbDomainKey)"/>
        public DomainIndex(IDomainIndex source) : base(source)
        { }
    }

    /// <inheritdoc/>
<<<<<<< HEAD
    public interface IDomainIndexName : IDbDomainKeyName, ISchemaIndexName
=======
    public interface IDomainIndexName : IDbDomainKeyName
>>>>>>> RenameIndexValue
    { }

    /// <inheritdoc/>
    public class DomainIndexName : DbDomainKeyName, IDomainIndexName
    {
        /// <inheritdoc cref="DbDomainKeyName(IDbDomainKeyName)"/>
        public DomainIndexName(IDomainIndexName source) : base(source) { }
    }
}
