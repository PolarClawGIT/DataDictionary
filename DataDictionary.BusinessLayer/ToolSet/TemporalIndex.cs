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
    public interface ITemporalIndex : ITemporalKey
    { }

    /// <inheritdoc/>
    public class TemporalIndex : TemporalKey, ITemporalIndex,
        IKeyEquality<ITemporalIndex>, IKeyEquality<TemporalIndex>
    {
        /// <inheritdoc cref="TemporalKey(ITemporalKey)"/>
        public TemporalIndex(ITemporalIndex source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(ITemporalIndex? other)
        { return other is ITemporalKey value && Equals(new TemporalKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(TemporalIndex? other)
        { return other is ITemporalKey value && Equals(new TemporalKey(value)); }
    }
}
