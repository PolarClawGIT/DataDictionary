using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Describes common file pattern properties
    /// </summary>
    public interface IDocumentFilePattern
    {
        /// <summary>
        /// Prefix to add to the front of the file name.
        /// </summary>
        String? FilePrefix { get; }

        /// <summary>
        /// Prefix to add to the end of the file name.
        /// </summary>
        String? FileSuffix { get; }

        /// <summary>
        /// File Extension to add to the end of the file name.
        /// </summary>
        String? FileExtension { get; }
    }
}
