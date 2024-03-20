using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface component for the Scripting Engine Column
    /// </summary>
    public interface IColumnData: IBindingList<ColumnItem>
    { }

    /// <summary>
    /// Implementation component for the Scripting Engine Column
    /// </summary>
    public class ColumnData : DataLayer.ScriptingData.Schema.ColumnCollection<ColumnItem>, IColumnData
    { }
}
