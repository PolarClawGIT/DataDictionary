using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.Main.Enumerations
{
    /// <summary>
    /// List of different Images used by commands within the UI
    /// </summary>
    enum CommandImageType
    {
        /// <summary>
        /// Default image
        /// </summary>
        Default,

        /// <summary>
        /// Browse: Browse/View the items in the collection.
        /// </summary>
        Browse,
        
        /// <summary>
        /// Add: Adding/New items to the Model.
        /// </summary>
        Add,

        /// <summary>
        /// Delete: Delete/Remove items from the Model
        /// </summary>
        Delete,

        /// <summary>
        /// Save: Save the a Document
        /// </summary>
        Save,

        /// <summary>
        /// Open: Open the a Document
        /// </summary>
        Open,

        /// <summary>
        /// Import data from an external source.
        /// </summary>
        Import,

        /// <summary>
        /// Export data to an external target.
        /// </summary>
        Export,

        /// <summary>
        /// Open from Database
        /// </summary>
        OpenDatabase,

        /// <summary>
        /// Save to Database
        /// </summary>
        SaveDatabase,

        /// <summary>
        /// Delete from Database
        /// </summary>
        DeleteDatabase
    }
}
