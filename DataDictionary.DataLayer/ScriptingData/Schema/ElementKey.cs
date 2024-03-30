using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Schema
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Schema Element.
    /// </summary>
    public interface IElementKey : IKey
    {
        /// <summary>
        /// Schema Id of the Scripting Schema.
        /// </summary>
        Guid? ElementId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Schema Element.
    /// </summary>
    public class ElementKey : IElementKey, IKeyEquality<IElementKey>
    {
        /// <inheritdoc/>
        public Guid? ElementId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Property.
        /// </summary>
        /// <param name="source"></param>
        public ElementKey(IElementKey source) : base()
        {
            if (source.ElementId is Guid) { ElementId = source.ElementId; }
            else { ElementId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IElementKey? other)
        { return other is IElementKey && EqualityComparer<Guid?>.Default.Equals(this.ElementId, other.ElementId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IElementKey value && this.Equals(new ElementKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ElementKey left, ElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ElementKey left, ElementKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (ElementId is Guid) { return (ElementId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
