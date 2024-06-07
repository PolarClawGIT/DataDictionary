using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Selection
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Selection.
    /// </summary>
    [Obsolete("To be removed", true)]
    public interface ISelectionKey : IKey
    {
        /// <summary>
        /// Selection Id of the Scripting Selection.
        /// </summary>
        Guid? SelectionId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Selection.
    /// </summary>
    [Obsolete("To be removed", true)]
    public class SelectionKey : ISelectionKey, IKeyEquality<ISelectionKey>
    {
        /// <inheritdoc/>
        public Guid? SelectionId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public SelectionKey(ISelectionKey source) : base()
        {
            if (source.SelectionId is Guid) { SelectionId = source.SelectionId; }
            else { SelectionId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ISelectionKey? other)
        { return other is ISelectionKey && EqualityComparer<Guid?>.Default.Equals(this.SelectionId, other.SelectionId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is ISelectionKey value && this.Equals(new SelectionKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(SelectionKey left, SelectionKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(SelectionKey left, SelectionKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (SelectionId is Guid) { return (SelectionId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
