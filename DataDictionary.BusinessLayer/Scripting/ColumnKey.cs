using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IColumnKey : DataLayer.ScriptingData.Schema.IColumnKey { }

    /// <inheritdoc/>
    public class ColumnKey: DataLayer.ScriptingData.Schema.ColumnKey, IColumnKey
    {
        /// <inheritdoc/>
        public ColumnKey(IColumnKey source): base(source) { }
    }
}
