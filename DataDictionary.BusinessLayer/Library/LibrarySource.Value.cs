using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;
using DbLayer = DataDictionary.DataLayer.LibraryData.Source;

namespace DataDictionary.BusinessLayer.Library
{
    /// <inheritdoc/>
    public interface ILibrarySourceValue : DbLayer.ILibrarySourceItem, ILibrarySourceIndex,
        IBindingTableRow, IBindingRowState, IBindingPropertyChanged
    { }

    /// <inheritdoc/>
    public class LibrarySourceValue : DbLayer.LibrarySourceItem, ILibrarySourceValue
    { }
}
