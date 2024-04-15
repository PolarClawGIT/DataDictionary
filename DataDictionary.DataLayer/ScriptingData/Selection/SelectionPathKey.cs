using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Selection Path.
    /// </summary>
    public interface ISelectionPathKey : IKey
    {
        /// <summary>
        /// Instance Id of the Scripting Selection Path.
        /// </summary>
        Guid? SelectionPathId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Selection Path.
    /// </summary>
    public class SelectionPathKey : ISelectionPathKey, IKeyEquality<ISelectionPathKey>
    {
        /// <inheritdoc/>
        public Guid? SelectionPathId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Selection Path.
        /// </summary>
        /// <param name="source"></param>
        public SelectionPathKey(ISelectionPathKey source) : base()
        {
            if (source.SelectionPathId is Guid) { SelectionPathId = source.SelectionPathId; }
            else { SelectionPathId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ISelectionPathKey? other)
        { return other is ISelectionPathKey && EqualityComparer<Guid?>.Default.Equals(this.SelectionPathId, other.SelectionPathId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISelectionPathKey value && this.Equals(new SelectionPathKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(SelectionPathKey left, SelectionPathKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SelectionPathKey left, SelectionPathKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (SelectionPathId is Guid) { return (SelectionPathId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
