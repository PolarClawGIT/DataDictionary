using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public interface ITransformItem : DataLayer.ScriptingData.Transform.ITransformItem
    { }

    /// <inheritdoc/>
    public class TransformItem : DataLayer.ScriptingData.Transform.TransformItem, ITransformItem
    {
        /// <inheritdoc/>
        public TransformItem() : base() { }
    }
}
