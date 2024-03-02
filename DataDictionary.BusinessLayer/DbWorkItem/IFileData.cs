using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Interface for Create workItems that Imports/Exports Data to a File
    /// </summary>
    public interface IFileData
    {
        /// <summary>
        /// Create WorkItems that Load Data from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Import(FileInfo file);

        /// <summary>
        /// Create WorkItems that Save Data to a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Export(FileInfo file);
    }

    interface IDataTableFile
    {
        /// <summary>
        /// Returns a List of DataTables for use Exporting to a File
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<System.Data.DataTable> Export();

        /// <summary>
        /// Loads the DataObject using the data found in the DataSet.
        /// </summary>
        /// <param name="source"></param>
        void Import(System.Data.DataSet source);
    }
}
