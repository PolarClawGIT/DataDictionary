using DataDictionary.DataLayer.DatabaseData.Constraint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IConstraintColumnValue : IDbConstraintColumnItem,
        IConstraintIndexName, ITableColumnIndexName, IConstraintColumnIndexReferenced,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ConstraintColumnValue : DbConstraintColumnItem, IConstraintColumnValue
    { }
}
