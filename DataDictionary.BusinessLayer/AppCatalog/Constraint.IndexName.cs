using DataDictionary.BusinessLayer.ToolSet;
using DataDictionary.DataLayer.AppCatalog;
using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.AppCatalog
{
    /// <inheritdoc/>
    public interface IConstraintIndexName : IConstraintKeyName, ISchemaIndexName
    { }

    /// <inheritdoc/>
    public class ConstraintIndexName : ConstraintKeyName, IConstraintIndexName,
        IKeyEquality<IConstraintIndexName>, IKeyEquality<ConstraintIndexName>
    {
        /// <inheritdoc cref="ConstraintKeyName(IConstraintKeyName)"/>
        public ConstraintIndexName(IConstraintIndexName source) : base(source) { }

        /// <inheritdoc/>
        public Boolean Equals(IConstraintIndexName? other)
        { return other is IConstraintKeyName value && Equals(new ConstraintKeyName(value)); }

        /// <inheritdoc/>
        public Boolean Equals(ConstraintIndexName? other)
        { return other is IConstraintKeyName value && Equals(new ConstraintKeyName(value)); }

        /// <summary>
        /// Convert ConstraintIndex to a DataIndexName
        /// </summary>
        /// <param name="source"></param>
        public static implicit operator DataIndexName(ConstraintIndexName source)
        { return new DataIndexName() { Title = source.ConstraintName ?? String.Empty }; }
    }
}
