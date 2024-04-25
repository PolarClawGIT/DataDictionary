using DataDictionary.DataLayer.DatabaseData.Constraint;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintIndex : IDbConstraintKey { }

    /// <inheritdoc/>
    public class ConstraintIndex : DbConstraintKey
    {
        /// <inheritdoc cref="DbConstraintKey(IDbConstraintKey)"/>
        public ConstraintIndex(IConstraintIndex source) : base(source) { }
    }

    /// <inheritdoc/>
    public interface IConstraintIndexName : IDbConstraintKeyName
    { }

    /// <inheritdoc/>
    public class ConstraintIndexName : DbConstraintKeyName, IConstraintIndexName
    {
        /// <inheritdoc cref="DbConstraintKeyName(IDbConstraintKeyName)"/>
        public ConstraintIndexName(IConstraintIndexName source) : base(source) { }
    }
}
