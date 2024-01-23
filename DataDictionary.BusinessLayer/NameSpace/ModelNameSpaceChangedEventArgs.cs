using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.NameSpace
{

    /// <summary>
    /// List of supported ListChangeTypes
    /// </summary>
    public enum ModelNameSpaceChangedType
    {
        /// <summary>
        /// An item added to the list. 
        /// Add raises this and data is the item added.
        /// </summary>
        ItemAdded = 1,

        /// <summary>
        /// An item deleted from the list.
        /// Remove and Clear raises this and data is the item removed.
        /// </summary>
        ItemDeleted = 2,

        /// <summary>
        /// An item was moved.
        /// </summary>
        ItemMoved = 3,
    }

    /// <summary>
    /// Event Args for change in the Model Name Space list
    /// </summary>
    /// <remarks>Inspired by the IBindingList.ListChanged Event</remarks>
    public class ModelNameSpaceChangedEventArgs : EventArgs
    {
        /// <summary>
        /// What type of change was made
        /// </summary>
        public ModelNameSpaceChangedType ChangedType { get; }

        /// <summary>
        /// Model NameSpace Item impacted, if any.
        /// </summary>
        public IModelNameSpaceItem? Item { get; }

        /// <summary>
        /// Constructor for Event Args for change in the Model Name Space list.
        /// </summary>
        /// <param name="changedType"></param>
        /// <param name="data"></param>
        public ModelNameSpaceChangedEventArgs(ModelNameSpaceChangedType changedType, IModelNameSpaceItem? data)
        {
            this.ChangedType = changedType;
            this.Item = data;
        }

    }
}
