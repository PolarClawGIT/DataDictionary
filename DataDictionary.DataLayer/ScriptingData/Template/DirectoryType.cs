using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Subset of Environment.SpecialFolder that are supported
    /// </summary>
    public enum DirectoryType
    {
        /// <summary>
        /// Unspecified Directory
        /// </summary>
        Null,

        /// <inheritdoc cref="Environment.SpecialFolder.MyDocuments"/>
        MyDocuments,

        /// <inheritdoc cref="Environment.SpecialFolder.ApplicationData"/>
        ApplicationData
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
