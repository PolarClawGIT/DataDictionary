using DataDictionary.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Composite Key for the Scripting Template Node.
    /// </summary>
    public interface IScriptingNodeKeyComposite : IScriptingNodeKey, IScriptingTemplateKey
    {
    }

    /// <summary>
    /// Implementation of the Composite Key of the Scripting Template Node.
    /// </summary>
    public class ScriptingNodeKeyComposite : ScriptingTemplateKey, IScriptingNodeKeyComposite,
        IKeyEquality<IScriptingNodeKeyComposite>, IKeyEquality<ScriptingNodeKeyComposite>
    {
        /// <inheritdoc/>
        public Guid? NodeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingNodeKeyComposite(IScriptingNodeKeyComposite source) : base(source)
        {
            if (source.NodeId is Guid) { NodeId = source.NodeId; }
            else { NodeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ScriptingNodeKeyComposite? other)
        {
            return other is IScriptingTemplateKey
                   && new ScriptingTemplateKey(other).Equals(this)
                   && other is IScriptingNodeKey
                   && new ScriptingNodeKey(other).Equals(this);
        }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingNodeKeyComposite? other)
        { return other is IScriptingNodeKeyComposite value && this.Equals(new ScriptingNodeKeyComposite(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(Object? obj)
        { return obj is IScriptingNodeKeyComposite value && this.Equals(new ScriptingNodeKeyComposite(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptingNodeKeyComposite left, ScriptingNodeKeyComposite right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptingNodeKeyComposite left, ScriptingNodeKeyComposite right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (TemplateId is Guid && NodeId is Guid)
            { return HashCode.Combine(TemplateId, NodeId); }
            else { return Guid.Empty.GetHashCode(); }
        }


        #endregion
    }
}
