using DataDictionary.DataLayer.DatabaseData.Constraint;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintValue : IDbConstraintItem, IConstraintIndex, IConstraintIndexName,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ConstraintValue : DbConstraintItem, IConstraintValue
    { }
}
