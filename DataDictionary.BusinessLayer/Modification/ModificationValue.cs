using DataDictionary.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.BindingTable;

namespace DataDictionary.BusinessLayer.Modification
{
    /// <summary>
    /// Item can be cast as a Generic Modification Value
    /// </summary>
    public interface IModificationValue: ITemporalTable,
        IBindingPropertyChanged, IBindingRowState, ITitle
    {
        /// <summary>
        /// Generic Description for a Value
        /// </summary>
        String Description { get { return GetDescription(); } }

        /// <summary>
        /// Gets the generic Description from the Value
        /// </summary>
        /// <returns></returns>
        String GetDescription();

    }
}
