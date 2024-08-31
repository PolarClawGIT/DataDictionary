using DataDictionary.DataLayer.DatabaseData.Reference;
using DataDictionary.DataLayer.DatabaseData.Table;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.Database;

/// <inheritdoc/>
public interface IReferencedIndexColumn : IDbReferencedKeyColumn
{ }

/// <inheritdoc/>
public class ReferencedIndexColumn : DbReferencedKeyColumn, IReferencedIndexColumn,
    IKeyEquality<DbReferencedKeyColumn>, IKeyEquality<ReferencedIndexColumn>
{
    /// <inheritdoc cref="DbReferencedKeyColumn(IDbReferencedKeyColumn)"/>
    public ReferencedIndexColumn(IReferencedIndexColumn source) : base(source) { }

    /// <inheritdoc cref="DbReferencedKeyColumn(IDbTableColumnKeyName)"/>
    public ReferencedIndexColumn(ITableColumnIndexName source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferencedIndexColumn? other)
    { return other is IReferencedIndexColumn value && Equals(new DbReferencedKeyColumn(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferencedIndexColumn? other)
    { return other is IReferencedIndexColumn value && Equals(new DbReferencedKeyColumn(value)); }
}