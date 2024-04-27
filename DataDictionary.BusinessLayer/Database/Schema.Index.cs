<<<<<<< HEAD
﻿using DataDictionary.DataLayer.DatabaseData.Catalog;
using DataDictionary.DataLayer.DatabaseData.Schema;
=======
﻿using DataDictionary.DataLayer.DatabaseData.Schema;
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface ISchemaIndex : IDbSchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : DbSchemaKey, ISchemaIndex
    {
        /// <inheritdoc cref="DbSchemaKey(IDbSchemaKey)"/>
        public SchemaIndex(IDbSchemaKey source) : base(source)
        { }
    }

    /// <inheritdoc/>
<<<<<<< HEAD
    public interface ISchemaIndexName : IDbSchemaKeyName, IDbCatalogKeyName, ICatalogIndexName
=======
    public interface ISchemaIndexName : IDbSchemaKeyName, ICatalogIndexName
>>>>>>> RenameIndexValue
    { }

    /// <inheritdoc/>
    public class SchemaIndexName : DbSchemaKeyName, ISchemaIndexName
    {
        /// <inheritdoc cref="DbSchemaKeyName(IDbSchemaKeyName)"/>
        public SchemaIndexName(ISchemaIndexName source) : base(source) { }
    }
}
