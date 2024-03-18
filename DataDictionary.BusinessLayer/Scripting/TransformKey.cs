using DataDictionary.DataLayer.ScriptingData.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <inheritdoc/>
    public class TransformKey : DataLayer.ScriptingData.Transform.TransformKey
    {
        /// <inheritdoc/>
        public TransformKey(ITransformKey source) : base(source) { }
    }
}
