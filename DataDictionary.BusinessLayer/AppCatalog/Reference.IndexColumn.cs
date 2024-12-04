using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;

namespace DataDictionary.BusinessLayer.AppCatalog;

/// <inheritdoc/>
public interface IReferencedIndexColumn : IReferencedKeyColumn
{ }

/// <inheritdoc/>
public class ReferencedIndexColumn : ReferencedKeyColumn, IReferencedIndexColumn,
    IKeyEquality<ReferencedKeyColumn>, IKeyEquality<ReferencedIndexColumn>
{
    /// <inheritdoc cref="ReferencedKeyColumn(IReferencedKeyColumn)"/>
    public ReferencedIndexColumn(IReferencedIndexColumn source) : base(source) { }

    /// <inheritdoc cref="ReferencedKeyColumn(ITableColumnKeyName)"/>
    public ReferencedIndexColumn(ITableColumnIndexName source) : base(source) { }

    /// <inheritdoc/>
    public Boolean Equals(IReferencedIndexColumn? other)
    { return other is IReferencedIndexColumn value && Equals(new ReferencedKeyColumn(value)); }

    /// <inheritdoc/>
    public Boolean Equals(ReferencedIndexColumn? other)
    { return other is IReferencedIndexColumn value && Equals(new ReferencedKeyColumn(value)); }
}