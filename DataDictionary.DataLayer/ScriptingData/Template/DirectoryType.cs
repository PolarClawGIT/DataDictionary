using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Supported Folders
    /// </summary>
    public enum DirectoryType
    {
        /// <summary>
        /// Unspecified Directory
        /// </summary>
        Null,

        /// <summary>
        /// Default Location Visual Studio places Source files.
        /// </summary>
        MySources,

        /// <inheritdoc cref="Environment.SpecialFolder.MyDocuments"/>
        MyDocuments,

        /// <summary>
        /// Default Location of the User Downloads.
        /// </summary>
        MyDownloads
    }

    /// <summary>
    /// Interface for Scripting ScriptAs Key.
    /// </summary>
    public interface IDirectoryType : IKey
    {
        /// <summary>
        /// Root Directory to place documents in (must be a supported Special Folder).
        /// </summary>
        DirectoryType RootDirectory { get; }
    }
}
