using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintColumnIndexName : IConstraintColumnKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class ConstraintColumnIndexName : ConstraintColumnKeyName, IConstraintColumnIndexName,
        IKeyEquality<IConstraintColumnIndexName>, IKeyEquality<ConstraintColumnIndexName>
    {
        /// <inheritdoc cref="ConstraintColumnKeyName(IConstraintColumnKeyName)"/>
        public ConstraintColumnIndexName(IConstraintColumnIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintColumnIndexName? other)
        { return other is IConstraintColumnKeyName value && Equals(new ConstraintColumnKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintColumnIndexName? other)
        { return other is IConstraintColumnKeyName value && Equals(new ConstraintColumnKeyName(value)); }

        /// <summary>
        /// Convert ConstraintColumnIndex to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(ConstraintColumnIndexName source)
        { return new DataIndexName() { Title = source.ColumnName ?? String.Empty }; }
    }
}
