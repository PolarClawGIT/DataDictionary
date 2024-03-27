using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IColumnItem : DataLayer.ScriptingData.Schema.IColumnItem
    { }

    /// <inheritdoc/>
    public class ColumnItem : DataLayer.ScriptingData.Schema.ColumnItem, IColumnItem
    {
        /// <inheritdoc/>
        public ColumnItem() : base() { }
    }
}
