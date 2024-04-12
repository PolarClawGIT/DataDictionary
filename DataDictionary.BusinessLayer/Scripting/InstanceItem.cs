using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface IInstanceItem : DataLayer.ScriptingData.Selection.IInstanceItem
    { }

    /// <inheritdoc/>
    public class InstanceItem : DataLayer.ScriptingData.Selection.InstanceItem, IInstanceItem
    {
        /// <inheritdoc/>
        public InstanceItem() : base() { }
    }
}
