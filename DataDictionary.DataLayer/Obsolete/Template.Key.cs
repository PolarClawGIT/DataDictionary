using DataDictionary.Resource;

namespace DataDictionary.DataLayer.Obsolete
{
    /// <summary>
    /// Interface for the Primary Key for the Scripting Template.
    /// </summary>
    [Obsolete]
    public interface ITemplateKey : IKey
    {
        /// <summary>
        /// Template Id of the Scripting Template.
        /// </summary>
        Guid? TemplateId { get; }
    }

    /// <summary>
    /// Implementation for the Primary Key for the Scripting Template.
    /// </summary>
    [Obsolete]
    public class TemplateKey : ITemplateKey,
        IKeyEquality<ITemplateKey>, IKeyEquality<TemplateKey>
    {
        /// <inheritdoc/>
        public Guid? TemplateId { get; init; } = Guid.Empty;

        /// <summary>
        /// Constructor for the Blank/Empty Template Key
        /// </summary>
        /// <remarks>Empty Key is never equal to anything.</remarks>
        public TemplateKey() : base()
        { }

        /// <summary>
        /// Constructor for the Primary Key of the Scripting Template.
        /// </summary>
        /// <param name="source"></param>
        public TemplateKey(ITemplateKey source) : base()
        {
            if (source.TemplateId is Guid) { TemplateId = source.TemplateId; }
            else { TemplateId = Guid.Empty; }
        }

        #region IEquatable
        /// <inheritdoc/>
        public Boolean Equals(TemplateKey? other)
        { return other is TemplateKey key && key.TemplateId != Guid.Empty && EqualityComparer<Guid?>.Default.Equals(TemplateId, other.TemplateId); }

        /// <inheritdoc/>
        public Boolean Equals(ITemplateKey? other)
        { return other is ITemplateKey value && Equals(new TemplateKey(value)); }

        /// <inheritdoc/>
        public override Boolean Equals(object? obj)
        { return obj is ITemplateKey value && Equals(new TemplateKey(value)); }

        /// <inheritdoc/>
        public static Boolean operator ==(TemplateKey left, TemplateKey right)
        { return left.Equals(right); }

        /// <inheritdoc/>
        public static Boolean operator !=(TemplateKey left, TemplateKey right)
        { return !left.Equals(right); }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            if (TemplateId is Guid) { return TemplateId.GetHashCode(); }
            else { return Guid.Empty.GetHashCode(); }
        }
        #endregion
    }
}
