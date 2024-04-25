using DataDictionary.DataLayer.ScriptingData.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public class SchemaKey : DataLayer.ScriptingData.Schema.SchemaKey
    {
        /// <inheritdoc/>
        public SchemaKey(ISchemaKey source) : base(source) { }
    }
}
