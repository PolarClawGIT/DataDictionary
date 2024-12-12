using DataDictionary.DataLayer;
using DataDictionary.Resource;
using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.ToolSet
{
    /// <summary>
    /// Item can be cast as a Temporal Value
    /// </summary>
    public interface ITemporalValue : ITemporalItem, IDataValue
    { }
}
