using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintColumnIndex : IConstraintColumnKey
    { }

    /// <inheritdoc/>
    public class ConstraintColumnIndex : ConstraintColumnKey, IConstraintColumnIndex,
        IKeyEquality<IConstraintColumnIndex>, IKeyEquality<ConstraintColumnIndex>
    {
        /// <inheritdoc cref="ConstraintColumnKey(IConstraintColumnKey)"/>
        public ConstraintColumnIndex(IConstraintColumnKey source) : base(source)
        { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintColumnIndex? other)
        { return other is IConstraintColumnKey value && Equals(new ConstraintColumnKey(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintColumnIndex? other)
        { return other is IConstraintColumnKey value && Equals(new ConstraintColumnKey(value)); }

        /// <summary>
        /// Convert ConstraintColumnIndex to a DataIndex
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndex(ConstraintColumnIndex source)
        { return new DataIndex() { SystemId = source.ConstraintColumnId ?? Guid.Empty }; }
    }
}
