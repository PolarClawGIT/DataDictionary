using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template Node.
    /// </summary>
    public interface IScriptingNodeKey : IKey
    {
        /// <summary>
        /// Node Id of the Scripting Template Node.
        /// </summary>
        Guid? NodeId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Template Node.
    /// </summary>
    public class ScriptingNodeKey : IScriptingNodeKey,
        IKeyEquality<IScriptingNodeKey>, IKeyEquality<ScriptingNodeKey>
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Node.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingNodeKey(IScriptingNodeKey source) : base()
        {
            if (source.NodeId is Guid) { NodeId = source.NodeId; }
            else { NodeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ScriptingNodeKey? other)
        { return other is ScriptingNodeKey && EqualityComparer<Guid?>.Default.Equals(this.NodeId, other.NodeId); }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingNodeKey? other)
        { return other is IScriptingNodeKey value && this.Equals(new ScriptingNodeKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IScriptingNodeKey value && this.Equals(new ScriptingNodeKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptingNodeKey left, ScriptingNodeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptingNodeKey left, ScriptingNodeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (NodeId is Guid) { return (NodeId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
