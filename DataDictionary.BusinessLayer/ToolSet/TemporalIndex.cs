using DataDictionary.DataLayer;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <inheritdoc/>
    public class TemporalIndex : TemporalKey
    {
        /// <inheritdoc cref="TemporalKey(ITemporal)"/>
        public TemporalIndex(ITemporal source) : base(source) { }

        /// <inheritdoc cref="TemporalKey(ITemporalItem)"/>
        public TemporalIndex(ITemporalValue source) : base(source) { }
    }
}
