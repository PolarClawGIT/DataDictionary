using DataDictionary.DataLayer.DatabaseData.Routine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IRoutineDependencyValue : IDbRoutineDependencyItem,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class RoutineDependencyValue : DbRoutineDependencyItem,  IRoutineDependencyValue
    { }
}
