using DataDictionary.BusinessLayer.AppCatalog;
using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppModel;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppModel
{
    /// <inheritdoc/>
    public interface IProcessIndexName : IProcessKeyName
    { }

    /// <inheritdoc/>
    public class ProcessIndexName : ProcessKeyName, IProcessIndexName,
        IKeyEquality<IProcessIndexName>, IKeyEquality<ProcessIndexName>
    {
        /// <inheritdoc cref="ProcessKeyName(IProcessKeyName)"/>
        public ProcessIndexName(IProcessIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IProcessIndexName? other)
        { return other is IProcessKeyName key && Equals(new ProcessKeyName(key)); }

        /// <inheritdoc/>
        public Boolean Equals(ProcessIndexName? other)
        { return other is IProcessKeyName key && Equals(new ProcessKeyName(key)); }

        /// <summary>
        /// Convert ProcessIndexName to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(ProcessIndexName source)
        { return new DataIndexName() { Title = source.ProcessTitle ?? String.Empty }; }
    }
}
