using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{

    /// <summary>
    /// List of supported Script types.
    /// </summary>
    public enum ScriptAsType
    {
        /// <summary>
        /// Not Scripted
        /// </summary>
        none,

        /// <summary>
        /// Microsoft C# code
        /// </summary>
        CSharp,

        /// <summary>
        /// Microsoft Visual Basic code
        /// </summary>
        VBNet,


        /// <summary>
        /// Microsoft SQL script
        /// </summary>
        MsSql,

        /// <summary>
        /// Script as Text
        /// </summary>
        Text,

        /// <summary>
        /// Script As XElement
        /// </summary>
        XML
    }

    /// <summary>
    /// Interface for Scripting ScriptAs Key.
    /// </summary>
    public interface IScriptAsType : IKey
    {
        /// <summary>
        /// Type of Script that is Generated
        /// </summary>
        ScriptAsType ScriptAs { get; }
    }
}
