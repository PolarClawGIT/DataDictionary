using DataDictionary.DataLayer.ApplicationData.Scope;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.BusinessLayer.Scripting
{
    /// <summary>
    /// Interface for the Scripting Schema Column data.
    /// </summary>
<<<<<<< HEAD
    public interface IColumnValue : IColumnIndex
=======
    public interface IColumnIValue : IColumnIndex
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc cref="DataColumn.AllowDBNull"/>
        Boolean AllowDBNull { get; }

        /// <inheritdoc cref="DataColumn.DataType"/>
        Type DataType { get; }
    }

    /// <summary>
    /// Implementation for the Scripting Schema Column data.
    /// </summary>
    /// <remarks>The items are expected to be static.</remarks>
<<<<<<< HEAD
    public class ColumnValue : IColumnValue
=======
    public class ColumnValue : IColumnIValue
>>>>>>> RenameIndexValue
    {
        /// <inheritdoc/>
        public ScopeType Scope { get; init; } = ScopeType.Null;

        /// <inheritdoc/>
        public String ColumnName { get; init; } = String.Empty;

        /// <inheritdoc/>
        public Boolean AllowDBNull { get; init; } = false;

        /// <inheritdoc/>
        public Type DataType { get; init; } = typeof(object);

        /// <summary>
        /// Constructor for Schema Scripting Schema Column Items
        /// </summary>
        public ColumnValue() : base()
        { }

        /// <summary>
        /// Constructor for Schema Scripting Schema Column Items
        /// </summary>
        public ColumnValue(ScopeType scope, DataColumn source) : this()
        {
            Scope = scope;
            ColumnName = source.ColumnName;
            AllowDBNull = source.AllowDBNull;
            DataType = source.DataType;
        }

        /// <inheritdoc/>
        public override string ToString()
        { return String.Format("{0} {1}", Scope.ToName(), ColumnName); }
    }
}
