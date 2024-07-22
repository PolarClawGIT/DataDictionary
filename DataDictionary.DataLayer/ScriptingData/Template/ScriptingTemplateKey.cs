using DataDictionary.Resource;

namespace DataDictionary.DataLayer.ScriptingData.Template
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template.
    /// </summary>
    public interface IScriptingTemplateKey : IKey
    {
        /// <summary>
        /// Template Id of the Scripting Template.
        /// </summary>
        Guid? TemplateId { get; }
    }

    /// <summary>
    /// Implementation of the Primary Key of the Scripting Template.
    /// </summary>
    public class ScriptingTemplateKey : IScriptingTemplateKey, IKeyEquality<IScriptingTemplateKey>
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template.
        /// </summary>
        /// <param name="source"></param>
        public ScriptingTemplateKey(IScriptingTemplateKey source) : base()
        {
            if (source.TemplateId is Guid) { TemplateId = source.TemplateId; }
            else { TemplateId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(IScriptingTemplateKey? other)
        { return other is IScriptingTemplateKey && EqualityComparer<Guid?>.Default.Equals(this.TemplateId, other.TemplateId); }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        { return obj is IScriptingTemplateKey value && this.Equals(new ScriptingTemplateKey(value)); }

        /// <inheritdoc/>
        public static bool operator ==(ScriptingTemplateKey left, ScriptingTemplateKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static bool operator !=(ScriptingTemplateKey left, ScriptingTemplateKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (TemplateId is Guid) { return (TemplateId).GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
