using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintIndexReferenced : IDbConstraintKeyReferenced
    { }

    /// <inheritdoc/>
    public class ConstraintIndexReferenced : DbConstraintKeyReferenced, IConstraintIndexReferenced,
        IKeyEquality<IConstraintIndexReferenced>, IKeyEquality<ConstraintIndexName>
    {
        /// <inheritdoc cref="DbConstraintKeyReferenced(IDbConstraintKeyReferenced)"/>
        public ConstraintIndexReferenced(IConstraintIndexReferenced source) : base(source) { }

        /// <inheritdoc/>
        public override TableIndexName AsTableName()
        { return new TableIndexName(base.AsTableName()); }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndexReferenced? other)
        { return other is IConstraintIndexReferenced value && Equals(new DbConstraintKeyReferenced(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndexName? other)
        { return other is IConstraintIndexReferenced value && Equals(new DbConstraintKeyReferenced(value)); }
    }
}
