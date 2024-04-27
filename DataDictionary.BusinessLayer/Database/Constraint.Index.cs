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
<<<<<<< HEAD
    public interface IConstraintIndexName : IDbConstraintKeyName, ITableIndexName
    { }

    /// <inheritdoc/>
    public class ConstraintIndexName : DbConstraintKeyName, ISchemaIndexName
=======
    public interface IConstraintIndexName : IDbConstraintKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class ConstraintIndexName : DbConstraintKeyName, IConstraintIndexName
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc cref="DbConstraintKeyName(IDbConstraintKeyName)"/>
        public ConstraintIndexName(IConstraintIndexName source) : base(source) { }
    }
}
