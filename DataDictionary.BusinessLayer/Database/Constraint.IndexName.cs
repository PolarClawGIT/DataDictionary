using DataDictionary.BusinessLayer.ToolSet;
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
    public interface IConstraintIndexName : IDbConstraintKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class ConstraintIndexName : DbConstraintKeyName, IConstraintIndexName,
        IKeyEquality<IConstraintIndexName>, IKeyEquality<ConstraintIndexName>
    {
        /// <inheritdoc cref="DbConstraintKeyName(IDbConstraintKeyName)"/>
        public ConstraintIndexName(IConstraintIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndexName? other)
        { return other is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndexName? other)
        { return other is IDbConstraintKeyName value && Equals(new DbConstraintKeyName(value)); }

        /// <summary>
        /// Convert ConstraintIndex to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(ConstraintIndexName source)
        { return new DataIndexName() { Title = source.ConstraintName ?? String.Empty }; }
    }
}
