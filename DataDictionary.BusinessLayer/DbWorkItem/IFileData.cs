using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Threading;

namespace DataDictionary.BusinessLayer.DbWorkItem
{
    /// <summary>
    /// Interface for Create workItems that Load Data from a File
    /// </summary>
    public interface ILoadFile
    {
        /// <summary>
        /// Create WorkItems that Load Data from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Load(FileInfo file);
    }

    /// <summary>
    /// Interface for Create workItems that Save Data to a File
    /// </summary>
    public interface ISaveFile: ILoadFile
    {
        /// <summary>
        /// Create WorkItems that Save Data to a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IReadOnlyList<WorkItem> Save(FileInfo file);
    }

}
