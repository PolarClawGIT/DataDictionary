using DataDictionary.DataLayer.ScriptingData.Schema;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
>>>>>>> RenameIndexValue

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
<<<<<<< HEAD
    public class SchemaKey : DataLayer.ScriptingData.Schema.SchemaKey
    {
        /// <inheritdoc/>
        public SchemaKey(ISchemaKey source) : base(source) { }
=======
    public interface ISchemaIndex : ISchemaKey
    { }

    /// <inheritdoc/>
    public class SchemaIndex : SchemaKey, ISchemaIndex
    {
        /// <inheritdoc cref="SchemaKey(ISchemaKey)"/>
        public SchemaIndex(ISchemaIndex source) : base(source) { }
>>>>>>> RenameIndexValue
    }
}
