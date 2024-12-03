using DataDictionary.DataLayer.AppCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintColumnValue : IConstraintColumnItem,
        IConstraintIndexName, ITableColumnIndexName, IConstraintColumnIndexReferenced,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ConstraintColumnValue : ConstraintColumnItem, IConstraintColumnValue
    { }
}
