using DataDictionary.Resource;

namespace DataDictionary.DataLayer.ScriptingData
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template Attribute.
    /// </summary>
    public interface IScriptingAttributeKey : IKey
    {
        /// <summary>
        /// Attribute Id of the Scripting Template Attribute.
        /// </summary>
        Guid? AttributeId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Template Attribute.
    /// </summary>
    public class ScriptingAttributeKey : IScriptingAttributeKey,
        IKeyEquality<IScriptingAttributeKey>, IKeyEquality<ScriptingAttributeKey>
    {
        /// <inheritdoc/>
        public Guid? AttributeId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template Attribute.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingAttributeKey(IScriptingAttributeKey source) : base()
        {
            if (source.AttributeId is Guid) { AttributeId = source.AttributeId; }
            else { AttributeId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(ScriptingAttributeKey? other)
        { return other is ScriptingAttributeKey && EqualityComparer<Guid?>.Default.Equals(AttributeId, other.AttributeId); }

        /// <inheritdoc/>
        public Boolean Equals(IScriptingAttributeKey? other)
        { return other is IScriptingAttributeKey value && Equals(new ScriptingAttributeKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is IScriptingAttributeKey value && Equals(new ScriptingAttributeKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptingAttributeKey left, ScriptingAttributeKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptingAttributeKey left, ScriptingAttributeKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (AttributeId is Guid) { return AttributeId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}

