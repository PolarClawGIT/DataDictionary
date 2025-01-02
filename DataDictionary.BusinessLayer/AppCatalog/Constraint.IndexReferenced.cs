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
    public interface IConstraintIndexReferenced : IConstraintKeyReferenced
    { }

    /// <inheritdoc/>
    public class ConstraintIndexReferenced : ConstraintKeyReferenced, IConstraintIndexReferenced,
        IKeyEquality<IConstraintIndexReferenced>, IKeyEquality<ConstraintIndexName>
    {
        /// <inheritdoc cref="ConstraintKeyReferenced(IConstraintKeyReferenced)"/>
        public ConstraintIndexReferenced(IConstraintIndexReferenced source) : base(source) { }

        /// <inheritdoc/>
        public override TableIndexName AsTableName()
        { return new TableIndexName(base.AsTableName()); }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndexReferenced? other)
        { return other is IConstraintIndexReferenced value && Equals(new ConstraintKeyReferenced(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndexName? other)
        { return other is IConstraintIndexReferenced value && Equals(new ConstraintKeyReferenced(value)); }
    }
}
