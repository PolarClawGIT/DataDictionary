using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Element.
    /// </summary>
    public interface IScriptingElementKey : IKey
    {
        /// <summary>
        /// Element Id of the Scripting Element.
        /// </summary>
        Guid? ElementId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Element.
    /// </summary>
    public class ScriptingElementKey : IScriptingElementKey, IKeyEquality<IScriptingElementKey>
    {
        /// <inheritdoc/>
        public Guid? ElementId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Element.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingElementKey(IScriptingElementKey source) : base()
        {
            if (source.ElementId is Guid) { ElementId = source.ElementId; }
            else { ElementId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IScriptingElementKey? other)
        { return other is IScriptingElementKey && EqualityComparer<Guid?>.Default.Equals(this.ElementId, other.ElementId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptingElementKey value && this.Equals(new ScriptingElementKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ScriptingElementKey left, ScriptingElementKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScriptingElementKey left, ScriptingElementKey right)
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
