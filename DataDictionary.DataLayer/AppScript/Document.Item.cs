using System.Data;
using System.Runtime.Serialization;
using Toolbox.BindingTable;

namespace DataDictionary.DataLayer.AppScript
{
    /// <summary>
    /// Interface for the Scripting Document
    /// </summary>
    public interface IDocumentItem : IDocumentKey, ITemplateKey
    {
        /// <summary>
        /// Filename of the Object that this document represents.
        /// </summary>
        String? FileName { get; }
    }
}
