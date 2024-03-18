using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ISchemaItem : DataLayer.ScriptingData.Schema.ISchemaItem
    { }

    /// <inheritdoc/>
    public class SchemaItem : DataLayer.ScriptingData.Schema.SchemaItem, ISchemaItem
    {
        /// <inheritdoc/>
        public SchemaItem() : base() { }
    }
}
