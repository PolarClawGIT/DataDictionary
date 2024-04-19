using DataDictionary.DataLayer.DatabaseData.ExtendedProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Database
{
    /// <inheritdoc/>
    public interface IExtendedPropertyValue : IDbExtendedPropertyItem, 
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class ExtendedPropertyValue : DbExtendedPropertyItem, IDbExtendedPropertyItem
    { }
}
