using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer
{
    /// <summary>
    /// Data Object that supports validation.
    /// </summary>
    public interface IValidateList<TRow>
        where TRow : class, IBindingTableRow
    {
        /// <summary>
        /// Returns a List of IBindingTableRow with Errors in them.
        /// </summary>
        /// <returns>Items that contain an Error. Empty List otherwise.</returns>
        IReadOnlyList<TRow> Validate();
    }

    /// <summary>
    /// Data Object Row
    /// </summary>
    /// <typeparam name="TRow"></typeparam>
    public interface IValidateItem<TRow>
        where TRow: class, IBindingTableRow, IValidateItem<TRow>
    {
        /// <summary>
        /// Performs validation of the IBindingTableRow
        /// </summary>
        /// <returns>
        /// True if no issues are detected. False others.
        /// DataRow Row Errors contain the detail.</returns>
        Boolean Validate();
    }


}
