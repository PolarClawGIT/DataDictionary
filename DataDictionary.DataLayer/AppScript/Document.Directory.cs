using DataDictionary.Resource.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the common elements within a Document Directory
    /// </summary>
    public interface IDocumentDirectory
    {
        /// <summary>
        /// Name of the Special Folder used as the Root Directory.
        /// </summary>
        /// <remarks>
        /// This uses an Enum that represents locations in: Environment.SpecialFolder.UserProfile
        /// </remarks>
        DirectoryType RootFolder { get; }

        /// <summary>
        /// Relative Directory off of the Root Directory where the XML Input file is located.
        /// </summary>
        String? RelativePath { get; }
    }
}
