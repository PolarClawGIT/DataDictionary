using DataDictionary.BusinessLayer.NamedScope;
using DataDictionary.DataLayer.DatabaseData.Constraint;
using DataDictionary.Resource.Enumerations;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintValue : IDbConstraintItem, IConstraintIndex, IConstraintIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ConstraintValue : DbConstraintItem, IConstraintValue, INamedScopeSourceValue
    {
        /// <inheritdoc cref="DbConstraintItem()"/>
        public ConstraintValue() : base()
        { }

        /// <inheritdoc/>
        public DataLayerIndex GetIndex()
        { return new ConstraintIndex(this); }

        /// <inheritdoc/>
        public NamedScopePath GetPath()
        { return new NamedScopePath(DatabaseName, SchemaName, ConstraintName); }

        /// <inheritdoc/>
        public String GetTitle()
        { return ConstraintName ?? ScopeEnumeration.Cast(Scope).Name; }
    }
}
