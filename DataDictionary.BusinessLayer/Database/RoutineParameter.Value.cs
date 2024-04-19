using DataDictionary.DataLayer.DatabaseData.Routine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    public interface IRoutineParameterValue : IDbRoutineParameterItem,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    public class RoutineParameterValue : DbRoutineParameterItem, IRoutineParameterValue
    {
    }
}
